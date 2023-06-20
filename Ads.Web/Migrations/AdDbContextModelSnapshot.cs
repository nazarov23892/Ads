﻿// <auto-generated />
using System;
using Ads.Web.DataContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ads.Web.Migrations
{
    [DbContext(typeof(AdDbContext))]
    partial class AdDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.18");

            modelBuilder.Entity("Ads.Web.Entities.Ad", b =>
                {
                    b.Property<int>("AdId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateCreatedUtc")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.HasKey("AdId");

                    b.ToTable("Ads");
                });

            modelBuilder.Entity("Ads.Web.Entities.ImageUrl", b =>
                {
                    b.Property<int>("ImageUrlId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AdId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsMain")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ImageUrlId");

                    b.HasIndex("AdId");

                    b.ToTable("ImageUrl");
                });

            modelBuilder.Entity("Ads.Web.Entities.ImageUrl", b =>
                {
                    b.HasOne("Ads.Web.Entities.Ad", null)
                        .WithMany("ImageUrls")
                        .HasForeignKey("AdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Ads.Web.Entities.Ad", b =>
                {
                    b.Navigation("ImageUrls");
                });
#pragma warning restore 612, 618
        }
    }
}
