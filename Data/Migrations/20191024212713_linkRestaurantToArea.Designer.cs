﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20191024212713_linkRestaurantToArea")]
    partial class linkRestaurantToArea
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DomainModel.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("DomainModel.Meal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Description");

                    b.Property<int>("GeneralPriority");

                    b.Property<string>("ImageUrl");

                    b.Property<int>("LocalPriority");

                    b.Property<int?>("MealCategoryId");

                    b.Property<int?>("MealTypeId");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<int?>("RestaurantId");

                    b.HasKey("Id");

                    b.HasIndex("MealCategoryId");

                    b.HasIndex("MealTypeId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("DomainModel.MealCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("MealCategories");
                });

            modelBuilder.Entity("DomainModel.MealContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<int?>("MealId");

                    b.HasKey("Id");

                    b.HasIndex("MealId");

                    b.ToTable("MealContents");
                });

            modelBuilder.Entity("DomainModel.MealType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Name");

                    b.Property<int>("RestaurantId");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("MealTypes");
                });

            modelBuilder.Entity("DomainModel.Restaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<int?>("AreaId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Description");

                    b.Property<decimal>("Latitude");

                    b.Property<decimal>("Longitude");

                    b.Property<string>("Name");

                    b.Property<string>("PhoneNumber");

                    b.Property<int>("Priority");

                    b.Property<int?>("RestaurantCategoryId");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("RestaurantCategoryId");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("DomainModel.RestaurantCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("RestaurantCategories");
                });

            modelBuilder.Entity("DomainModel.RestaurantImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<int>("ImagePriority");

                    b.Property<string>("ImageUrl")
                        .IsRequired();

                    b.Property<int?>("RestaurantId");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("RestaurantImages");
                });

            modelBuilder.Entity("DomainModel.WorkingHour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Day");

                    b.Property<string>("FromTime");

                    b.Property<int?>("RestaurantId");

                    b.Property<string>("ToTime");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("WorkingHours");
                });

            modelBuilder.Entity("DomainModel.Meal", b =>
                {
                    b.HasOne("DomainModel.MealCategory", "MealCategory")
                        .WithMany()
                        .HasForeignKey("MealCategoryId");

                    b.HasOne("DomainModel.MealType", "MealType")
                        .WithMany()
                        .HasForeignKey("MealTypeId");

                    b.HasOne("DomainModel.Restaurant", "Restaurant")
                        .WithMany("Meals")
                        .HasForeignKey("RestaurantId");
                });

            modelBuilder.Entity("DomainModel.MealContent", b =>
                {
                    b.HasOne("DomainModel.Meal")
                        .WithMany("MealContents")
                        .HasForeignKey("MealId");
                });

            modelBuilder.Entity("DomainModel.MealType", b =>
                {
                    b.HasOne("DomainModel.Restaurant", "Restaurant")
                        .WithMany("MealTypes")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DomainModel.Restaurant", b =>
                {
                    b.HasOne("DomainModel.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId");

                    b.HasOne("DomainModel.RestaurantCategory", "RestaurantCategory")
                        .WithMany()
                        .HasForeignKey("RestaurantCategoryId");
                });

            modelBuilder.Entity("DomainModel.RestaurantImage", b =>
                {
                    b.HasOne("DomainModel.Restaurant", "Restaurant")
                        .WithMany("RestaurantImages")
                        .HasForeignKey("RestaurantId");
                });

            modelBuilder.Entity("DomainModel.WorkingHour", b =>
                {
                    b.HasOne("DomainModel.Restaurant")
                        .WithMany("WorkingHours")
                        .HasForeignKey("RestaurantId");
                });
#pragma warning restore 612, 618
        }
    }
}