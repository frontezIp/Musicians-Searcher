﻿// <auto-generated />
using System;
using Chat.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Chat.DataAccess.Migrations
{
    [DbContext(typeof(ChatContext))]
    [Migration("20230909130618_AddDefaultValueSqlToLastSentMessageAt")]
    partial class AddDefaultValueSqlToLastSentMessageAt
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Chat.DataAccess.Models.ChatParticipant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChatRoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChatRoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<Guid>("MessengerUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("Id");

                    b.HasIndex("ChatRoleId");

                    b.HasIndex("ChatRoomId");

                    b.HasIndex("MessengerUserId", "ChatRoomId")
                        .IsUnique();

                    b.ToTable("ChatParticipants");
                });

            modelBuilder.Entity("Chat.DataAccess.Models.ChatRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ChatRoles");
                });

            modelBuilder.Entity("Chat.DataAccess.Models.ChatRoom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastSentMessageAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("MembersNumber")
                        .HasColumnType("int");

                    b.Property<int>("MessagesCount")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("ChatRooms");
                });

            modelBuilder.Entity("Chat.DataAccess.Models.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChatRoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<Guid>("MessengerUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("Id");

                    b.HasIndex("ChatRoomId");

                    b.HasIndex("MessengerUserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Chat.DataAccess.Models.MessengerUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("MessangerUsers");
                });

            modelBuilder.Entity("Chat.DataAccess.Models.ChatParticipant", b =>
                {
                    b.HasOne("Chat.DataAccess.Models.ChatRole", "ChatRole")
                        .WithMany()
                        .HasForeignKey("ChatRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Chat.DataAccess.Models.ChatRoom", "ChatRoom")
                        .WithMany("ChatParticipants")
                        .HasForeignKey("ChatRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Chat.DataAccess.Models.MessengerUser", "MessengerUser")
                        .WithMany("ParticipatedChats")
                        .HasForeignKey("MessengerUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ChatRole");

                    b.Navigation("ChatRoom");

                    b.Navigation("MessengerUser");
                });

            modelBuilder.Entity("Chat.DataAccess.Models.ChatRoom", b =>
                {
                    b.HasOne("Chat.DataAccess.Models.MessengerUser", "Creator")
                        .WithMany("CreatedChatRooms")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Chat.DataAccess.Models.Message", b =>
                {
                    b.HasOne("Chat.DataAccess.Models.ChatRoom", "ChatRoom")
                        .WithMany("Messages")
                        .HasForeignKey("ChatRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Chat.DataAccess.Models.MessengerUser", "MessengerUser")
                        .WithMany("Messages")
                        .HasForeignKey("MessengerUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ChatRoom");

                    b.Navigation("MessengerUser");
                });

            modelBuilder.Entity("Chat.DataAccess.Models.ChatRoom", b =>
                {
                    b.Navigation("ChatParticipants");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Chat.DataAccess.Models.MessengerUser", b =>
                {
                    b.Navigation("CreatedChatRooms");

                    b.Navigation("Messages");

                    b.Navigation("ParticipatedChats");
                });
#pragma warning restore 612, 618
        }
    }
}
