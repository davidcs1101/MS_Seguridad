﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SEG.DataAccess;

#nullable disable

namespace SEG.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("SEG.Dominio.Entidades.SEG_Grupos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<bool>("EstadoActivo")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("FechaCreado")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("FechaModificado")
                        .HasColumnType("datetime");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<int>("UsuarioCreadorId")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioModificadorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Codigo")
                        .IsUnique();

                    b.HasIndex("UsuarioCreadorId");

                    b.HasIndex("UsuarioModificadorId");

                    b.ToTable("SEG_Grupos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Codigo = "ADMINISTRADORSISTEMA",
                            EstadoActivo = true,
                            FechaCreado = new DateTime(2024, 11, 19, 9, 32, 7, 916, DateTimeKind.Local).AddTicks(7636),
                            Nombre = "ADMINISTRADOR SISTEMA",
                            UsuarioCreadorId = 1
                        },
                        new
                        {
                            Id = 2,
                            Codigo = "ADMINISTRADOREMPRESA",
                            EstadoActivo = true,
                            FechaCreado = new DateTime(2024, 11, 19, 9, 32, 7, 916, DateTimeKind.Local).AddTicks(7639),
                            Nombre = "ADMINISTRADOR DE EMPRESA",
                            UsuarioCreadorId = 1
                        },
                        new
                        {
                            Id = 3,
                            Codigo = "ADMINISTRADORSEDE",
                            EstadoActivo = true,
                            FechaCreado = new DateTime(2024, 11, 19, 9, 32, 7, 916, DateTimeKind.Local).AddTicks(7640),
                            Nombre = "ADMINISTRADOR DE SEDE",
                            UsuarioCreadorId = 1
                        },
                        new
                        {
                            Id = 4,
                            Codigo = "CONTRARREFERENCIA",
                            EstadoActivo = true,
                            FechaCreado = new DateTime(2024, 11, 19, 9, 32, 7, 916, DateTimeKind.Local).AddTicks(7662),
                            Nombre = "USUARIOS DE CONTRARREFERENCIA",
                            UsuarioCreadorId = 1
                        },
                        new
                        {
                            Id = 5,
                            Codigo = "REFERENCIA",
                            EstadoActivo = true,
                            FechaCreado = new DateTime(2024, 11, 19, 9, 32, 7, 916, DateTimeKind.Local).AddTicks(7685),
                            Nombre = "USUARIOS DE REFERENCIA",
                            UsuarioCreadorId = 1
                        },
                        new
                        {
                            Id = 6,
                            Codigo = "REFERENCIAYCONTRARREFERENCIA",
                            EstadoActivo = true,
                            FechaCreado = new DateTime(2024, 11, 19, 9, 32, 7, 916, DateTimeKind.Local).AddTicks(7687),
                            Nombre = "USUARIOS DE REFERENCIA Y CONTRARREFERENCIA",
                            UsuarioCreadorId = 1
                        });
                });

            modelBuilder.Entity("SEG.Dominio.Entidades.SEG_GruposProgramas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("EstadoActivo")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("FechaCreado")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaModificado")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("GrupoId")
                        .HasColumnType("int");

                    b.Property<int>("ProgramaId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioCreadorId")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioModificadorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProgramaId");

                    b.HasIndex("UsuarioCreadorId");

                    b.HasIndex("UsuarioModificadorId");

                    b.HasIndex("GrupoId", "ProgramaId")
                        .IsUnique();

                    b.ToTable("SEG_GruposProgramas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EstadoActivo = true,
                            FechaCreado = new DateTime(2024, 11, 19, 9, 32, 7, 916, DateTimeKind.Local).AddTicks(7753),
                            GrupoId = 1,
                            ProgramaId = 1,
                            UsuarioCreadorId = 1
                        },
                        new
                        {
                            Id = 2,
                            EstadoActivo = true,
                            FechaCreado = new DateTime(2024, 11, 19, 9, 32, 7, 916, DateTimeKind.Local).AddTicks(7754),
                            GrupoId = 1,
                            ProgramaId = 2,
                            UsuarioCreadorId = 1
                        },
                        new
                        {
                            Id = 3,
                            EstadoActivo = true,
                            FechaCreado = new DateTime(2024, 11, 19, 9, 32, 7, 916, DateTimeKind.Local).AddTicks(7756),
                            GrupoId = 1,
                            ProgramaId = 3,
                            UsuarioCreadorId = 1
                        },
                        new
                        {
                            Id = 4,
                            EstadoActivo = true,
                            FechaCreado = new DateTime(2024, 11, 19, 9, 32, 7, 916, DateTimeKind.Local).AddTicks(7757),
                            GrupoId = 1,
                            ProgramaId = 4,
                            UsuarioCreadorId = 1
                        },
                        new
                        {
                            Id = 5,
                            EstadoActivo = true,
                            FechaCreado = new DateTime(2024, 11, 19, 9, 32, 7, 916, DateTimeKind.Local).AddTicks(7805),
                            GrupoId = 1,
                            ProgramaId = 5,
                            UsuarioCreadorId = 1
                        },
                        new
                        {
                            Id = 6,
                            EstadoActivo = true,
                            FechaCreado = new DateTime(2024, 11, 19, 9, 32, 7, 916, DateTimeKind.Local).AddTicks(7807),
                            GrupoId = 1,
                            ProgramaId = 6,
                            UsuarioCreadorId = 1
                        },
                        new
                        {
                            Id = 7,
                            EstadoActivo = true,
                            FechaCreado = new DateTime(2024, 11, 19, 9, 32, 7, 916, DateTimeKind.Local).AddTicks(7808),
                            GrupoId = 1,
                            ProgramaId = 7,
                            UsuarioCreadorId = 1
                        },
                        new
                        {
                            Id = 8,
                            EstadoActivo = true,
                            FechaCreado = new DateTime(2024, 11, 19, 9, 32, 7, 916, DateTimeKind.Local).AddTicks(7809),
                            GrupoId = 1,
                            ProgramaId = 8,
                            UsuarioCreadorId = 1
                        });
                });

            modelBuilder.Entity("SEG.Dominio.Entidades.SEG_Programas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<bool>("EstadoActivo")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("FechaCreado")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("FechaModificado")
                        .HasColumnType("datetime");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<int>("UsuarioCreadorId")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioModificadorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Codigo")
                        .IsUnique();

                    b.HasIndex("UsuarioCreadorId");

                    b.HasIndex("UsuarioModificadorId");

                    b.ToTable("SEG_Programas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Codigo = "ASOCIACIONUSUARIOS",
                            EstadoActivo = true,
                            FechaCreado = new DateTime(2024, 11, 19, 9, 32, 7, 916, DateTimeKind.Local).AddTicks(7715),
                            Nombre = "ASOCIACION DE USUARIOS DE APLICACION",
                            UsuarioCreadorId = 1
                        },
                        new
                        {
                            Id = 2,
                            Codigo = "CONTRARREFERENCIA",
                            EstadoActivo = true,
                            FechaCreado = new DateTime(2024, 11, 19, 9, 32, 7, 916, DateTimeKind.Local).AddTicks(7717),
                            Nombre = "CONTRARREFERENCIA A PACIENTES",
                            UsuarioCreadorId = 1
                        },
                        new
                        {
                            Id = 3,
                            Codigo = "MEDICOSSEDES",
                            EstadoActivo = true,
                            FechaCreado = new DateTime(2024, 11, 19, 9, 32, 7, 916, DateTimeKind.Local).AddTicks(7719),
                            Nombre = "MEDICOS POR SEDE",
                            UsuarioCreadorId = 1
                        },
                        new
                        {
                            Id = 4,
                            Codigo = "REFERENCIA",
                            EstadoActivo = true,
                            FechaCreado = new DateTime(2024, 11, 19, 9, 32, 7, 916, DateTimeKind.Local).AddTicks(7720),
                            Nombre = "REFERENCIA A PACIENTES",
                            UsuarioCreadorId = 1
                        },
                        new
                        {
                            Id = 5,
                            Codigo = "EMPRESAS",
                            EstadoActivo = true,
                            FechaCreado = new DateTime(2024, 11, 19, 9, 32, 7, 916, DateTimeKind.Local).AddTicks(7722),
                            Nombre = "EMPRESAS",
                            UsuarioCreadorId = 1
                        },
                        new
                        {
                            Id = 6,
                            Codigo = "SEDES",
                            EstadoActivo = true,
                            FechaCreado = new DateTime(2024, 11, 19, 9, 32, 7, 916, DateTimeKind.Local).AddTicks(7723),
                            Nombre = "SEDES",
                            UsuarioCreadorId = 1
                        },
                        new
                        {
                            Id = 7,
                            Codigo = "LISTAS",
                            EstadoActivo = true,
                            FechaCreado = new DateTime(2024, 11, 19, 9, 32, 7, 916, DateTimeKind.Local).AddTicks(7725),
                            Nombre = "MAESTRO DE LISTAS",
                            UsuarioCreadorId = 1
                        },
                        new
                        {
                            Id = 8,
                            Codigo = "PROGRAMAS",
                            EstadoActivo = true,
                            FechaCreado = new DateTime(2024, 11, 19, 9, 32, 7, 916, DateTimeKind.Local).AddTicks(7726),
                            Nombre = "MAESTRO DE PROGRAMAS",
                            UsuarioCreadorId = 1
                        });
                });

            modelBuilder.Entity("SEG.Dominio.Entidades.SEG_Usuarios", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido1")
                        .IsRequired()
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Apellido2")
                        .HasColumnType("varchar(25)");

                    b.Property<bool>("CambiarClave")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Clave")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<bool>("EstadoActivo")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("FechaCreado")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaModificado")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Identificacion")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Nombre1")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Nombre2")
                        .HasColumnType("varchar(25)");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasColumnType("varchar(60)");

                    b.Property<int>("TipoIdentificacionId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioCreadorId")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioModificadorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("NombreUsuario")
                        .IsUnique();

                    b.HasIndex("UsuarioCreadorId");

                    b.HasIndex("UsuarioModificadorId");

                    b.HasIndex("TipoIdentificacionId", "Identificacion")
                        .IsUnique();

                    b.ToTable("SEG_Usuarios");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Apellido1 = "SISTEMA",
                            Apellido2 = "",
                            CambiarClave = true,
                            Clave = "1feTCdMwhKKkOSWaM5+yXEI0ZrBPlq9pbnB4k4+JRUU=",
                            Email = "CORREO@GMAIL.COM",
                            EstadoActivo = true,
                            FechaCreado = new DateTime(2024, 11, 19, 9, 32, 7, 916, DateTimeKind.Local).AddTicks(7451),
                            Identificacion = "ADMINISTRADOR",
                            Nombre1 = "ADMINISTRADOR",
                            Nombre2 = "",
                            NombreUsuario = "ADMINISTRADOR",
                            TipoIdentificacionId = 1,
                            UsuarioCreadorId = 1
                        });
                });

            modelBuilder.Entity("SEG.Dominio.Entidades.SEG_UsuariosSedesGrupos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("EstadoActivo")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("FechaCreado")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaModificado")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("GrupoId")
                        .HasColumnType("int");

                    b.Property<int>("SedeId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioCreadorId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioModificadorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GrupoId");

                    b.HasIndex("UsuarioCreadorId");

                    b.HasIndex("UsuarioModificadorId");

                    b.HasIndex("UsuarioId", "SedeId")
                        .IsUnique();

                    b.ToTable("SEG_UsuariosSedesGrupos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EstadoActivo = true,
                            FechaCreado = new DateTime(2024, 11, 19, 9, 32, 7, 916, DateTimeKind.Local).AddTicks(7834),
                            GrupoId = 1,
                            SedeId = 1,
                            UsuarioCreadorId = 1,
                            UsuarioId = 1
                        });
                });

            modelBuilder.Entity("SEG.Dominio.Entidades.SEG_Grupos", b =>
                {
                    b.HasOne("SEG.Dominio.Entidades.SEG_Usuarios", "UsuarioCreador")
                        .WithMany()
                        .HasForeignKey("UsuarioCreadorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SEG.Dominio.Entidades.SEG_Usuarios", "UsuarioModificador")
                        .WithMany()
                        .HasForeignKey("UsuarioModificadorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("UsuarioCreador");

                    b.Navigation("UsuarioModificador");
                });

            modelBuilder.Entity("SEG.Dominio.Entidades.SEG_GruposProgramas", b =>
                {
                    b.HasOne("SEG.Dominio.Entidades.SEG_Grupos", "Grupo")
                        .WithMany("GruposProgramas")
                        .HasForeignKey("GrupoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SEG.Dominio.Entidades.SEG_Programas", "Programa")
                        .WithMany("GruposProgramas")
                        .HasForeignKey("ProgramaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SEG.Dominio.Entidades.SEG_Usuarios", "UsuarioCreador")
                        .WithMany()
                        .HasForeignKey("UsuarioCreadorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SEG.Dominio.Entidades.SEG_Usuarios", "UsuarioModificador")
                        .WithMany()
                        .HasForeignKey("UsuarioModificadorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Grupo");

                    b.Navigation("Programa");

                    b.Navigation("UsuarioCreador");

                    b.Navigation("UsuarioModificador");
                });

            modelBuilder.Entity("SEG.Dominio.Entidades.SEG_Programas", b =>
                {
                    b.HasOne("SEG.Dominio.Entidades.SEG_Usuarios", "UsuarioCreador")
                        .WithMany()
                        .HasForeignKey("UsuarioCreadorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SEG.Dominio.Entidades.SEG_Usuarios", "UsuarioModificador")
                        .WithMany()
                        .HasForeignKey("UsuarioModificadorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("UsuarioCreador");

                    b.Navigation("UsuarioModificador");
                });

            modelBuilder.Entity("SEG.Dominio.Entidades.SEG_Usuarios", b =>
                {
                    b.HasOne("SEG.Dominio.Entidades.SEG_Usuarios", "UsuarioCreador")
                        .WithMany()
                        .HasForeignKey("UsuarioCreadorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SEG.Dominio.Entidades.SEG_Usuarios", "UsuarioModificador")
                        .WithMany()
                        .HasForeignKey("UsuarioModificadorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("UsuarioCreador");

                    b.Navigation("UsuarioModificador");
                });

            modelBuilder.Entity("SEG.Dominio.Entidades.SEG_UsuariosSedesGrupos", b =>
                {
                    b.HasOne("SEG.Dominio.Entidades.SEG_Grupos", "Grupo")
                        .WithMany("UsuariosSedes")
                        .HasForeignKey("GrupoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SEG.Dominio.Entidades.SEG_Usuarios", "UsuarioCreador")
                        .WithMany()
                        .HasForeignKey("UsuarioCreadorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SEG.Dominio.Entidades.SEG_Usuarios", "Usuario")
                        .WithMany("UsuariosSedes")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SEG.Dominio.Entidades.SEG_Usuarios", "UsuarioModificador")
                        .WithMany()
                        .HasForeignKey("UsuarioModificadorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Grupo");

                    b.Navigation("Usuario");

                    b.Navigation("UsuarioCreador");

                    b.Navigation("UsuarioModificador");
                });

            modelBuilder.Entity("SEG.Dominio.Entidades.SEG_Grupos", b =>
                {
                    b.Navigation("GruposProgramas");

                    b.Navigation("UsuariosSedes");
                });

            modelBuilder.Entity("SEG.Dominio.Entidades.SEG_Programas", b =>
                {
                    b.Navigation("GruposProgramas");
                });

            modelBuilder.Entity("SEG.Dominio.Entidades.SEG_Usuarios", b =>
                {
                    b.Navigation("UsuariosSedes");
                });
#pragma warning restore 612, 618
        }
    }
}
