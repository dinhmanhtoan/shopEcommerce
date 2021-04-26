using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Model.Extention;

namespace Model.Models
{
    public partial class shopContext : IdentityDbContext<User, Role, long, IdentityUserClaim<long>, UserRole, IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>>
    { 
        public shopContext()
        {
        }

        public shopContext(DbContextOptions<shopContext> options)
            : base(options)
        {
        }
        public virtual DbSet<User> Users{ get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductMedia> ProductMedias { get; set; }
        public virtual DbSet<Media> Medias { get; set; }

        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=shop;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            CoreSeedData.SeedData(modelBuilder); 
            //modelBuilder.Entity<Category>(entity =>
            //{
            //    entity.ToTable("category");

            //    entity.HasIndex(e => e.Code)
            //        .HasName("UQ__category__A25C5AA7EAA66A4E")
            //        .IsUnique();

            //    entity.Property(e => e.Id).HasColumnName("ID");

            //    entity.Property(e => e.Code)
            //        .IsRequired()
            //        .HasMaxLength(20)
            //        .IsUnicode(false);

            //    entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            //    entity.Property(e => e.Description).HasMaxLength(1000);

            //    entity.Property(e => e.EditOn).HasColumnType("datetime");

            //    entity.Property(e => e.Thumbnail).HasMaxLength(250);

            //    entity.Property(e => e.Title)
            //        .IsRequired()
            //        .HasMaxLength(250);
            //});

            //modelBuilder.Entity<Product>(entity =>
            //{
            //    entity.ToTable("product");

            //    entity.HasIndex(e => e.Code)
            //        .HasName("UQ__product__A25C5AA7805FBE52")
            //        .IsUnique();

            //    entity.Property(e => e.Id).HasColumnName("ID");

            //    entity.Property(e => e.Code)
            //        .IsRequired()
            //        .HasMaxLength(20)
            //        .IsUnicode(false);

            //    entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            //    entity.Property(e => e.Description).HasMaxLength(1000);

            //    entity.Property(e => e.EditOn).HasColumnType("datetime");

            //    entity.Property(e => e.Images).HasColumnType("xml");

            //    entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            //    entity.Property(e => e.Sale).HasColumnType("decimal(18, 2)");

            //    entity.Property(e => e.Thumbnail).HasMaxLength(250);

            //    entity.Property(e => e.Title)
            //        .IsRequired()
            //        .HasMaxLength(250);
            //});
            modelBuilder.Entity<Category>(c =>
            {
                c.HasKey(m => m.Id);
                c.HasOne(x => x.Thumbnail)
                  .WithMany()
                  .HasForeignKey(x => x.ThumbnailId);
                c.ToTable("Category");
            });
             modelBuilder.Entity<Media>(m =>
            {
                m.HasKey(m => m.Id);
                m.ToTable("Media");
            });
            modelBuilder.Entity<Product>(p =>
            {
                p.HasKey(p => p.Id);
                p.ToTable("Product");
            });
            modelBuilder.Entity<Rating>(p =>
            {
                p.HasKey(p => p.Id);
                p.ToTable("Rating");
            });
            modelBuilder.Entity<User>(u =>
            {
                u.ToTable("User");
            });
            modelBuilder.Entity<ProductMedia>(p =>
            {
                //p.HasKey(pm => new { pm.MediaId, pm.ProductId });
                p.HasOne(pm => pm.Product).WithMany(x => x.Images).HasForeignKey(r => r.ProductId);
                p.HasOne(pm => pm.Media).WithMany(x => x.Products).HasForeignKey(r => r.MediaId);
                p.ToTable("ProductMedia");

            });

            modelBuilder.Entity<Role>().ToTable("Role");

            modelBuilder.Entity<IdentityUserClaim<long>>(b =>
            {
                b.HasKey(uc => uc.Id);
                b.ToTable("UserClaim");
            });
            modelBuilder.Entity<IdentityRoleClaim<long>>(b =>
            {
                b.HasKey(rc => rc.Id);
                b.ToTable("RoleClaim");
            });
            modelBuilder.Entity<UserRole>(b =>
            {
                b.HasKey(ur => new { ur.UserId, ur.RoleId });
                b.HasOne(ur => ur.Role).WithMany(x => x.Users).HasForeignKey(r => r.RoleId);
                b.HasOne(ur => ur.User).WithMany(x => x.Roles).HasForeignKey(u => u.UserId);
                b.ToTable("UserRole");
            });

            modelBuilder.Entity<IdentityUserLogin<long>>(b =>
            {
                
                b.ToTable("UserLogin");
            });

            modelBuilder.Entity<IdentityUserToken<long>>(b =>
            {
                b.ToTable("UserToken");
            });
            modelBuilder.Entity<Order>(o =>{

                o.HasKey(x => x.Id);

                o.Property(x => x.CreateOn);
                o.Property(x => x.FullName).IsRequired().HasMaxLength(200);

                o.Property(x => x.Email).IsRequired().IsUnicode(false).HasMaxLength(50);

                o.Property(x => x.AddressLine1).IsRequired().HasMaxLength(500);
                o.Property(x => x.AddressLine2).HasMaxLength(500);
                o.Property(x => x.Node).IsRequired().HasMaxLength(1000);
                o.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(50);

            

            });
            modelBuilder.Entity<OrderDetail>(o => {

                o.HasKey(x => new { x.OrderId, x.ProductId });

                o.HasOne(x => x.Order).WithMany(x => x.OrderDetails).HasForeignKey(x => x.OrderId);

                o.HasOne(x => x.Product).WithMany(x => x.OrderDetails).HasForeignKey(x => x.ProductId);

            });

          

        }

       // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
