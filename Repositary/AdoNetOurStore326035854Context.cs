using System;
using System.Collections.Generic;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Tamar_Sheva_Project;

public partial class AdoNetOurStore326035854Context : DbContext
{
    public AdoNetOurStore326035854Context()
    {
    }

    public AdoNetOurStore326035854Context(DbContextOptions<AdoNetOurStore326035854Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Catgory> Catgories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        //if(!optionsBuilder.IsConfigured)
      => optionsBuilder.UseSqlServer("Data Source=srv2\\PUPILS;Initial Catalog=Ado_Net_OurStore_326035854;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Catgory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.ToTable("CATGORIES");

            entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("CATEGORY_NAME");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("ORDERS");

            entity.Property(e => e.OrderId).HasColumnName("ORDER_ID");
            entity.Property(e => e.OrderDate).HasColumnName("ORDER_DATE");
            entity.Property(e => e.OrderSum).HasColumnName("ORDER_SUM");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_ORDERS_USERS");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.ToTable("ORDER_ITEM");

            entity.Property(e => e.OrderItemId).HasColumnName("ORDER_ITEM_ID");
            entity.Property(e => e.OrderId).HasColumnName("ORDER_ID");
            entity.Property(e => e.ProductId).HasColumnName("PRODUCT_ID");
            entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_ORDER_ITEM_ORDERS");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ORDER_ITEM_PRODUCTS");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("PRODUCTS");

            entity.Property(e => e.ProductId).HasColumnName("PRODUCT_ID");
            entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.ImgUrl)
                .HasMaxLength(40)
                .IsFixedLength()
                .HasColumnName("IMG_URL");
            entity.Property(e => e.Price).HasColumnName("PRICE");
            entity.Property(e => e.ProductName)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("PRODUCT_NAME");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_PRODUCTS_CATGORIES");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("USERS");

            entity.Property(e => e.UserId).HasColumnName("USER_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("EMAIL");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("PASSWORD");
            entity.Property(e => e.UserName)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("USER_NAME");
        });



        OnModelCreatingPartial(modelBuilder);
    }

   

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
