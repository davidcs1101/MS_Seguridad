using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using SEG.Dominio.Repositorio;
using SEG.DataAccess;
using SEG.Infraestructura.Aplicacion.ServiciosExternos;
using SEG.Infraestructura.Dominio.Repositorio;
using SEG.Aplicacion.CasosUso.Implementaciones;
using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.Servicios.Interfaces;
using SEG.Aplicacion.Servicios.Implementaciones;
using SEG.Api.Seguridad.Middlewares;
using SEG.Dominio.Servicios.Interfaces;
using SEG.Dominio.Servicios.Implementaciones;
using Hangfire;
using Hangfire.MySql;
using SEG.Dominio.Repositorio.UnidadTrabajo;
using SEG.Intraestructura.Dominio.Repositorio.UnidadTrabajo;
using SEG.Intraestructura.Dominio.Repositorio;
using SEG.Infraestructura.Servicios.Interfaces;
using SEG.Infraestructura.Servicios.Implementaciones;
using SEG.Dtos.AppSettings;
using SEG.Aplicacion.ServiciosExternos.config;
using SEG.Infraestructura.Aplicacion.ServiciosExternos.config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuramos Swagger para que permita envío de Bearer Token
// Agregar esto después de 'builder.Services.AddSwaggerGen();'
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SEG.Api.Seguridad", Version = "1.0" });

    // Configuración de Bearer Token
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor ingrese el token Bearer en el siguiente formato: Bearer su_token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();

builder.Services.AddScoped<IUsuarioSedeGrupoRepositorio, UsuarioSedeGrupoRepositorio>();
builder.Services.AddScoped<IUsuarioSedeGrupoServicio, UsuarioSedeGrupoServicio>();

builder.Services.AddScoped<IGrupoRepositorio, GrupoRepositorio>();
builder.Services.AddScoped<IGrupoServicio, GrupoServicio>();
builder.Services.AddScoped<IProgramaRepositorio, ProgramaRepositorio>();
builder.Services.AddScoped<IProgramaServicio, ProgramaServicio>();
builder.Services.AddScoped<IGrupoProgramaRepositorio, GrupoProgramaRepositorio>();
builder.Services.AddScoped<IGrupoProgramaServicio, GrupoProgramaServicio>();
builder.Services.AddScoped<IAutenticacionServicio, AutenticacionServicio>();

builder.Services.AddScoped<IApiResponse, ApiResponse>();

builder.Services.AddScoped<IUsuarioValidador, UsuarioValidador>();

builder.Services.AddScoped<IConstructorTextosNotificacion, ConstructorTextosNotificacion>();
builder.Services.AddScoped<IConstructorMensajesNotificacionCorreo, ConstructorMensajesNotificacionCorreo>();
builder.Services.AddScoped<INotificadorCorreo, NotificadorCorreo>();

builder.Services.AddScoped<IColaSolicitudRepositorio, ColaSolicitudRepositorio>();

builder.Services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajoEF>();

builder.Services.AddScoped<ISerializadorJsonServicio, SerializadorJsonServicio>();

builder.Services.AddScoped<IRespuestaHttpValidador, RespuestaHttpValidador>();

builder.Services.AddScoped<IColaSolicitudServicio, ColaSolicitudServicio>();
builder.Services.AddScoped<IJobEncoladorServicio, JobEncoladorServicio>();

//Servicio que obtiene el UsuarioId del Token
builder.Services.AddScoped<IUsuarioContextoServicio, UsuarioContextoServicio>();

//Servicio para gestionar sedes desde el micorservicio de empresas
builder.Services.AddScoped<IMSEmpresas, MSEmpresas>();
builder.Services.AddScoped<IMSDatosComunes, MSDatosComunes>();
builder.Services.AddScoped(typeof(IEntidadValidador<>), typeof(EntidadValidador<>));


#region REG_Servicios de configuraciones Appsettings
builder.Services.Configure<TrabajosColasSettings>(builder.Configuration.GetSection("TrabajosColas"));
builder.Services.AddSingleton<IConfiguracionesTrabajosColas, ConfiguracionesTrabajosColas>();

builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));
builder.Services.AddSingleton<IConfiguracionesJwt, ConfiguracionesJwt>();
#endregion


//Configuramos AutoMapper para el mapeo de DTOS a las entidades y le decimos que se hará a nivel de Ensamblado
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Configuración de log4net
var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
builder.Services.AddLogging(loggingBuilder => { loggingBuilder.AddLog4Net(); });

// Configuracion de JWT
var configuracionJWT = builder.Configuration.GetSection("JWT");
var issuer = configuracionJWT["Issuer"];
var audiences = configuracionJWT.GetSection("Audience").GetChildren().Select(a => a.Value).ToList();
var key = configuracionJWT["Key"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer
    (opcion =>
    {
        opcion.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudiences = audiences,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ClockSkew = TimeSpan.Zero //No se permite tolerancia de tiempo una vez el token caduca (por defecto es 5 minutos si no se establece)
        };
    });

builder.Services.AddAuthorization(options => options.AddPolicy("UsuariosPermiso",
permiso => permiso.RequireClaim("Programa", "USUARIOS")));
builder.Services.AddAuthorization(options => options.AddPolicy("GruposPermiso",
permiso => permiso.RequireClaim("Programa", "GRUPOS")));
builder.Services.AddAuthorization(options => options.AddPolicy("ProgramasPermiso",
permiso => permiso.RequireClaim("Programa", "PROGRAMAS")));
builder.Services.AddAuthorization(options => options.AddPolicy("GruposProgramasPermiso",
permiso => permiso.RequireClaim("Programa", "GRUPOSPROGRAMAS")));
builder.Services.AddAuthorization(options => options.AddPolicy("UsuarioSedesGruposPermiso",
permiso => permiso.RequireClaim("Programa", "USUARIOSSEDESGRUPOS")));

builder.Services.AddDbContext<AppDbContext>
    (opciones => opciones
    .UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    //ServerVersion.Parse("8.0.39-mysql")
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

builder.Services.AddHangfire(opciones =>
{
    opciones.UseStorage(
        new MySqlStorage(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            new MySqlStorageOptions { TablesPrefix = "XHAF_SEG_" }));
});

//Necesario para correr el background job server
builder.Services.AddHangfireServer(opciones => {opciones.ServerName = "MSSeguridadServer";});


//Servicio para obtener el usuarioId de los Tokens de la solicitud
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<MiddlewareManejadorTokens>();


//Configuracion para llamado de otros MicroServicios atraves de la Url Gateway
var urlGateway = builder.Configuration["UrlGateway"];

builder.Services.AddHttpClient<IMSEnvioCorreosBackgroundServicio, MSEnvioCorreosBackgroundServicio>
    (cliente =>
    {
        cliente.BaseAddress = new Uri(urlGateway);
        cliente.DefaultRequestHeaders.Add("Accept", "application/json");
    });

builder.Services.AddHttpClient<IMSEmpresasContextoWebServicio, MSEmpresasContextoWebServicio>
    (cliente =>
    {
        cliente.BaseAddress = new Uri(urlGateway);
        cliente.DefaultRequestHeaders.Add("Accept", "application/json");
    })
    .AddHttpMessageHandler<MiddlewareManejadorTokens>();

builder.Services.AddHttpClient<IMSDatosComunesContextoWebServicio, MSDatosComunesContextoWebServicio>
    (cliente =>
    {
        cliente.BaseAddress = new Uri(urlGateway);
        cliente.DefaultRequestHeaders.Add("Accept", "application/json");
    })
    .AddHttpMessageHandler<MiddlewareManejadorTokens>();



var app = builder.Build();

//Dashboard para ver los jobs en el navegador
app.UseHangfireDashboard("/hangfire");

//Configuracion para la tarea Job en segundo plano que rastrea las solicitudes pendientes de procesar.
var configuracionTrabajosColas = app.Services.GetRequiredService<IConfiguracionesTrabajosColas>();
RecurringJob.AddOrUpdate<IColaSolicitudServicio>("procesador_solicitudes", x => x.ProcesarColaSolicitudesAsync(),
    configuracionTrabajosColas.ObtenerProcesarColaSolicitudesCron());


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<MiddlewareExcepcionesGlobales>();

app.UseAuthentication();
app.UseMiddleware<MiddlewareAutorizationPersonalizado>();  // Aquí agregamos nuestro middleware personalizado
app.UseAuthorization();

app.MapControllers();

app.Run();
