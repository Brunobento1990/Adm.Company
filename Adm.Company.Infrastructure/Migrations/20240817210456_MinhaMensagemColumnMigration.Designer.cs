﻿// <auto-generated />
using System;
using Adm.Company.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Adm.Company.Infrastructure.Migrations
{
    [DbContext(typeof(AdmCompanyContext))]
    [Migration("20240817210456_MinhaMensagemColumnMigration")]
    partial class MinhaMensagemColumnMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Adm.Company.Domain.Entities.Atendimento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("AtualizadoEm")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CriadoEm")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<Guid>("EmpresaId")
                        .HasColumnType("uuid");

                    b.Property<string>("MensagemCancelamento")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<long>("Numero")
                        .HasColumnType("bigint");

                    b.Property<string>("NumeroWhats")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Observacao")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid?>("UsuarioCancelamentoId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UsuarioId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("Numero");

                    b.HasIndex("NumeroWhats");

                    b.HasIndex("Status");

                    b.HasIndex("UsuarioCancelamentoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Atendimentos");
                });

            modelBuilder.Entity("Adm.Company.Domain.Entities.ConfiguracaoAtendimentoEmpresa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("AtualizadoEm")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CriadoEm")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<Guid>("EmpresaId")
                        .HasColumnType("uuid");

                    b.Property<long>("Numero")
                        .HasColumnType("bigint");

                    b.Property<string>("WhatsApp")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("Numero");

                    b.ToTable("ConfiguracaoAtendimentoEmpresa");
                });

            modelBuilder.Entity("Adm.Company.Domain.Entities.Empresa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("AtualizadoEm")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("character varying(14)");

                    b.Property<DateTime>("CriadoEm")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("NomeFantasia")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<long>("Numero")
                        .HasColumnType("bigint");

                    b.Property<string>("RazaoSocial")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.HasIndex("Cnpj")
                        .IsUnique();

                    b.HasIndex("Numero");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("Adm.Company.Domain.Entities.MensagemAtendimento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AtendimentoId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("AtualizadoEm")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CriadoEm")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Mensagem")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("character varying(5000)");

                    b.Property<bool>("MinhaMensagem")
                        .HasColumnType("boolean");

                    b.Property<long>("Numero")
                        .HasColumnType("bigint");

                    b.Property<string>("RemoteId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("TipoMensagem")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.HasIndex("AtendimentoId");

                    b.HasIndex("Numero");

                    b.ToTable("MensagemAtendimentos");
                });

            modelBuilder.Entity("Adm.Company.Domain.Entities.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("AtualizadoEm")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Bloqueado")
                        .HasColumnType("boolean");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.Property<DateTime>("CriadoEm")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid>("EmpresaId")
                        .HasColumnType("uuid");

                    b.Property<long>("Numero")
                        .HasColumnType("bigint");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("WhatsApp")
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)");

                    b.HasKey("Id");

                    b.HasIndex("Cpf");

                    b.HasIndex("Email");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("Numero");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Adm.Company.Domain.Entities.Atendimento", b =>
                {
                    b.HasOne("Adm.Company.Domain.Entities.Empresa", "Empresa")
                        .WithMany()
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Adm.Company.Domain.Entities.Usuario", "UsuarioCancelamento")
                        .WithMany()
                        .HasForeignKey("UsuarioCancelamentoId");

                    b.HasOne("Adm.Company.Domain.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Empresa");

                    b.Navigation("Usuario");

                    b.Navigation("UsuarioCancelamento");
                });

            modelBuilder.Entity("Adm.Company.Domain.Entities.ConfiguracaoAtendimentoEmpresa", b =>
                {
                    b.HasOne("Adm.Company.Domain.Entities.Empresa", "Empresa")
                        .WithMany()
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("Adm.Company.Domain.Entities.MensagemAtendimento", b =>
                {
                    b.HasOne("Adm.Company.Domain.Entities.Atendimento", "Atendimento")
                        .WithMany("Mensagens")
                        .HasForeignKey("AtendimentoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Atendimento");
                });

            modelBuilder.Entity("Adm.Company.Domain.Entities.Usuario", b =>
                {
                    b.HasOne("Adm.Company.Domain.Entities.Empresa", "Empresa")
                        .WithMany("Usuarios")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("Adm.Company.Domain.Entities.Atendimento", b =>
                {
                    b.Navigation("Mensagens");
                });

            modelBuilder.Entity("Adm.Company.Domain.Entities.Empresa", b =>
                {
                    b.Navigation("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
