using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SEG.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SEG_Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SEG_ColaSolicitudes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<string>(type: "varchar(250)", nullable: false, comment: "Tipo de solicitud a realizar.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UrlDestino = table.Column<string>(type: "varchar(500)", nullable: false, comment: "Url destino para la cual se publica el evento.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Payload = table.Column<string>(type: "Text", nullable: false, comment: "Payload para la solicitud.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<short>(type: "smallint", nullable: false, comment: "Estado del proceso de la solicitud. (0: Pendiente, 1: Procesando, 2: Exitosa, 3: Fallida)."),
                    Intentos = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "Intentos del proceso"),
                    FechaCreado = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    FechaUltimoIntento = table.Column<DateTime>(type: "datetime", nullable: true),
                    ErrorMensaje = table.Column<string>(type: "Text", nullable: true, comment: "Detalle de error de procasado de la solicitud.")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEG_ColaSolicitudes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SEG_Acciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(type: "varchar(30)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(250)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioCreadorId = table.Column<int>(type: "int", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioModificadorId = table.Column<int>(type: "int", nullable: true),
                    FechaModificado = table.Column<DateTime>(type: "datetime", nullable: true),
                    EstadoActivo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEG_Acciones", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SEG_Grupos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(type: "varchar(30)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(250)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioCreadorId = table.Column<int>(type: "int", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioModificadorId = table.Column<int>(type: "int", nullable: true),
                    FechaModificado = table.Column<DateTime>(type: "datetime", nullable: true),
                    EstadoActivo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEG_Grupos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SEG_Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TipoIdentificacionId = table.Column<int>(type: "int", nullable: false),
                    Identificacion = table.Column<string>(type: "varchar(20)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre1 = table.Column<string>(type: "varchar(150)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre2 = table.Column<string>(type: "varchar(25)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Apellido1 = table.Column<string>(type: "varchar(25)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Apellido2 = table.Column<string>(type: "varchar(25)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(150)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NombreUsuario = table.Column<string>(type: "varchar(60)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Clave = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CambiarClave = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    GrupoDirectoId = table.Column<int>(type: "int", nullable: true, comment: "ID del grupo que tiene directamente el usuario, es decir, no depende de la empresa"),
                    UsuarioCreadorId = table.Column<int>(type: "int", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioModificadorId = table.Column<int>(type: "int", nullable: true),
                    FechaModificado = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    EstadoActivo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEG_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SEG_Usuarios_SEG_Grupos_GrupoDirectoId",
                        column: x => x.GrupoDirectoId,
                        principalTable: "SEG_Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SEG_Usuarios_SEG_Usuarios_UsuarioCreadorId",
                        column: x => x.UsuarioCreadorId,
                        principalTable: "SEG_Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SEG_Usuarios_SEG_Usuarios_UsuarioModificadorId",
                        column: x => x.UsuarioModificadorId,
                        principalTable: "SEG_Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SEG_Programas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(type: "varchar(30)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(250)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioCreadorId = table.Column<int>(type: "int", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioModificadorId = table.Column<int>(type: "int", nullable: true),
                    FechaModificado = table.Column<DateTime>(type: "datetime", nullable: true),
                    EstadoActivo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEG_Programas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SEG_Programas_SEG_Usuarios_UsuarioCreadorId",
                        column: x => x.UsuarioCreadorId,
                        principalTable: "SEG_Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SEG_Programas_SEG_Usuarios_UsuarioModificadorId",
                        column: x => x.UsuarioModificadorId,
                        principalTable: "SEG_Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SEG_UsuariosSedesGrupos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    SedeId = table.Column<int>(type: "int", nullable: false),
                    GrupoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioCreadorId = table.Column<int>(type: "int", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioModificadorId = table.Column<int>(type: "int", nullable: true),
                    FechaModificado = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    EstadoActivo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEG_UsuariosSedesGrupos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SEG_UsuariosSedesGrupos_SEG_Grupos_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "SEG_Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SEG_UsuariosSedesGrupos_SEG_Usuarios_UsuarioCreadorId",
                        column: x => x.UsuarioCreadorId,
                        principalTable: "SEG_Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SEG_UsuariosSedesGrupos_SEG_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "SEG_Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SEG_UsuariosSedesGrupos_SEG_Usuarios_UsuarioModificadorId",
                        column: x => x.UsuarioModificadorId,
                        principalTable: "SEG_Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SEG_GruposProgramas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GrupoId = table.Column<int>(type: "int", nullable: false),
                    ProgramaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioCreadorId = table.Column<int>(type: "int", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioModificadorId = table.Column<int>(type: "int", nullable: true),
                    FechaModificado = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    EstadoActivo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEG_GruposProgramas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SEG_GruposProgramas_SEG_Grupos_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "SEG_Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SEG_GruposProgramas_SEG_Programas_ProgramaId",
                        column: x => x.ProgramaId,
                        principalTable: "SEG_Programas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SEG_GruposProgramas_SEG_Usuarios_UsuarioCreadorId",
                        column: x => x.UsuarioCreadorId,
                        principalTable: "SEG_Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SEG_GruposProgramas_SEG_Usuarios_UsuarioModificadorId",
                        column: x => x.UsuarioModificadorId,
                        principalTable: "SEG_Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SEG_Permisos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProgramaId = table.Column<int>(type: "int", nullable: false),
                    AccionId = table.Column<int>(type: "int", nullable: false),
                    Codigo = table.Column<string>(type: "varchar(70)", nullable: false, comment: "Código único del permiso.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(250)", nullable: false, comment: "Nombre descriptivo del permiso.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioCreadorId = table.Column<int>(type: "int", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioModificadorId = table.Column<int>(type: "int", nullable: true),
                    FechaModificado = table.Column<DateTime>(type: "datetime", nullable: true),
                    EstadoActivo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEG_Permisos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SEG_Permisos_SEG_Acciones_AccionId",
                        column: x => x.AccionId,
                        principalTable: "SEG_Acciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SEG_Permisos_SEG_Programas_ProgramaId",
                        column: x => x.ProgramaId,
                        principalTable: "SEG_Programas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SEG_Permisos_SEG_Usuarios_UsuarioCreadorId",
                        column: x => x.UsuarioCreadorId,
                        principalTable: "SEG_Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SEG_Permisos_SEG_Usuarios_UsuarioModificadorId",
                        column: x => x.UsuarioModificadorId,
                        principalTable: "SEG_Usuarios",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SEG_GruposPermisos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GrupoId = table.Column<int>(type: "int", nullable: false),
                    PermisoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioCreadorId = table.Column<int>(type: "int", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioModificadorId = table.Column<int>(type: "int", nullable: true),
                    FechaModificado = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    EstadoActivo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEG_GruposPermisos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SEG_GruposPermisos_SEG_Grupos_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "SEG_Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SEG_GruposPermisos_SEG_Permisos_PermisoId",
                        column: x => x.PermisoId,
                        principalTable: "SEG_Permisos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SEG_GruposPermisos_SEG_Usuarios_UsuarioCreadorId",
                        column: x => x.UsuarioCreadorId,
                        principalTable: "SEG_Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SEG_GruposPermisos_SEG_Usuarios_UsuarioModificadorId",
                        column: x => x.UsuarioModificadorId,
                        principalTable: "SEG_Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "SEG_Usuarios",
                columns: new[] { "Id", "Apellido1", "Apellido2", "CambiarClave", "Clave", "Email", "EstadoActivo", "FechaCreado", "FechaModificado", "GrupoDirectoId", "Identificacion", "Nombre1", "Nombre2", "NombreUsuario", "TipoIdentificacionId", "UsuarioCreadorId", "UsuarioModificadorId" },
                values: new object[] { 1, "SISTEMA", "", true, "1feTCdMwhKKkOSWaM5+yXEI0ZrBPlq9pbnB4k4+JRUU=", "CORREO@GMAIL.COM", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(1929), null, null, "ADMINISTRADOR", "ADMINISTRADOR", "", "ADMINISTRADOR", 1, 1, null });

            migrationBuilder.InsertData(
                table: "SEG_Acciones",
                columns: new[] { "Id", "Codigo", "EstadoActivo", "FechaCreado", "FechaModificado", "Nombre", "UsuarioCreadorId", "UsuarioModificadorId" },
                values: new object[,]
                {
                    { 1, "CONSULTAR", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2150), null, "CONSULTAR", 1, null },
                    { 2, "CREAR", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2152), null, "CREAR", 1, null },
                    { 3, "MODIFICAR", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2153), null, "MODIFICAR", 1, null },
                    { 4, "ELIMINAR", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2155), null, "ELIMINAR", 1, null },
                    { 5, "LISTAR", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2157), null, "LISTAR", 1, null },
                    { 6, "CREARCONSEDE", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2158), null, "CREAR CON SEDE", 1, null },
                    { 7, "CREARCONGRUPO", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2160), null, "CREAR CON GRUPO", 1, null }
                });

            migrationBuilder.InsertData(
                table: "SEG_Grupos",
                columns: new[] { "Id", "Codigo", "EstadoActivo", "FechaCreado", "FechaModificado", "Nombre", "UsuarioCreadorId", "UsuarioModificadorId" },
                values: new object[,]
                {
                    { 1, "ADMINISTRADORSISTEMA", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2064), null, "ADMINISTRADOR SISTEMA", 1, null },
                    { 2, "ADMINISTRADOREMPRESA", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2067), null, "ADMINISTRADOR DE EMPRESA", 1, null },
                    { 3, "ADMINISTRADORSEDE", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2069), null, "ADMINISTRADOR DE SEDE", 1, null },
                    { 4, "CONTRARREFERENCIA", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2116), null, "USUARIOS DE CONTRARREFERENCIA", 1, null },
                    { 5, "REFERENCIA", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2118), null, "USUARIOS DE REFERENCIA", 1, null },
                    { 6, "REFERENCIAYCONTRARREFERENCIA", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2120), null, "USUARIOS DE REFERENCIA Y CONTRARREFERENCIA", 1, null }
                });

            migrationBuilder.InsertData(
                table: "SEG_Programas",
                columns: new[] { "Id", "Codigo", "EstadoActivo", "FechaCreado", "FechaModificado", "Nombre", "UsuarioCreadorId", "UsuarioModificadorId" },
                values: new object[,]
                {
                    { 1, "GRUPOS", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2187), null, "MAESTRO DE GRUPOS", 1, null },
                    { 2, "CONTRARREFERENCIA", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2189), null, "CONTRARREFERENCIA A PACIENTES", 1, null },
                    { 3, "MEDICOSSEDES", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2191), null, "MEDICOS POR SEDE", 1, null },
                    { 4, "REFERENCIA", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2192), null, "REFERENCIA A PACIENTES", 1, null },
                    { 5, "EMPRESAS", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2194), null, "EMPRESAS", 1, null },
                    { 6, "SEDES", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2195), null, "SEDES", 1, null },
                    { 7, "LISTAS", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2197), null, "MAESTRO DE LISTAS", 1, null },
                    { 8, "DATOSCONSTANTES", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2199), null, "MAESTRO DE DATOS CONSTANTES", 1, null },
                    { 9, "USUARIOSSEDESGRUPOS", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2200), null, "ASOCIACION DE USUARIOS SEDES GRUPOS", 1, null },
                    { 10, "PROGRAMAS", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2202), null, "MAESTRO DE PROGRAMAS", 1, null },
                    { 11, "USUARIOS", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2203), null, "MAESTRO DE USUARIOS", 1, null }
                });

            migrationBuilder.InsertData(
                table: "SEG_Permisos",
                columns: new[] { "Id", "AccionId", "Codigo", "EstadoActivo", "FechaCreado", "FechaModificado", "Nombre", "ProgramaId", "UsuarioCreadorId", "UsuarioModificadorId" },
                values: new object[,]
                {
                    { 1, 1, "GRUPOS.CONSULTAR", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2231), null, "CONSULTAR GRUPOS", 1, 1, null },
                    { 2, 2, "GRUPOS.CREAR", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2234), null, "CREAR GRUPOS", 1, 1, null },
                    { 3, 3, "GRUPOS.MODIFICAR", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2236), null, "MODIFICAR GRUPOS", 1, 1, null },
                    { 4, 4, "GRUPOS.ELIMINAR", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2238), null, "ELIMINAR GRUPOS", 1, 1, null },
                    { 5, 5, "GRUPOS.LISTAR", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2240), null, "LISTAR GRUPOS", 1, 1, null },
                    { 6, 1, "PROGRAMAS.CONSULTAR", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2242), null, "CONSULTAR PROGRAMAS", 10, 1, null },
                    { 7, 2, "PROGRAMAS.CREAR", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2244), null, "CREAR PROGRAMAS", 10, 1, null },
                    { 8, 3, "PROGRAMAS.MODIFICAR", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2246), null, "MODIFICAR PROGRAMAS", 10, 1, null },
                    { 9, 4, "PROGRAMAS.ELIMINAR", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2247), null, "ELIMINAR PROGRAMAS", 10, 1, null },
                    { 10, 5, "PROGRAMAS.LISTAR", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2249), null, "LISTAR PROGRAMAS", 10, 1, null },
                    { 11, 6, "USUARIOS.CREARCONSEDE", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2251), null, "CREAR USUARIOS CON SEDE", 11, 1, null },
                    { 12, 7, "USUARIOS.CREARCONGRUPO", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2253), null, "CREAR USUARIOS CON GRUPO", 11, 1, null },
                    { 13, 1, "USUARIOS.CONSULTAR", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2254), null, "CONSULTAR USUARIOS", 11, 1, null },
                    { 14, 5, "USUARIOS.LISTAR", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2256), null, "LISTAR USUARIOS", 11, 1, null },
                    { 15, 2, "USUARIOSSEDESGRUPOS.CREAR", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2258), null, "CREAR USUARIOS SEDES GRUPOS", 9, 1, null },
                    { 16, 3, "USUARIOSSEDESGRUPOS.MODIFICAR", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2260), null, "MODIFICAR USUARIOS SEDES GRUPOS", 9, 1, null },
                    { 17, 4, "USUARIOSSEDESGRUPOS.ELIMINAR", true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2261), null, "ELIMINAR USUARIOS SEDES GRUPOS", 9, 1, null }
                });

            migrationBuilder.InsertData(
                table: "SEG_UsuariosSedesGrupos",
                columns: new[] { "Id", "EstadoActivo", "FechaCreado", "FechaModificado", "GrupoId", "SedeId", "UsuarioCreadorId", "UsuarioId", "UsuarioModificadorId" },
                values: new object[] { 1, true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2381), null, 1, 1, 1, 1, null });

            migrationBuilder.InsertData(
                table: "SEG_GruposPermisos",
                columns: new[] { "Id", "EstadoActivo", "FechaCreado", "FechaModificado", "GrupoId", "PermisoId", "UsuarioCreadorId", "UsuarioModificadorId" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2330), null, 1, 1, 1, null },
                    { 2, true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2332), null, 1, 2, 1, null },
                    { 3, true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2333), null, 1, 3, 1, null },
                    { 4, true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2335), null, 1, 4, 1, null },
                    { 5, true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2337), null, 1, 5, 1, null },
                    { 6, true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2339), null, 1, 6, 1, null },
                    { 7, true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2340), null, 1, 7, 1, null },
                    { 8, true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2341), null, 1, 8, 1, null },
                    { 9, true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2343), null, 1, 9, 1, null },
                    { 10, true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2344), null, 1, 10, 1, null },
                    { 11, true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2345), null, 1, 11, 1, null },
                    { 12, true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2347), null, 1, 12, 1, null },
                    { 13, true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2348), null, 1, 13, 1, null },
                    { 14, true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2350), null, 1, 14, 1, null },
                    { 15, true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2351), null, 1, 15, 1, null },
                    { 16, true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2352), null, 1, 16, 1, null },
                    { 17, true, new DateTime(2026, 7, 10, 17, 54, 56, 234, DateTimeKind.Local).AddTicks(2354), null, 1, 17, 1, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SEG_Acciones_Codigo",
                table: "SEG_Acciones",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SEG_Acciones_UsuarioCreadorId",
                table: "SEG_Acciones",
                column: "UsuarioCreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_SEG_Acciones_UsuarioModificadorId",
                table: "SEG_Acciones",
                column: "UsuarioModificadorId");

            migrationBuilder.CreateIndex(
                name: "IX_SEG_ColaSolicitudes_Estado",
                table: "SEG_ColaSolicitudes",
                column: "Estado");

            migrationBuilder.CreateIndex(
                name: "IX_SEG_ColaSolicitudes_Tipo",
                table: "SEG_ColaSolicitudes",
                column: "Tipo");

            migrationBuilder.CreateIndex(
                name: "IX_SEG_Grupos_Codigo",
                table: "SEG_Grupos",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SEG_Grupos_UsuarioCreadorId",
                table: "SEG_Grupos",
                column: "UsuarioCreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_SEG_Grupos_UsuarioModificadorId",
                table: "SEG_Grupos",
                column: "UsuarioModificadorId");

            migrationBuilder.CreateIndex(
                name: "IX_SEG_GruposPermisos_GrupoId_PermisoId",
                table: "SEG_GruposPermisos",
                columns: new[] { "GrupoId", "PermisoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SEG_GruposPermisos_PermisoId",
                table: "SEG_GruposPermisos",
                column: "PermisoId");

            migrationBuilder.CreateIndex(
                name: "IX_SEG_GruposPermisos_UsuarioCreadorId",
                table: "SEG_GruposPermisos",
                column: "UsuarioCreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_SEG_GruposPermisos_UsuarioModificadorId",
                table: "SEG_GruposPermisos",
                column: "UsuarioModificadorId");

            migrationBuilder.CreateIndex(
                name: "IX_SEG_GruposProgramas_GrupoId_ProgramaId",
                table: "SEG_GruposProgramas",
                columns: new[] { "GrupoId", "ProgramaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SEG_GruposProgramas_ProgramaId",
                table: "SEG_GruposProgramas",
                column: "ProgramaId");

            migrationBuilder.CreateIndex(
                name: "IX_SEG_GruposProgramas_UsuarioCreadorId",
                table: "SEG_GruposProgramas",
                column: "UsuarioCreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_SEG_GruposProgramas_UsuarioModificadorId",
                table: "SEG_GruposProgramas",
                column: "UsuarioModificadorId");

            migrationBuilder.CreateIndex(
                name: "IX_SEG_Permisos_AccionId",
                table: "SEG_Permisos",
                column: "AccionId");

            migrationBuilder.CreateIndex(
                name: "IX_SEG_Permisos_Codigo",
                table: "SEG_Permisos",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SEG_Permisos_ProgramaId_AccionId",
                table: "SEG_Permisos",
                columns: new[] { "ProgramaId", "AccionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SEG_Permisos_UsuarioCreadorId",
                table: "SEG_Permisos",
                column: "UsuarioCreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_SEG_Permisos_UsuarioModificadorId",
                table: "SEG_Permisos",
                column: "UsuarioModificadorId");

            migrationBuilder.CreateIndex(
                name: "IX_SEG_Programas_Codigo",
                table: "SEG_Programas",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SEG_Programas_UsuarioCreadorId",
                table: "SEG_Programas",
                column: "UsuarioCreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_SEG_Programas_UsuarioModificadorId",
                table: "SEG_Programas",
                column: "UsuarioModificadorId");

            migrationBuilder.CreateIndex(
                name: "IX_SEG_Usuarios_Email",
                table: "SEG_Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SEG_Usuarios_GrupoDirectoId",
                table: "SEG_Usuarios",
                column: "GrupoDirectoId");

            migrationBuilder.CreateIndex(
                name: "IX_SEG_Usuarios_NombreUsuario",
                table: "SEG_Usuarios",
                column: "NombreUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SEG_Usuarios_TipoIdentificacionId_Identificacion",
                table: "SEG_Usuarios",
                columns: new[] { "TipoIdentificacionId", "Identificacion" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SEG_Usuarios_UsuarioCreadorId",
                table: "SEG_Usuarios",
                column: "UsuarioCreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_SEG_Usuarios_UsuarioModificadorId",
                table: "SEG_Usuarios",
                column: "UsuarioModificadorId");

            migrationBuilder.CreateIndex(
                name: "IX_SEG_UsuariosSedesGrupos_GrupoId",
                table: "SEG_UsuariosSedesGrupos",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_SEG_UsuariosSedesGrupos_UsuarioCreadorId",
                table: "SEG_UsuariosSedesGrupos",
                column: "UsuarioCreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_SEG_UsuariosSedesGrupos_UsuarioId_SedeId",
                table: "SEG_UsuariosSedesGrupos",
                columns: new[] { "UsuarioId", "SedeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SEG_UsuariosSedesGrupos_UsuarioModificadorId",
                table: "SEG_UsuariosSedesGrupos",
                column: "UsuarioModificadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_SEG_Acciones_SEG_Usuarios_UsuarioCreadorId",
                table: "SEG_Acciones",
                column: "UsuarioCreadorId",
                principalTable: "SEG_Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SEG_Acciones_SEG_Usuarios_UsuarioModificadorId",
                table: "SEG_Acciones",
                column: "UsuarioModificadorId",
                principalTable: "SEG_Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SEG_Grupos_SEG_Usuarios_UsuarioCreadorId",
                table: "SEG_Grupos",
                column: "UsuarioCreadorId",
                principalTable: "SEG_Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SEG_Grupos_SEG_Usuarios_UsuarioModificadorId",
                table: "SEG_Grupos",
                column: "UsuarioModificadorId",
                principalTable: "SEG_Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SEG_Grupos_SEG_Usuarios_UsuarioCreadorId",
                table: "SEG_Grupos");

            migrationBuilder.DropForeignKey(
                name: "FK_SEG_Grupos_SEG_Usuarios_UsuarioModificadorId",
                table: "SEG_Grupos");

            migrationBuilder.DropTable(
                name: "SEG_ColaSolicitudes");

            migrationBuilder.DropTable(
                name: "SEG_GruposPermisos");

            migrationBuilder.DropTable(
                name: "SEG_GruposProgramas");

            migrationBuilder.DropTable(
                name: "SEG_UsuariosSedesGrupos");

            migrationBuilder.DropTable(
                name: "SEG_Permisos");

            migrationBuilder.DropTable(
                name: "SEG_Acciones");

            migrationBuilder.DropTable(
                name: "SEG_Programas");

            migrationBuilder.DropTable(
                name: "SEG_Usuarios");

            migrationBuilder.DropTable(
                name: "SEG_Grupos");
        }
    }
}
