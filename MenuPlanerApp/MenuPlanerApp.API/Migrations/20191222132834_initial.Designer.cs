﻿// <auto-generated />
using System;
using MenuPlanerApp.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MenuPlanerApp.API.Migrations
{
    [DbContext(typeof(MenuPlanerAppApiContext))]
    [Migration("20191222132834_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MenuPlanerApp.API.Model.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CompatibleForCeliac")
                        .HasColumnType("bit");

                    b.Property<bool>("CompatibleForFructose")
                        .HasColumnType("bit");

                    b.Property<bool>("CompatibleForHistamin")
                        .HasColumnType("bit");

                    b.Property<bool>("CompatibleForLactose")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<string>("ReferenceUnit")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.HasKey("Id");

                    b.ToTable("Ingredient");
                });

            modelBuilder.Entity("MenuPlanerApp.API.Model.IngredientWithAmount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("IngredientId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.HasIndex("RecipeId");

                    b.ToTable("IngredientWithAmount");
                });

            modelBuilder.Entity("MenuPlanerApp.API.Model.MenuPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("StartDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("MenuPlan");
                });

            modelBuilder.Entity("MenuPlanerApp.API.Model.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("DirectionPictures")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsFavorite")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.HasKey("Id");

                    b.ToTable("Recipe");
                });

            modelBuilder.Entity("MenuPlanerApp.API.Model.RecipeWithAmount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DayOfWeek")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("MealDayTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("MenuPlanId")
                        .HasColumnType("int");

                    b.Property<int>("NumbersOfMeals")
                        .HasColumnType("int");

                    b.Property<int?>("RecipeId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MenuPlanId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeWithAmount");
                });

            modelBuilder.Entity("MenuPlanerApp.API.Model.UserOptions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("WantsUserToSeeRecipesWithCeliac")
                        .HasColumnType("bit");

                    b.Property<bool>("WantsUserToSeeRecipesWithFructose")
                        .HasColumnType("bit");

                    b.Property<bool>("WantsUserToSeeRecipesWithHistamin")
                        .HasColumnType("bit");

                    b.Property<bool>("WantsUserToSeeRecipesWithLactose")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("UserOptions");
                });

            modelBuilder.Entity("MenuPlanerApp.API.Model.IngredientWithAmount", b =>
                {
                    b.HasOne("MenuPlanerApp.API.Model.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MenuPlanerApp.API.Model.Recipe", null)
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId");
                });

            modelBuilder.Entity("MenuPlanerApp.API.Model.RecipeWithAmount", b =>
                {
                    b.HasOne("MenuPlanerApp.API.Model.MenuPlan", null)
                        .WithMany("RecipesWithAmounts")
                        .HasForeignKey("MenuPlanId");

                    b.HasOne("MenuPlanerApp.API.Model.Recipe", "Recipe")
                        .WithMany()
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
