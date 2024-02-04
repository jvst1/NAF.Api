﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NAF.Infra.Data.Context;

#nullable disable

namespace NAF.Infra.Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("NAF.Domain.Entities.Area", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<Guid>("Codigo")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DtAlteracao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DtInclusao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Area");
                });

            modelBuilder.Entity("NAF.Domain.Entities.Chamado", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<Guid>("Codigo")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("CodigoOperador")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CodigoServico")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CodigoUsuario")
                        .HasColumnType("char(36)");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DtAlteracao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DtInclusao")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("ServicoId")
                        .HasColumnType("bigint");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<long?>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ServicoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Chamado");
                });

            modelBuilder.Entity("NAF.Domain.Entities.ChamadoComentario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("ChamadoId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("Codigo")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CodigoChamado")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CodigoUsuario")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DtAlteracao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DtInclusao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Mensagem")
                        .HasColumnType("longtext");

                    b.Property<long?>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ChamadoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("ChamadoComentario");
                });

            modelBuilder.Entity("NAF.Domain.Entities.ChamadoDocumento", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<byte[]>("Arquivo")
                        .HasColumnType("longblob");

                    b.Property<long?>("ChamadoId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("Codigo")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CodigoChamado")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CodigoUsuario")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DtInclusao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NomeArquivo")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<long?>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ChamadoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("ChamadoDocumento");
                });

            modelBuilder.Entity("NAF.Domain.Entities.ChamadoHistorico", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("CampoAlterado")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<long?>("ChamadoId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("Codigo")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CodigoChamado")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CodigoUsuario")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DtInclusao")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("TipoAlteracao")
                        .HasColumnType("int");

                    b.Property<long?>("UsuarioId")
                        .HasColumnType("bigint");

                    b.Property<string>("ValorAntigo")
                        .HasColumnType("longtext");

                    b.Property<string>("ValorNovo")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ChamadoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("ChamadoHistorico");
                });

            modelBuilder.Entity("NAF.Domain.Entities.PerguntaFrequente", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<Guid>("Codigo")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DtAlteracao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DtInclusao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Pergunta")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Resposta")
                        .HasMaxLength(2000)
                        .HasColumnType("varchar(2000)");

                    b.HasKey("Id");

                    b.HasIndex("DtAlteracao");

                    b.ToTable("PerguntaFrequente");
                });

            modelBuilder.Entity("NAF.Domain.Entities.Servico", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("AreaId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("Codigo")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CodigoArea")
                        .HasColumnType("char(36)");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DtAlteracao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DtInclusao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.ToTable("Servico");
                });

            modelBuilder.Entity("NAF.Domain.Entities.Usuario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<Guid>("Codigo")
                        .HasColumnType("char(36)");

                    b.Property<string>("DocumentoFederal")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("DtInclusao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Identificador")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("IdentityUserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Nome")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.Property<string>("TelefoneCelular")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("TipoPerfil")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("IdentityUserId");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NAF.Domain.Entities.Chamado", b =>
                {
                    b.HasOne("NAF.Domain.Entities.Servico", "Servico")
                        .WithMany()
                        .HasForeignKey("ServicoId");

                    b.HasOne("NAF.Domain.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Servico");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("NAF.Domain.Entities.ChamadoComentario", b =>
                {
                    b.HasOne("NAF.Domain.Entities.Chamado", "Chamado")
                        .WithMany()
                        .HasForeignKey("ChamadoId");

                    b.HasOne("NAF.Domain.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Chamado");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("NAF.Domain.Entities.ChamadoDocumento", b =>
                {
                    b.HasOne("NAF.Domain.Entities.Chamado", "Chamado")
                        .WithMany()
                        .HasForeignKey("ChamadoId");

                    b.HasOne("NAF.Domain.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Chamado");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("NAF.Domain.Entities.ChamadoHistorico", b =>
                {
                    b.HasOne("NAF.Domain.Entities.Chamado", "Chamado")
                        .WithMany()
                        .HasForeignKey("ChamadoId");

                    b.HasOne("NAF.Domain.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Chamado");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("NAF.Domain.Entities.Servico", b =>
                {
                    b.HasOne("NAF.Domain.Entities.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId");

                    b.Navigation("Area");
                });

            modelBuilder.Entity("NAF.Domain.Entities.Usuario", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "IdentityUser")
                        .WithMany()
                        .HasForeignKey("IdentityUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdentityUser");
                });
#pragma warning restore 612, 618
        }
    }
}
