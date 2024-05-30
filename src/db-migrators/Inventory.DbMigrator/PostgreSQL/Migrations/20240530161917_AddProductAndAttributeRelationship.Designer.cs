﻿// <auto-generated />
using System;
using ECommerce.Inventory.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ECommerce.Inventory.DbMigrator.PostgreSQL.Migrations
{
    [DbContext(typeof(InventoryDbContext))]
    [Migration("20240530161917_AddProductAndAttributeRelationship")]
    partial class AddProductAndAttributeRelationship
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ECommerce.Inventory.Domain.AggregatesModel.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("description")
                        .HasDefaultValueSql("''");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid")
                        .HasColumnName("parent_id");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("slug");

                    b.HasKey("Id")
                        .HasName("pk_categories");

                    b.HasIndex("ParentId")
                        .HasDatabaseName("ix_categories_parent_id");

                    b.ToTable("categories", (string)null);
                });

            modelBuilder.Entity("ECommerce.Inventory.Domain.AggregatesModel.CategoryProduct", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid")
                        .HasColumnName("category_id");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnName("product_id");

                    b.HasKey("CategoryId", "ProductId")
                        .HasName("pk_category_products");

                    b.HasIndex("ProductId")
                        .HasDatabaseName("ix_category_products_product_id");

                    b.ToTable("category_products", (string)null);
                });

            modelBuilder.Entity("ECommerce.Inventory.Domain.AggregatesModel.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("slug");

                    b.HasKey("Id")
                        .HasName("pk_products");

                    b.ToTable("products", (string)null);
                });

            modelBuilder.Entity("ECommerce.Inventory.Domain.AggregatesModel.ProductAttribute", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_product_attributes");

                    b.ToTable("product_attributes", (string)null);
                });

            modelBuilder.Entity("ECommerce.Inventory.Domain.AggregatesModel.ProductProductAttribute", b =>
                {
                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnName("product_id");

                    b.Property<Guid>("ProductAttributeId")
                        .HasColumnType("uuid")
                        .HasColumnName("product_attribute_id");

                    b.HasKey("ProductId", "ProductAttributeId")
                        .HasName("pk_product_product_attributes");

                    b.HasIndex("ProductAttributeId")
                        .HasDatabaseName("ix_product_product_attributes_product_attribute_id");

                    b.ToTable("product_product_attributes", (string)null);
                });

            modelBuilder.Entity("ECommerce.Inventory.Domain.AggregatesModel.ProductVariant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("now()");

                    b.Property<decimal>("Price")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric")
                        .HasDefaultValue(0m)
                        .HasColumnName("price");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnName("product_id");

                    b.Property<int>("Stock")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0)
                        .HasColumnName("stock");

                    b.HasKey("Id")
                        .HasName("pk_product_variants");

                    b.HasIndex("ProductId")
                        .HasDatabaseName("ix_product_variants_product_id");

                    b.ToTable("product_variants", (string)null);
                });

            modelBuilder.Entity("ECommerce.Inventory.Domain.AggregatesModel.ProductVariantAttributeValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("now()");

                    b.Property<Guid>("ProductAttributeId")
                        .HasColumnType("uuid")
                        .HasColumnName("product_attribute_id");

                    b.Property<Guid>("ProductVariantId")
                        .HasColumnType("uuid")
                        .HasColumnName("product_variant_id");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_product_variant_attribute_values");

                    b.HasAlternateKey("ProductVariantId", "ProductAttributeId")
                        .HasName("ak_product_variant_attribute_values_product_variant_id_product");

                    b.HasIndex("ProductAttributeId")
                        .HasDatabaseName("ix_product_variant_attribute_values_product_attribute_id");

                    b.ToTable("product_variant_attribute_values", (string)null);
                });

            modelBuilder.Entity("ECommerce.Inventory.Domain.AggregatesModel.Category", b =>
                {
                    b.HasOne("ECommerce.Inventory.Domain.AggregatesModel.Category", "Parent")
                        .WithMany("Categories")
                        .HasForeignKey("ParentId")
                        .HasConstraintName("fk_categories_categories_parent_id");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("ECommerce.Inventory.Domain.AggregatesModel.CategoryProduct", b =>
                {
                    b.HasOne("ECommerce.Inventory.Domain.AggregatesModel.Category", "Category")
                        .WithMany("CategoryProducts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_category_products_categories_category_id");

                    b.HasOne("ECommerce.Inventory.Domain.AggregatesModel.Product", "Product")
                        .WithMany("CategoryProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_category_products_products_product_id");

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ECommerce.Inventory.Domain.AggregatesModel.ProductProductAttribute", b =>
                {
                    b.HasOne("ECommerce.Inventory.Domain.AggregatesModel.ProductAttribute", "ProductAttribute")
                        .WithMany("ProductProductAttributes")
                        .HasForeignKey("ProductAttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_product_product_attributes_product_attributes_product_attri");

                    b.HasOne("ECommerce.Inventory.Domain.AggregatesModel.Product", "Product")
                        .WithMany("ProductProductAttributes")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_product_product_attributes_products_product_id");

                    b.Navigation("Product");

                    b.Navigation("ProductAttribute");
                });

            modelBuilder.Entity("ECommerce.Inventory.Domain.AggregatesModel.ProductVariant", b =>
                {
                    b.HasOne("ECommerce.Inventory.Domain.AggregatesModel.Product", "Product")
                        .WithMany("ProductVariants")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_product_variants_products_product_id");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ECommerce.Inventory.Domain.AggregatesModel.ProductVariantAttributeValue", b =>
                {
                    b.HasOne("ECommerce.Inventory.Domain.AggregatesModel.ProductAttribute", "ProductAttribute")
                        .WithMany("ProductVariantAttributeValues")
                        .HasForeignKey("ProductAttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_product_variant_attribute_values_product_attributes_product");

                    b.HasOne("ECommerce.Inventory.Domain.AggregatesModel.ProductVariant", "ProductVariant")
                        .WithMany("ProductVariantAttributeValues")
                        .HasForeignKey("ProductVariantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_product_variant_attribute_values_product_variants_product_v");

                    b.Navigation("ProductAttribute");

                    b.Navigation("ProductVariant");
                });

            modelBuilder.Entity("ECommerce.Inventory.Domain.AggregatesModel.Category", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("CategoryProducts");
                });

            modelBuilder.Entity("ECommerce.Inventory.Domain.AggregatesModel.Product", b =>
                {
                    b.Navigation("CategoryProducts");

                    b.Navigation("ProductProductAttributes");

                    b.Navigation("ProductVariants");
                });

            modelBuilder.Entity("ECommerce.Inventory.Domain.AggregatesModel.ProductAttribute", b =>
                {
                    b.Navigation("ProductProductAttributes");

                    b.Navigation("ProductVariantAttributeValues");
                });

            modelBuilder.Entity("ECommerce.Inventory.Domain.AggregatesModel.ProductVariant", b =>
                {
                    b.Navigation("ProductVariantAttributeValues");
                });
#pragma warning restore 612, 618
        }
    }
}
