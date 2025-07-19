using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SEG.Dtos;
using SEG.Dominio.Entidades;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Utilidades;
using SEG.Dominio.Repositorio;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.Servicios.Interfaces;
using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Dominio.Servicios.Interfaces;
using SEG.Aplicacion.ServiciosExternos.config;

namespace SEG.Aplicacion.CasosUso.Implementaciones
{
    public class AutenticacionServicio: IAutenticacionServicio
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IUsuarioSedeGrupoRepositorio _usuarioSedeRepositorio;
        private readonly IGrupoProgramaRepositorio _grupoRepositorio;
        private readonly IConfiguration _configuracion;
        private readonly IUsuarioContextoServicio _usuarioContextoServicio;
        private readonly IApiResponse _apiResponse;
        private readonly IUsuarioValidador _usuarioValidador;
        private readonly IEntidadValidador<SEG_UsuarioSedeGrupo> _usuarioSedeGrupoValidador;
        private readonly IConfiguracionesJwt _configuracionesJwt;

        public AutenticacionServicio(IUsuarioRepositorio usuarioRepositorio, IUsuarioSedeGrupoRepositorio usuarioSedeRepositorio, IGrupoProgramaRepositorio grupoRepositorio, IConfiguration configuracion,
            IUsuarioContextoServicio usuarioContextoServicio, IApiResponse apiResponseServicio, IUsuarioValidador usuarioValidador, IEntidadValidador<SEG_UsuarioSedeGrupo> usuarioSedeGrupoValidador, IConfiguracionesJwt configuracionesJwt)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _usuarioSedeRepositorio = usuarioSedeRepositorio;
            _grupoRepositorio = grupoRepositorio;
            _configuracion = configuracion;
            _usuarioContextoServicio = usuarioContextoServicio;
            _apiResponse = apiResponseServicio;
            _usuarioValidador = usuarioValidador;
            _usuarioSedeGrupoValidador = usuarioSedeGrupoValidador;
            _configuracionesJwt = configuracionesJwt;
        }

        public async Task<ApiResponse<string>> AutenticarUsuarioAsync(AutenticacionRequest autenticacionRequest)
        {
            var usuario = await _usuarioRepositorio.ObtenerPorUsuarioAsync(autenticacionRequest.NombreUsuario);
            _usuarioValidador.ValidarLoguin(usuario, ProcesadorClaves.EncriptarClave(autenticacionRequest.Clave), Textos.Usuarios.MENSAJE_LOGIN_INCORRECTO);
         
            var token = await GenerarTokenAsync(usuario, null, null);
            return _apiResponse.CrearRespuesta(true, "", token);
        }

        public async Task<ApiResponse<string>> AutenticarSedeAsync(int sedeId)
        {
            var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            var usuarioSede = await _usuarioSedeRepositorio.ObtenerUsuarioSedeAsync(usuarioId, sedeId);
            _usuarioSedeGrupoValidador.ValidarDatoNoEncontrado(usuarioSede, Textos.Usuarios.MENSAJE_LOGIN_SEDE_INCORRECTO);

            var token = await GenerarTokenAsync(usuarioSede.Usuario, usuarioSede.GrupoId, usuarioSede.SedeId);
            return _apiResponse.CrearRespuesta(true, "", token);
        }

        private  async Task<string> GenerarTokenAsync(SEG_Usuario usuario, int? grupoId, int? sedeId)
        {
            //Datos de configuracon para el Token
            var issuer = _configuracionesJwt.ObtenerIssuer();
            var audiences = _configuracionesJwt.ObtenerAudience();
            int tiempoExpiracion = _configuracionesJwt.ObtenerMinutosDuracionTokenAutenticacionUsuario();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuracionesJwt.ObtenerKey()));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            #region REG_Adicionamos Claims específicos del usuario
            var claims = new List<Claim>
            {
                new Claim("UsuarioId", usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreUsuario)
            };
            if (grupoId.HasValue)
            {
                claims.Add(new Claim("GrupoId", grupoId.ToString()));
                var programas = _grupoRepositorio.ListarProgramasPorGrupo(grupoId.Value)
                    .Where(gp => gp.EstadoActivo);
                foreach (var programa in programas)
                {
                    claims.Add(new Claim("Programa", programa.Programa.Codigo.ToUpper()));
                }
            }
            if (sedeId.HasValue)
            {
                claims.Add(new Claim("SedeId", sedeId.ToString()));
            }
            #endregion


            #region REG_Adicionamos Claims de opciones para seleccion de sedes y cambio de email
            /*
             Los siguientes Claims se adicionan solo si el usuario ya ha realizado el cambio de clave ya que es un requisisto 
            obligatorio para poder navegar en las opciones controladas por los permisos que aquí se adicionan.
             */
            if (!usuario.CambiarClave)
            {
                tiempoExpiracion = Convert.ToInt32(_configuracionesJwt.ObtenerMinutosDuracionTokenAutenticacionSede());
                claims.Add(new Claim("Accion", "CAMBIOCLAVEOK"));
            }
            #endregion

            var token = new JwtSecurityToken(
                issuer : issuer,
                audience: _configuracionesJwt.ObtenerAudienceTexto(),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(tiempoExpiracion),
                signingCredentials: credenciales);
            token.Payload["aud"] = audiences;

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}