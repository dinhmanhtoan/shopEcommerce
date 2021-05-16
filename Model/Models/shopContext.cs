using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductMedia> ProductMedias { get; set; }
        public virtual DbSet<Media> Medias { get; set; }

        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<ProductOption> ProductOption { get; set; }
        public virtual DbSet<ProductOptionValue> ProductOptionValue { get; set; }
        public virtual DbSet<ProductOptionCombination> ProductOptionCombination { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<CartItem> CartItem { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=shop2;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            CoreSeedData.SeedData(modelBuilder);
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
                p.HasOne(p => p.Brand).WithMany(x => x.Products).HasForeignKey(r => r.BrandId);
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
            modelBuilder.Entity<Order>(o =>
            {

                o.HasKey(x => x.Id);

                o.Property(x => x.CreateOn);
                o.Property(x => x.FullName).IsRequired().HasMaxLength(200);
                o.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(50);
                o.Property(x => x.Email).IsUnicode(false).HasMaxLength(50);
                o.Property(x => x.AddressLine1).IsRequired().HasMaxLength(500);
                o.Property(x => x.AddressLine2).HasMaxLength(500);
                o.Property(x => x.Node).HasMaxLength(1000);
    



            });
            modelBuilder.Entity<OrderDetail>(o =>
            {

                o.HasKey(x => x.Id);

                o.HasOne(x => x.Order).WithMany(x => x.OrderDetails).HasForeignKey(x => x.OrderId);

                o.HasOne(x => x.Product).WithMany(x => x.OrderDetails).HasForeignKey(x => x.ProductId);

            });

            modelBuilder.Entity<Brand>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Name).IsRequired().HasMaxLength(450);
                b.Property(x => x.Slug).IsRequired().HasMaxLength(450);
                b.Property(x => x.Description).IsRequired().HasColumnType("nvarchar(max)");
                b.Property(x => x.IsDeleted).IsRequired();
                b.Property(x => x.IsPublished).IsRequired();
            });

            modelBuilder.Entity<ProductOption>(o =>
            {
                o.HasKey(x => x.Id);
                o.Property(x => x.Name).IsRequired();
            });
            modelBuilder.Entity<ProductOptionValue>(o =>
            {
                o.HasKey(x => new { x.Id });
                o.HasOne(x => x.Option).WithMany(x => x.OptionValue).HasForeignKey(x => x.OptionId);
                o.Property(x => x.Value).HasMaxLength(450);
                o.Property(x => x.DisplayType).HasMaxLength(450);
                o.Property(x => x.SortIndex).IsRequired();
                o.HasOne(x => x.Product).WithMany(x => x.OptionValues).HasForeignKey(x => x.ProductId);
            });
            modelBuilder.Entity<ProductOptionCombination>(o =>
            {
                o.HasKey(x => x.Id);

                o.HasOne(x => x.Product).WithMany(x => x.OptionCombinations).HasForeignKey(x => x.ProductId);

                o.HasOne(x => x.Option).WithMany(x => x.OptionCombinations).HasForeignKey(x => x.OptionId);
            });
            modelBuilder.Entity<Cart>(c =>
            {
                c.HasKey(x => x.Id);
            });
            modelBuilder.Entity<CartItem>(c =>
            {
                c.HasKey(x => x.Id);
                c.HasOne(x => x.Cart).WithMany(x => x.cartItems).HasForeignKey(x => x.CartId);
                c.HasOne(x => x.Product).WithMany(x => x.cartItems).HasForeignKey(x => x.ProductId);

            });
        }

        // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
