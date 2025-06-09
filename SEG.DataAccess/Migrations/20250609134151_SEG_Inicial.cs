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
                    Tipo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Payload = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Reintentos = table.Column<int>(type: "int", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaUltimoIntento = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ErrorMensaje = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEG_ColaSolicitudes", x => x.Id);
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
                    table.ForeignKey(
                        name: "FK_SEG_Grupos_SEG_Usuarios_UsuarioCreadorId",
                        column: x => x.UsuarioCreadorId,
                        principalTable: "SEG_Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SEG_Grupos_SEG_Usuarios_UsuarioModificadorId",
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

            migrationBuilder.InsertData(
                table: "SEG_Usuarios",
                columns: new[] { "Id", "Apellido1", "Apellido2", "CambiarClave", "Clave", "Email", "EstadoActivo", "FechaCreado", "FechaModificado", "Identificacion", "Nombre1", "Nombre2", "NombreUsuario", "TipoIdentificacionId", "UsuarioCreadorId", "UsuarioModificadorId" },
                values: new object[] { 1, "SISTEMA", "", true, "1feTCdMwhKKkOSWaM5+yXEI0ZrBPlq9pbnB4k4+JRUU=", "CORREO@GMAIL.COM", true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1409), null, "ADMINISTRADOR", "ADMINISTRADOR", "", "ADMINISTRADOR", 1, 1, null });

            migrationBuilder.InsertData(
                table: "SEG_Grupos",
                columns: new[] { "Id", "Codigo", "EstadoActivo", "FechaCreado", "FechaModificado", "Nombre", "UsuarioCreadorId", "UsuarioModificadorId" },
                values: new object[,]
                {
                    { 1, "ADMINISTRADORSISTEMA", true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1611), null, "ADMINISTRADOR SISTEMA", 1, null },
                    { 2, "ADMINISTRADOREMPRESA", true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1613), null, "ADMINISTRADOR DE EMPRESA", 1, null },
                    { 3, "ADMINISTRADORSEDE", true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1615), null, "ADMINISTRADOR DE SEDE", 1, null },
                    { 4, "CONTRARREFERENCIA", true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1617), null, "USUARIOS DE CONTRARREFERENCIA", 1, null },
                    { 5, "REFERENCIA", true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1619), null, "USUARIOS DE REFERENCIA", 1, null },
                    { 6, "REFERENCIAYCONTRARREFERENCIA", true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1621), null, "USUARIOS DE REFERENCIA Y CONTRARREFERENCIA", 1, null }
                });

            migrationBuilder.InsertData(
                table: "SEG_Programas",
                columns: new[] { "Id", "Codigo", "EstadoActivo", "FechaCreado", "FechaModificado", "Nombre", "UsuarioCreadorId", "UsuarioModificadorId" },
                values: new object[,]
                {
                    { 1, "USUARIOSSEDESGRUPOS", true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1653), null, "ASOCIACION DE USUARIOS SEDES GRUPOS", 1, null },
                    { 2, "CONTRARREFERENCIA", true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1655), null, "CONTRARREFERENCIA A PACIENTES", 1, null },
                    { 3, "MEDICOSSEDES", true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1657), null, "MEDICOS POR SEDE", 1, null },
                    { 4, "REFERENCIA", true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1659), null, "REFERENCIA A PACIENTES", 1, null },
                    { 5, "EMPRESAS", true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1660), null, "EMPRESAS", 1, null },
                    { 6, "SEDES", true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1662), null, "SEDES", 1, null },
                    { 7, "LISTAS", true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1663), null, "MAESTRO DE LISTAS", 1, null },
                    { 8, "DATOSCONSTANTES", true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1698), null, "MAESTRO DE DATOS CONSTANTES", 1, null },
                    { 9, "GRUPOS", true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1700), null, "MAESTRO DE GRUPOS", 1, null },
                    { 10, "PROGRAMAS", true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1702), null, "MAESTRO DE PROGRAMAS", 1, null },
                    { 11, "GRUPOSPROGRAMAS", true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1703), null, "MAESTRO DE PROGRAMAS POR GRUPO", 1, null },
                    { 12, "USUARIOS", true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1705), null, "MAESTRO DE USUARIOS", 1, null }
                });

            migrationBuilder.InsertData(
                table: "SEG_GruposProgramas",
                columns: new[] { "Id", "EstadoActivo", "FechaCreado", "FechaModificado", "GrupoId", "ProgramaId", "UsuarioCreadorId", "UsuarioModificadorId" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1748), null, 1, 1, 1, null },
                    { 2, true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1750), null, 1, 2, 1, null },
                    { 3, true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1751), null, 1, 3, 1, null },
                    { 4, true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1753), null, 1, 4, 1, null },
                    { 5, true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1754), null, 1, 5, 1, null },
                    { 6, true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1756), null, 1, 6, 1, null },
                    { 7, true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1758), null, 1, 7, 1, null },
                    { 8, true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1760), null, 1, 8, 1, null },
                    { 9, true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1761), null, 1, 9, 1, null },
                    { 10, true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1763), null, 1, 10, 1, null },
                    { 11, true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1764), null, 1, 11, 1, null },
                    { 12, true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1765), null, 1, 12, 1, null }
                });

            migrationBuilder.InsertData(
                table: "SEG_UsuariosSedesGrupos",
                columns: new[] { "Id", "EstadoActivo", "FechaCreado", "FechaModificado", "GrupoId", "SedeId", "UsuarioCreadorId", "UsuarioId", "UsuarioModificadorId" },
                values: new object[] { 1, true, new DateTime(2025, 6, 9, 8, 41, 51, 109, DateTimeKind.Local).AddTicks(1794), null, 1, 1, 1, 1, null });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SEG_ColaSolicitudes");

            migrationBuilder.DropTable(
                name: "SEG_GruposProgramas");

            migrationBuilder.DropTable(
                name: "SEG_UsuariosSedesGrupos");

            migrationBuilder.DropTable(
                name: "SEG_Programas");

            migrationBuilder.DropTable(
                name: "SEG_Grupos");

            migrationBuilder.DropTable(
                name: "SEG_Usuarios");
        }
    }
}
