﻿using CheeseMVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CheeseMVC.Migrations
{
    [DbContext(typeof(CheeseDbContext))]
    partial class CheeseDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CheeseMVC.Models.Cheese", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryID");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Cheeses");
                });

            modelBuilder.Entity("CheeseMVC.Models.CheeseCategory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CheeseMVC.Models.CheeseMenu", b =>
                {
                    b.Property<int>("CheeseID");

                    b.Property<int>("MenuID");

                    b.HasKey("CheeseID", "MenuID");

                    b.HasIndex("MenuID");

                    b.ToTable("CheeseMenus");
                });

            modelBuilder.Entity("CheeseMVC.Models.Menu", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("CheeseMVC.Models.Cheese", b =>
                {
                    b.HasOne("CheeseMVC.Models.CheeseCategory", "Category")
                        .WithMany("Cheese")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CheeseMVC.Models.CheeseMenu", b =>
                {
                    b.HasOne("CheeseMVC.Models.Cheese", "Cheese")
                        .WithMany("CheeseMenus")
                        .HasForeignKey("CheeseID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CheeseMVC.Models.Menu", "Menu")
                        .WithMany("CheeseMenus")
                        .HasForeignKey("MenuID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
