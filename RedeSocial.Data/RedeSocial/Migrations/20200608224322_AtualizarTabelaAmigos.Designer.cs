﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RedeSocial.Data.RedeSocial.Context;

namespace RedeSocial.Data.RedeSocial.Migrations
{
    [DbContext(typeof(RedeSocialContext))]
    [Migration("20200608224322_AtualizarTabelaAmigos")]
    partial class AtualizarTabelaAmigos
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RedeSocial.Model.Entity.AmigosModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataInicioAmizade")
                        .HasColumnType("datetime2");

                    b.Property<int>("StatusAmizade")
                        .HasColumnType("int");

                    b.Property<string>("UserId1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId2")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AmigosModel");
                });

            modelBuilder.Entity("RedeSocial.Model.Entity.CommentPostModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdPostModelId")
                        .HasColumnType("int");

                    b.Property<string>("IdentityUser")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdPostModelId");

                    b.ToTable("CommentPostModel");
                });

            modelBuilder.Entity("RedeSocial.Model.Entity.LikesPostModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("IdPostModelId")
                        .HasColumnType("int");

                    b.Property<string>("IdentityUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MyProperty")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdPostModelId");

                    b.ToTable("LikesPostModel");
                });

            modelBuilder.Entity("RedeSocial.Model.Entity.PostModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataPostagem")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdentityUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Privacidade")
                        .HasColumnType("int");

                    b.Property<string>("Texto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UriImage")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PostModel");
                });

            modelBuilder.Entity("RedeSocial.Model.Entity.UsuarioModel", b =>
                {
                    b.Property<string>("IdentityUser")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("Cpf")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("FotoPerfil")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sobrenome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdentityUser");

                    b.ToTable("UsuarioModel");
                });

            modelBuilder.Entity("RedeSocial.Model.Entity.CommentPostModel", b =>
                {
                    b.HasOne("RedeSocial.Model.Entity.PostModel", "IdPostModel")
                        .WithMany()
                        .HasForeignKey("IdPostModelId");
                });

            modelBuilder.Entity("RedeSocial.Model.Entity.LikesPostModel", b =>
                {
                    b.HasOne("RedeSocial.Model.Entity.PostModel", "IdPostModel")
                        .WithMany()
                        .HasForeignKey("IdPostModelId");
                });
#pragma warning restore 612, 618
        }
    }
}
