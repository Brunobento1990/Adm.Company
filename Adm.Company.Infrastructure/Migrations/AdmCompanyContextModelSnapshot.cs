﻿// <auto-generated />
using System;
using Adm.Company.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Adm.Company.Infrastructure.Migrations
{
    [DbContext(typeof(AdmCompanyContext))]
    partial class AdmCompanyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

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
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Numero"));

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
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Numero"));

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

            modelBuilder.Entity("Adm.Company.Domain.Entities.Usuario", b =>
                {
                    b.HasOne("Adm.Company.Domain.Entities.Empresa", "Empresa")
                        .WithMany("Usuarios")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("Adm.Company.Domain.Entities.Empresa", b =>
                {
                    b.Navigation("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
