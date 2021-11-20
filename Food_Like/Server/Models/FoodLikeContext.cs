using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Food_Like.Shared;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Food_Like.Server.Models
{
    public partial class foodlikeContext : DbContext
    {
        public foodlikeContext()
        {
        }

        public foodlikeContext(DbContextOptions<foodlikeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Buyer> Buyer { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Meal> Meal { get; set; }
        public virtual DbSet<Mealcategory> Mealcategory { get; set; }
        public virtual DbSet<Mealorder> Mealorder { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<Seller> Seller { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;port=3306;database=foodlike;user=foodlike;password=foodlike", x => x.ServerVersion("8.0.23-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("address");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Line1)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Line2)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Buyer>(entity =>
            {
                entity.ToTable("buyer");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.EncryptedPassword)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ProfilePicture)
                    .IsRequired()
                    .HasColumnType("mediumblob");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Titel)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Meal>(entity =>
            {
                entity.ToTable("meal");

                entity.HasIndex(e => e.SellerId)
                    .HasName("SellerId");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Ingridients)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.MealDescription)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.MealPicture)
                    .IsRequired()
                    .HasColumnType("mediumblob");

                entity.Property(e => e.PortionPrice).HasColumnType("decimal(5,2)");

                entity.Property(e => e.Titel)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.Meal)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("meal_ibfk_1");
            });

            modelBuilder.Entity<Mealcategory>(entity =>
            {
                entity.HasKey(e => new { e.MealId, e.CategoryId })
                    .HasName("PRIMARY");

                entity.ToTable("mealcategory");

                entity.HasIndex(e => e.CategoryId)
                    .HasName("CategoryId");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Mealcategory)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mealcategory_ibfk_2");

                entity.HasOne(d => d.Meal)
                    .WithMany(p => p.Mealcategory)
                    .HasForeignKey(d => d.MealId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mealcategory_ibfk_1");
            });

            modelBuilder.Entity<Mealorder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PRIMARY");

                entity.ToTable("mealorder");

                entity.HasIndex(e => e.BuyerId)
                    .HasName("BuyerId");

                entity.HasIndex(e => e.MealId)
                    .HasName("MealId");

                entity.Property(e => e.PickUpTime).HasColumnType("datetime");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.Mealorder)
                    .HasForeignKey(d => d.BuyerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mealorder_ibfk_1");

                entity.HasOne(d => d.Meal)
                    .WithMany(p => p.Mealorder)
                    .HasForeignKey(d => d.MealId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mealorder_ibfk_2");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("review");

                entity.HasIndex(e => e.BuyerId)
                    .HasName("BuyerId");

                entity.HasIndex(e => e.MealId)
                    .HasName("MealId");

                entity.Property(e => e.ReviewDescription)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.BuyerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("review_ibfk_1");

                entity.HasOne(d => d.Meal)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.MealId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("review_ibfk_2");
            });

            modelBuilder.Entity<Seller>(entity =>
            {
                entity.ToTable("seller");

                entity.HasIndex(e => e.AddressId)
                    .HasName("AddressId");

                entity.Property(e => e.KitchenPicture)
                    .IsRequired()
                    .HasColumnType("mediumblob");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Seller)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("seller_ibfk_2");

                entity.HasOne(d => d.SellerNavigation)
                    .WithOne(p => p.Seller)
                    .HasForeignKey<Seller>(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("seller_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
