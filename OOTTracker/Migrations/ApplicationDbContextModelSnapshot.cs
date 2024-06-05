﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OOTTracker.Data;

#nullable disable

namespace OOTTracker.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OOTTracker.Data.Collectable", b =>
                {
                    b.Property<Guid>("CollectableId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CollectableTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CollectableId");

                    b.HasIndex("CollectableTypeId");

                    b.HasIndex("LocationId");

                    b.ToTable("Collectables");
                });

            modelBuilder.Entity("OOTTracker.Data.CollectableType", b =>
                {
                    b.Property<Guid>("CollectableTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IconClass")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CollectableTypeId");

                    b.ToTable("CollectableTypes");
                });

            modelBuilder.Entity("OOTTracker.Data.InventoryEquipment", b =>
                {
                    b.Property<Guid>("InventoryEquipmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InventoryEquipmentId");

                    b.ToTable("InventoryEquipment");
                });

            modelBuilder.Entity("OOTTracker.Data.ItemAgeRequirement", b =>
                {
                    b.Property<Guid>("ItemAgeRequirementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ItemAgeRequirementId");

                    b.ToTable("ItemAgeRequirements");
                });

            modelBuilder.Entity("OOTTracker.Data.ItemCheck", b =>
                {
                    b.Property<Guid>("ItemCheckId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ItemAgeRequirementId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ItemCheckTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ItemCheckId");

                    b.HasIndex("ItemAgeRequirementId");

                    b.HasIndex("ItemCheckTypeId");

                    b.HasIndex("LocationId");

                    b.ToTable("ItemChecks");
                });

            modelBuilder.Entity("OOTTracker.Data.ItemCheckRequirement", b =>
                {
                    b.Property<Guid>("ItemCheckRequirementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("InventoryEquipmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ItemCheckId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ItemCheckRequirementId");

                    b.HasIndex("InventoryEquipmentId");

                    b.HasIndex("ItemCheckId");

                    b.ToTable("ItemCheckRequirements");
                });

            modelBuilder.Entity("OOTTracker.Data.ItemCheckType", b =>
                {
                    b.Property<Guid>("ItemCheckTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ItemCheckTypeId");

                    b.ToTable("ItemCheckTypes");
                });

            modelBuilder.Entity("OOTTracker.Data.Location", b =>
                {
                    b.Property<Guid>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LocationId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("OOTTracker.Data.Playthrough", b =>
                {
                    b.Property<Guid>("PlaythroughId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsRandomized")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlaythroughId");

                    b.ToTable("Playthroughs");
                });

            modelBuilder.Entity("OOTTracker.Data.PlaythroughEquipment", b =>
                {
                    b.Property<Guid>("PlaythroughEquipmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("InventoryEquipmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("Obtained")
                        .HasColumnType("bit");

                    b.Property<Guid?>("PlaythroughId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PlaythroughEquipmentId");

                    b.HasIndex("InventoryEquipmentId");

                    b.HasIndex("PlaythroughId");

                    b.ToTable("PlaythroughEquipment");
                });

            modelBuilder.Entity("OOTTracker.Data.PlaythroughItemCheck", b =>
                {
                    b.Property<Guid>("PlaythroughItemCheckId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ItemCheckId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("Obtained")
                        .HasColumnType("bit");

                    b.Property<Guid?>("PlaythroughId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Spoiler")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlaythroughItemCheckId");

                    b.HasIndex("ItemCheckId");

                    b.HasIndex("PlaythroughId");

                    b.ToTable("PlaythroughItemChecks");
                });

            modelBuilder.Entity("OOTTracker.Data.Collectable", b =>
                {
                    b.HasOne("OOTTracker.Data.CollectableType", "CollectableType")
                        .WithMany("Collectables")
                        .HasForeignKey("CollectableTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OOTTracker.Data.Location", "Location")
                        .WithMany("Collectables")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("CollectableType");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("OOTTracker.Data.ItemCheck", b =>
                {
                    b.HasOne("OOTTracker.Data.ItemAgeRequirement", "ItemAgeRequirement")
                        .WithMany("ItemChecks")
                        .HasForeignKey("ItemAgeRequirementId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OOTTracker.Data.ItemCheckType", "ItemCheckType")
                        .WithMany("ItemChecks")
                        .HasForeignKey("ItemCheckTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OOTTracker.Data.Location", "Location")
                        .WithMany("ItemChecks")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("ItemAgeRequirement");

                    b.Navigation("ItemCheckType");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("OOTTracker.Data.ItemCheckRequirement", b =>
                {
                    b.HasOne("OOTTracker.Data.InventoryEquipment", "InventoryEquipment")
                        .WithMany("ItemCheckRequirements")
                        .HasForeignKey("InventoryEquipmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OOTTracker.Data.ItemCheck", "ItemCheck")
                        .WithMany("ItemCheckRequirements")
                        .HasForeignKey("ItemCheckId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("InventoryEquipment");

                    b.Navigation("ItemCheck");
                });

            modelBuilder.Entity("OOTTracker.Data.PlaythroughEquipment", b =>
                {
                    b.HasOne("OOTTracker.Data.InventoryEquipment", "InventoryEquipment")
                        .WithMany("PlaythroughEquipment")
                        .HasForeignKey("InventoryEquipmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OOTTracker.Data.Playthrough", "Playthrough")
                        .WithMany("PlaythroughEquipment")
                        .HasForeignKey("PlaythroughId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("InventoryEquipment");

                    b.Navigation("Playthrough");
                });

            modelBuilder.Entity("OOTTracker.Data.PlaythroughItemCheck", b =>
                {
                    b.HasOne("OOTTracker.Data.ItemCheck", "ItemCheck")
                        .WithMany("PlaythroughItemChecks")
                        .HasForeignKey("ItemCheckId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OOTTracker.Data.Playthrough", "Playthrough")
                        .WithMany("PlaythroughItemChecks")
                        .HasForeignKey("PlaythroughId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("ItemCheck");

                    b.Navigation("Playthrough");
                });

            modelBuilder.Entity("OOTTracker.Data.CollectableType", b =>
                {
                    b.Navigation("Collectables");
                });

            modelBuilder.Entity("OOTTracker.Data.InventoryEquipment", b =>
                {
                    b.Navigation("ItemCheckRequirements");

                    b.Navigation("PlaythroughEquipment");
                });

            modelBuilder.Entity("OOTTracker.Data.ItemAgeRequirement", b =>
                {
                    b.Navigation("ItemChecks");
                });

            modelBuilder.Entity("OOTTracker.Data.ItemCheck", b =>
                {
                    b.Navigation("ItemCheckRequirements");

                    b.Navigation("PlaythroughItemChecks");
                });

            modelBuilder.Entity("OOTTracker.Data.ItemCheckType", b =>
                {
                    b.Navigation("ItemChecks");
                });

            modelBuilder.Entity("OOTTracker.Data.Location", b =>
                {
                    b.Navigation("Collectables");

                    b.Navigation("ItemChecks");
                });

            modelBuilder.Entity("OOTTracker.Data.Playthrough", b =>
                {
                    b.Navigation("PlaythroughEquipment");

                    b.Navigation("PlaythroughItemChecks");
                });
#pragma warning restore 612, 618
        }
    }
}
