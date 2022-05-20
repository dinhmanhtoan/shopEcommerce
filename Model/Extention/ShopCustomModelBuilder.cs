namespace Model.Extention;

public class ShopCustomModelBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>(a =>
        {
            a.HasKey(x => x.Id);
            a.HasOne(x => x.ActivityType).WithMany().HasForeignKey(x => x.ActivityTypeId);
        });
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
        modelBuilder.Entity<User>(u =>
        {
            u.HasOne(x => x.DefaultShippingAddress)
                .WithMany()
                .HasForeignKey(x => x.DefaultShippingAddressId)
                .OnDelete(DeleteBehavior.Restrict);

            u.HasOne(x => x.DefaultBillingAddress)
                .WithMany()
                .HasForeignKey(x => x.DefaultBillingAddressId)
                .OnDelete(DeleteBehavior.Restrict);
            u.ToTable("User");
        });
        modelBuilder.Entity<StateOrProvince>(x =>
        {
            x.HasKey(x => x.Id);
            x.HasOne(x => x.Country).WithMany(x => x.StatesOrProvinces).HasForeignKey(x => x.CountryId);
        });
        modelBuilder.Entity<UserAddress>()
            .HasOne(x => x.User)
            .WithMany(a => a.UserAddresses)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Address>(x =>
        {
            x.HasOne(d => d.District)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            x.HasOne(d => d.StateOrProvince)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            x.HasOne(d => d.Country)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<ProductMedia>(p =>
        {
            p.HasOne(pm => pm.Product).WithMany(x => x.Images).HasForeignKey(r => r.ProductId);
            p.HasOne(pm => pm.Media).WithMany(x => x.Products).HasForeignKey(r => r.MediaId);
            p.ToTable("ProductMedia");

        });
        modelBuilder.Entity<ProductCategory>(p =>
        {
            p.HasOne(pm => pm.Product).WithMany(x => x.Categories).HasForeignKey(r => r.ProductId);
            p.HasOne(pm => pm.Category).WithMany(x => x.Products).HasForeignKey(r => r.CategoryId);
            p.ToTable("ProductCategory");

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
        modelBuilder.Entity<OrderAddress>(x =>
        {
            x.HasKey(x => x.Id);
            x.HasOne(d => d.District)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            x.HasOne(d => d.StateOrProvince)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            x.HasOne(d => d.Country)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Order>(u =>
        {
            u.HasKey(x => x.Id);
            u.HasOne(x => x.ShippingAddress)
            .WithMany()
            .HasForeignKey(x => x.ShippingAddressId)
            .OnDelete(DeleteBehavior.Restrict);

            u.HasOne(x => x.BillingAddress)
            .WithMany()
            .HasForeignKey(x => x.BillingAddressId)
            .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<OrderHistory>(o =>
        {
            o.HasOne(x => x.CreatedBy)
            .WithMany()
            .HasForeignKey(x => x.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Brand>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Name).IsRequired().HasMaxLength(450);
            b.Property(x => x.Slug).IsRequired().HasMaxLength(450);
            b.Property(x => x.Description).HasColumnType("nvarchar(max)");
            b.Property(x => x.IsDeleted);
            b.Property(x => x.IsPublished);
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
        modelBuilder.Entity<CartItem>(c =>
        {
            c.HasKey(x => x.Id);
            c.HasOne(x => x.Cart).WithMany(x => x.cartItems).HasForeignKey(x => x.CartId).OnDelete(DeleteBehavior.Restrict);
            c.HasOne(x => x.Product).WithMany(x => x.cartItems).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);

        });
        modelBuilder.Entity<WishListItem>(c =>
        {
            c.HasKey(x => x.Id);
            c.HasOne(x => x.WishList).WithMany(x => x.Items).HasForeignKey(x => x.WishListId);
        });
        modelBuilder.Entity<ProductLink>(c =>
        {
            c.HasKey(x => x.Id);
            c.HasOne(x => x.Product).WithMany(p => p.ProductLinks).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);
            c.HasOne(x => x.LinkedProduct).WithMany(p => p.LinkedProductLinks).HasForeignKey(x => x.LinkedProductId).OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<StockHistory>(c =>
        {
            c.HasKey(x => x.Id);
            c.HasOne(x => x.Warehouse).WithMany().HasForeignKey(x => x.WarehouseId);
        });

        modelBuilder.Entity<Warehouse>(w =>
        {
            w.HasKey(x => x.Id);
            w.HasOne(x => x.Address).WithMany().HasForeignKey(x => x.AddressId);
        });
        modelBuilder.Entity<Pages>(p =>
        {
            p.HasKey(x => x.Id);
            p.HasOne(x => x.CreatedBy).WithMany().OnDelete(DeleteBehavior.Restrict);
            p.HasOne(x => x.LatestUpdatedBy).WithMany().OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<Payment>(p =>
        {
            p.HasKey(x => x.Id);
            p.HasOne(x => x.Order).WithMany().HasForeignKey(x => x.OrderId);
        });
        modelBuilder.Entity<Shipment>(s =>
        {
            s.HasKey(x => x.Id);
            s.HasOne(x => x.Order).WithMany().HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.Restrict); ;
            s.HasOne(x => x.CreatedBy).WithMany().HasForeignKey(x => x.CreatedById).OnDelete(DeleteBehavior.Restrict);
            s.HasOne(x => x.Warehouse).WithMany().HasForeignKey(x => x.WarehouseId).OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<ShipmentItem>(s =>
        {
            s.HasKey(x => x.Id);
            s.HasOne(x => x.Shipment).WithMany(x => x.Items).HasForeignKey(x => x.ShipmentId).OnDelete(DeleteBehavior.Restrict);
            s.HasOne(x => x.Product).WithMany(x => x.ShipmentItem).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<TaxRate>(t =>
        {
            t.HasKey(x => x.Id);
            t.HasOne(x => x.TaxClass).WithMany().HasForeignKey(x => x.TaxClassId).OnDelete(DeleteBehavior.Restrict);
            t.HasOne(x => x.Country).WithMany().HasForeignKey(x => x.CountryId).OnDelete(DeleteBehavior.Restrict); ;
            t.HasOne(x => x.StateOrProvince).WithMany().HasForeignKey(x => x.StateOrProvinceId).OnDelete(DeleteBehavior.Restrict); ;

        });

        modelBuilder.Entity<Reply>(r =>
        {
            r.HasKey(x => x.Id);
            r.HasOne(x => x.Review).WithMany(x => x.Replies).HasForeignKey(x => x.ReviewId).OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<AppSetting>(a =>
        {
            a.HasKey(x => x.Id);
        });
        modelBuilder.Entity<NewsItemCategory>(a =>
        {
            a.HasKey(x => new { x.CategoryId, x.NewsItemId });
            a.HasOne(x => x.Category).WithMany(x => x.NewsItems).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Cascade);
            a.HasOne(x => x.NewsItem).WithMany(x => x.Categories).HasForeignKey(x => x.NewsItemId).OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<CustomerGroup>()
            .HasIndex(d => d.Name)
            .IsUnique();

        modelBuilder.Entity<CustomerGroupUser>(b =>
        {
            b.HasKey(ur => new { ur.UserId, ur.CustomerGroupId });
            b.HasOne(ur => ur.User).WithMany(r => r.CustomerGroups).HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Cascade);
            b.HasOne(ur => ur.CustomerGroup).WithMany(u => u.Users).HasForeignKey(u => u.CustomerGroupId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CustomerGroupUser>(b =>
        {
            b.HasKey(ur => new { ur.UserId, ur.CustomerGroupId });
            b.HasOne(ur => ur.User).WithMany(r => r.CustomerGroups).HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Cascade);
            b.HasOne(ur => ur.CustomerGroup).WithMany(u => u.Users).HasForeignKey(u => u.CustomerGroupId).OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<ProductTemplateProductAttribute>()
        .HasKey(t => new { t.ProductTemplateId, t.ProductAttributeId });
        modelBuilder.Entity<ProductTemplateProductAttribute>()
            .HasOne(pt => pt.ProductTemplate)
            .WithMany(p => p.ProductAttributes)
            .HasForeignKey(pt => pt.ProductTemplateId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<ProductTemplateProductAttribute>()
            .HasOne(pt => pt.ProductAttribute)
            .WithMany(t => t.ProductTemplates)
            .HasForeignKey(pt => pt.ProductAttributeId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Coupon>(b =>
        {
            b.HasOne(cc => cc.CartRule).WithMany(c => c.Coupons).HasForeignKey(x => x.CartRuleId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CartRuleCategory>(b =>
        {
            b.HasKey(cc => new { cc.CartRuleId, cc.CategoryId });
            b.HasOne(cc => cc.CartRule).WithMany(c => c.Categories).HasForeignKey(x => x.CartRuleId).OnDelete(DeleteBehavior.Cascade);
            b.HasOne(cc => cc.Category).WithMany().HasForeignKey(x => x.CategoryId);
        });

        modelBuilder.Entity<CartRuleProduct>(b =>
        {
            b.HasKey(cp => new { cp.CartRuleId, cp.ProductId });
            b.HasOne(cp => cp.CartRule).WithMany(p => p.Products).HasForeignKey(x => x.CartRuleId).OnDelete(DeleteBehavior.Cascade);
            b.HasOne(cp => cp.Product).WithMany().HasForeignKey(x => x.ProductId);
        });

        modelBuilder.Entity<CartRuleCustomerGroup>(b =>
        {
            b.HasKey(cc => new { cc.CartRuleId, cc.CustomerGroupId });
            b.HasOne(cc => cc.CartRule).WithMany(c => c.CustomerGroups).HasForeignKey(x => x.CartRuleId).OnDelete(DeleteBehavior.Cascade);
            b.HasOne(cc => cc.CustomerGroup).WithMany().HasForeignKey(x => x.CustomerGroupId);
        });

        modelBuilder.Entity<CatalogRuleCustomerGroup>(b =>
        {
            b.HasKey(cc => new { cc.CatalogRuleId, cc.CustomerGroupId });
            b.HasOne(cc => cc.CatalogRule).WithMany(c => c.CustomerGroups).HasForeignKey(x => x.CatalogRuleId);
            b.HasOne(cc => cc.CustomerGroup).WithMany().HasForeignKey(x => x.CustomerGroupId);
        });
        modelBuilder.Entity<Widget>().HasData(
            new Widget("CarouselWidget") { Name = "Carousel Widget", ViewComponentName = "CarouselWidget", CreateUrl = "CarouselWidget/Create", EditUrl = "CarouselWidget/Edit", CreatedOn = new DateTimeOffset(new DateTime(2018, 5, 29, 4, 33, 39, 163, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)) },
            new Widget("CategoryWidget") { Name = "Category Widget", ViewComponentName = "CategoryWidget", CreateUrl = "CategoryWidget/Create", EditUrl = "CategoryWidget/Edit", CreatedOn = new DateTimeOffset(new DateTime(2018, 5, 29, 4, 33, 39, 160, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)) },
            new Widget("HtmlWidget") { Name = "Html Widget", ViewComponentName = "HtmlWidget", CreateUrl = "HtmlWidget/Create", EditUrl = "HtmlWidget/Edit", CreatedOn = new DateTimeOffset(new DateTime(2018, 5, 29, 4, 33, 39, 160, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)) },
            new Widget("ProductWidget") { Name = "Product Widget", ViewComponentName = "ProductWidget", CreateUrl = "ProductWidget/Create", EditUrl = "ProductWidget/Edit", CreatedOn = new DateTimeOffset(new DateTime(2018, 5, 29, 4, 33, 39, 163, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)) },
            new Widget("RecentlyViewedWidget") { Name = "Recently Viewed Widget", ViewComponentName = "RecentlyViewedWidget", CreateUrl = "SimpleProductWidget/Create", EditUrl = "SimpleProductWidget/Edit", CreatedOn = new DateTimeOffset(new DateTime(2018, 5, 29, 4, 33, 39, 163, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)) },
            new Widget("SimpleProductWidget") { Name = "Simple Product Widget", ViewComponentName = "SimpleProductWidget", CreateUrl = "SimpleProductWidget/Create", EditUrl = "SimpleProductWidget/Edit", CreatedOn = new DateTimeOffset(new DateTime(2018, 5, 29, 4, 33, 39, 163, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)) },
            new Widget("ProductCarouselWidget") { Name = "Product Carousel Widget", ViewComponentName = "ProductCarouselWidget", CreateUrl = "ProductCarouselWidget/Create", EditUrl = "ProductCarouselWidget/Edit", CreatedOn = new DateTimeOffset(new DateTime(2018, 5, 29, 4, 33, 39, 163, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)) },
            new Widget("SpaceBarWidget") { Name = "SpaceBar Widget", ViewComponentName = "SpaceBarWidget", CreateUrl = "SpaceBarWidget/Create", EditUrl = "SpaceBarWidget/Edit", CreatedOn = new DateTimeOffset(new DateTime(2018, 5, 29, 4, 33, 39, 163, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)) }

        );
        modelBuilder.Entity<PaymentProvider>().ToTable("PaymentProvider");
        modelBuilder.Entity<ShippingProvider>().ToTable("ShippingProvider");
    }
}
