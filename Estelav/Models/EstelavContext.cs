using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Estelav.Models
{
    public partial class EstelavContext : DbContext
    {
        public EstelavContext()
        {
        }

        public EstelavContext(DbContextOptions<EstelavContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<ImagesList> ImagesList { get; set; }
        public virtual DbSet<ItemSizes> ItemSizes { get; set; }
        public virtual DbSet<Items> Items { get; set; }
        public virtual DbSet<ItemsDescription> ItemsDescription { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=Estelav;Initial Catalog=Estelav;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.CatergoryId);

                entity.Property(e => e.CatergoryId)
                    .HasColumnName("CatergoryID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CategoryDescription).HasColumnType("text");

                entity.Property(e => e.CategoryDescriptionCz)
                    .HasColumnName("CategoryDescriptionCZ")
                    .HasColumnType("text");

                entity.Property(e => e.CategoryDescriptionRu)
                    .HasColumnName("CategoryDescriptionRU")
                    .HasColumnType("text");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.CategoryNameCz)
                    .HasColumnName("CategoryNameCZ")
                    .HasColumnType("text");

                entity.Property(e => e.CategoryNameRu)
                    .HasColumnName("CategoryNameRU")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<ImagesList>(entity =>
            {
                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ImagesList)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImagesList_Items");
            });

            modelBuilder.Entity<ItemSizes>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Size)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Item)
                    .WithMany()
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemSizes_Items");
            });

            modelBuilder.Entity<Items>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("text");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Items_Categories");
            });

            modelBuilder.Entity<ItemsDescription>(entity =>
            {
                entity.HasKey(e => e.DescrId);

                entity.Property(e => e.Description)
                    .IsUnicode(true);

                entity.Property(e => e.Language)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(true);

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemsDescription)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemsDescription_Items");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.Property(e => e.StatusId)
                    .HasColumnName("status_id");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasColumnName("status_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();


                entity.Property(e => e.CartId)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ShoppingCartItem>(entity =>
            {
                entity.Property(e => e.ShoppingCartId)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Ordered)
                    .HasColumnName("Ordered");

                entity.HasOne(d => d.ItemNavigation)
                    .WithMany(p => p.ShoppingCartItem)
                    .HasForeignKey(d => d.Item)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingCartItem_Items");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.CreatedUtc).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
