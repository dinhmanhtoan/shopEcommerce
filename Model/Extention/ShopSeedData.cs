namespace Model.Extention;
public class ShopSeedData
{
    public static void SeedData(ModelBuilder builder)
    {
        builder.Entity<Role>().HasData(
        new Role { Id = 1L, ConcurrencyStamp = "4776a1b2-dbe4-4056-82ec-8bed211d1454", Name = "admin", NormalizedName = "ADMIN" },
        new Role { Id = 2L, ConcurrencyStamp = "00d172be-03a0-4856-8b12-26d63fcf4374", Name = "customer", NormalizedName = "CUSTOMER" },
        new Role { Id = 3L, ConcurrencyStamp = "d4754388-8355-4018-b728-218018836817", Name = "guest", NormalizedName = "GUEST" },
        new Role { Id = 4L, ConcurrencyStamp = "71f10604-8c4d-4a7d-ac4a-ffefb11cefeb", Name = "vendor", NormalizedName = "VENDOR" }
        );

        builder.Entity<User>().HasData(
            new User
            {
                Id = 10L,
                AccessFailedCount = 0,
                ConcurrencyStamp = "101cd6ae-a8ef-4a37-97fd-04ac2dd630e4",
                CreatedOn = DateTime.Now,
                Email = "system@shop.com",
                EmailConfirmed = false,
                FullName = "System User",
                LockoutEnabled = false,
                NormalizedEmail = "SYSTEM@SHOP.COM",
                NormalizedUserName = "SYSTEM@SHOP.COM",
                PasswordHash = "AQAAAAEAACcQAAAAEJ90QpvSu/TBqD8+UzFWfTTU0zWPnkqNoU5qhCoPVI9/rJt1Lnbjy+lyRWQz/wYzwg==",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                UserName = "system@shop.com",
                SecurityStamp = "a9565acb-cee6-425f-9833-419a793f5fba",
                Guid = new Guid("5f72f83b-7436-4221-869c-1b69b2e23aae")
            });
        builder.Entity<UserRole>().HasData(
            new UserRole { UserId = 10, RoleId = 1 }
        );
        builder.Entity<Country>().HasData(
            new Country("VN") { Code3 = "VNM", Name = "Việt Nam", IsBillingEnabled = true, IsShippingEnabled = true, IsCityEnabled = false, IsZipCodeEnabled = false, IsDistrictEnabled = true },
            new Country("US") { Code3 = "USA", Name = "United States", IsBillingEnabled = true, IsShippingEnabled = true, IsCityEnabled = true, IsZipCodeEnabled = true, IsDistrictEnabled = false }
        );

        builder.Entity<StateOrProvince>().HasData(
            new StateOrProvince(1) { CountryId = "VN", Name = "Hồ Chí Minh", Type = "Thành Phố" },
            new StateOrProvince(2) { CountryId = "US", Name = "Washington", Code = "WA" }
        );

        builder.Entity<District>().HasData(
            new District(1) { Name = "Quận 1", StateOrProvinceId = 1, Type = "Quận" },
            new District(2) { Name = "Quận 2", StateOrProvinceId = 1, Type = "Quận" }
        );

        builder.Entity<Address>().HasData(
            new Address(1) { AddressLine1 = "Trieu Khuc-Thanh Tri-Ha Noi", ContactName = "Toan Dinh", CountryId = "VN", StateOrProvinceId = 1 }
        );
        builder.Entity<Warehouse>().HasData(
            new Warehouse(1) { Name = "Default warehouse", AddressId = 1 }
        );
        builder.Entity<EntityType>().HasData(
            new EntityType("Category") { RoutingController = "Category", RoutingAction = "CategoryDetail", IsMenuable = true },
            new EntityType("Brand") { RoutingController = "Brand", RoutingAction = "BrandDetail", IsMenuable = true },
            new EntityType("Product") { RoutingController = "Product", RoutingAction = "ProductDetail", IsMenuable = false },
            new EntityType("NewsCategory") { RoutingController = "NewsCategory", RoutingAction = "NewsCategoryDetail", IsMenuable = true },
            new EntityType("NewsItem") { RoutingController = "NewsItem", RoutingAction = "NewsItemDetail", IsMenuable = false },
            new EntityType("Page") { RoutingController = "Pages", RoutingAction = "PageDetail", IsMenuable = true }

        );
        builder.Entity<TaxClass>().HasData(new TaxClass(1) { Name = "Standard VAT" });
        builder.Entity<PaymentProvider>().HasData(
                new PaymentProvider("Braintree") { Name = "Brain tree", LandingViewComponentName = "BraintreeLanding", ConfigureUrl = "BraintreeConfig", IsEnabled = true, AdditionalSettings = "{\"PublicKey\": \"6j4d7qspt5n48kx4\", \"PrivateKey\" : \"bd1c26e53a6d811243fcc3eb268113e1\", \"MerchantId\" : \"ncsh7wwqvzs3cx9q\", \"IsProduction\" : \"false\" }" },
                new PaymentProvider("CashOnDelivery") { Name = "Cash On Delivery", LandingViewComponentName = "CoDLanding", ConfigureUrl = "CoDConfig", IsEnabled = true, AdditionalSettings = "{\"MinOrderValue\": \"1\",\"MaxOrderValue\":\"10000000\",\"PaymentFee\":\"2\"}" },
                new PaymentProvider("MomoPayment") { Name = "Momo Payment", LandingViewComponentName = "MomoLanding", ConfigureUrl = "MomoConfig", IsEnabled = true, AdditionalSettings = "{\"IsSandbox\":\"true\",\"PartnerCode\":\"MOMOIQA420180417\",\"AccessKey\":\"SvDmj2cOTYZmQQ3H\",\"SecretKey\":\"PPuDXq1KowPT1ftR8DvlQTHhC03aul17\",\"PaymentFee\":\"0.0\"}" },
                new PaymentProvider("NganLuongPayment") { Name = "Ngan Luong Payment", LandingViewComponentName = "NganLuongLanding", ConfigureUrl = "NganLuongConfig", IsEnabled = true, AdditionalSettings = "{\"IsSandbox\":\"true\", \"MerchantId\": \"47249\", \"MerchantPassword\": \"e530745693dbde678f9da98a7c821a07\", \"ReceiverEmail\": \"dinhmanhtoan1998@gmail.com\"}" },
                new PaymentProvider("PaypalExpress") { Name = "Paypal Express", LandingViewComponentName = "PaypalExpressLanding", ConfigureUrl = "PaypalExpressConfig", IsEnabled = true, AdditionalSettings = "{\"IsSandbox\":\"true\", \"ClientId\":\"\",\"ClientSecret\":\"\" }" },
                new PaymentProvider("Stripe") { Name = "Stripe", LandingViewComponentName = "StripeLanding", ConfigureUrl = "StripeConfig", IsEnabled = true, AdditionalSettings = "{\"PublicKey\": \"pk_test_6pRNASCoBOKtIshFeQd4XMUh\", \"PrivateKey\" : \"sk_test_BQokikJOvBiI2HlWgH4olfQ2\"}" }

        );
        builder.Entity<ShippingProvider>().HasData(
            new ShippingProvider("FreeShip") { Name = "Free Ship", IsEnabled = true, ConfigureUrl = "", ShippingPriceServiceTypeName = "Model.Services.FreeShippingServiceProvider,Model", AdditionalSettings = "{MinimumOrderAmount : 1}", ToAllShippingEnabledCountries = true, ToAllShippingEnabledStatesOrProvinces = true },
            new ShippingProvider("TaxRate") { Name = "Tax Rate", IsEnabled = true, ConfigureUrl = "shipping-table-rate-config", ShippingPriceServiceTypeName = "Model.Services.TableRateShippingServiceProvider,Model", AdditionalSettings = "", ToAllShippingEnabledCountries = true, ToAllShippingEnabledStatesOrProvinces = true }


            );
        builder.Entity<WidgetZone>().HasData(
          new WidgetZone(1) { Name = "Home Featured" },
          new WidgetZone(2) { Name = "Home Main Content" },
          new WidgetZone(3) { Name = "Home After Main Content" }
      );
        builder.Entity<Activity>().HasIndex(x => x.ActivityTypeId);

        builder.Entity<ActivityType>().HasData(new ActivityType(1) { Name = "EntityView" });

        builder.Entity<Menu>().HasData(
            new Menu(1) { Name = "Customer Services", IsPublished = true, IsSystem = true },
            new Menu(2) { Name = "Information", IsPublished = true, IsSystem = true }
        );
    }
}

