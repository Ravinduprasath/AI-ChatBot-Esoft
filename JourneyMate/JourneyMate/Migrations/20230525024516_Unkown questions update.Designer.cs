﻿// <auto-generated />
using System;
using JourneyMate.DbLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JourneyMate.Migrations
{
    [DbContext(typeof(ChatBotContext))]
    [Migration("20230525024516_Unkown questions update")]
    partial class Unkownquestionsupdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("JourneyMate.DbLayer.Domains.AnswerType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("AnswerTypes");
                });

            modelBuilder.Entity("JourneyMate.DbLayer.Domains.BotAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AnswerTypeId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Image")
                        .HasColumnType("longblob");

                    b.Property<bool>("IsButton")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsHtml")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AnswerTypeId");

                    b.HasIndex("QuestionId");

                    b.ToTable("BotAnswers");
                });

            modelBuilder.Entity("JourneyMate.DbLayer.Domains.Intent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Intents");
                });

            modelBuilder.Entity("JourneyMate.DbLayer.Domains.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("IntentId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("IntentId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("JourneyMate.DbLayer.Domains.UnkownQuestions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Answer")
                        .HasColumnType("longtext");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("UnkownQuestions");
                });

            modelBuilder.Entity("JourneyMate.DbLayer.Domains.UserIntent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("IntentId")
                        .HasColumnType("int");

                    b.Property<string>("Keyword")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("IntentId");

                    b.ToTable("UserIntents");
                });

            modelBuilder.Entity("JourneyMate.DbLayer.Domains.BotAnswer", b =>
                {
                    b.HasOne("JourneyMate.DbLayer.Domains.AnswerType", "AnswerType")
                        .WithMany("BotAnswers")
                        .HasForeignKey("AnswerTypeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("JourneyMate.DbLayer.Domains.Question", "Question")
                        .WithMany("BotAnswers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnswerType");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("JourneyMate.DbLayer.Domains.Question", b =>
                {
                    b.HasOne("JourneyMate.DbLayer.Domains.Intent", "Intent")
                        .WithMany("Questions")
                        .HasForeignKey("IntentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Intent");
                });

            modelBuilder.Entity("JourneyMate.DbLayer.Domains.UserIntent", b =>
                {
                    b.HasOne("JourneyMate.DbLayer.Domains.Intent", "Intent")
                        .WithMany("UserIntents")
                        .HasForeignKey("IntentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Intent");
                });

            modelBuilder.Entity("JourneyMate.DbLayer.Domains.AnswerType", b =>
                {
                    b.Navigation("BotAnswers");
                });

            modelBuilder.Entity("JourneyMate.DbLayer.Domains.Intent", b =>
                {
                    b.Navigation("Questions");

                    b.Navigation("UserIntents");
                });

            modelBuilder.Entity("JourneyMate.DbLayer.Domains.Question", b =>
                {
                    b.Navigation("BotAnswers");
                });
#pragma warning restore 612, 618
        }
    }
}
