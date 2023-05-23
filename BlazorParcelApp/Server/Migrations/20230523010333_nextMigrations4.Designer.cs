﻿// <auto-generated />
using System;
using BlazorParcelApp.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BlazorParcelApp.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230523010333_nextMigrations4")]
    partial class nextMigrations4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BlazorParcelApp.Shared.Locker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Lockers");
                });

            modelBuilder.Entity("BlazorParcelApp.Shared.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ParcelId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParcelId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("BlazorParcelApp.Shared.Parcel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CurrentState")
                        .HasColumnType("integer");

                    b.Property<int?>("DestId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ReceiverId")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<int?>("SenderId")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<int?>("SrcId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DestId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.HasIndex("SrcId");

                    b.ToTable("Parcels");
                });

            modelBuilder.Entity("BlazorParcelApp.Shared.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BlazorParcelApp.Shared.Notification", b =>
                {
                    b.HasOne("BlazorParcelApp.Shared.Parcel", "Parcel")
                        .WithMany("Notifications")
                        .HasForeignKey("ParcelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parcel");
                });

            modelBuilder.Entity("BlazorParcelApp.Shared.Parcel", b =>
                {
                    b.HasOne("BlazorParcelApp.Shared.Locker", "DestLocker")
                        .WithMany("ParcelsDest")
                        .HasForeignKey("DestId");

                    b.HasOne("BlazorParcelApp.Shared.User", "Receiver")
                        .WithMany("ReceivedParcels")
                        .HasForeignKey("ReceiverId")
                        .IsRequired();

                    b.HasOne("BlazorParcelApp.Shared.User", "Sender")
                        .WithMany("SentParcels")
                        .HasForeignKey("SenderId")
                        .IsRequired();

                    b.HasOne("BlazorParcelApp.Shared.Locker", "SrcLocker")
                        .WithMany("ParcelsSrc")
                        .HasForeignKey("SrcId");

                    b.Navigation("DestLocker");

                    b.Navigation("Receiver");

                    b.Navigation("Sender");

                    b.Navigation("SrcLocker");
                });

            modelBuilder.Entity("BlazorParcelApp.Shared.Locker", b =>
                {
                    b.Navigation("ParcelsDest");

                    b.Navigation("ParcelsSrc");
                });

            modelBuilder.Entity("BlazorParcelApp.Shared.Parcel", b =>
                {
                    b.Navigation("Notifications");
                });

            modelBuilder.Entity("BlazorParcelApp.Shared.User", b =>
                {
                    b.Navigation("ReceivedParcels");

                    b.Navigation("SentParcels");
                });
#pragma warning restore 612, 618
        }
    }
}