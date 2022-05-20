using Microsoft.Extensions.Localization;
using Microsoft.Extensions.WebEncoders;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);
ConfigureService();
var app = builder.Build();
Configure();
app.Run();

void ConfigureService(){

    builder.Services.AddMvc
        (o =>
        {
            o.EnableEndpointRouting = false;
            o.ModelBinderProviders.Insert(0, new InvariantDecimalModelBinderProvider());
        })
        .AddJsonOptions(options =>
        options.JsonSerializerOptions.PropertyNamingPolicy = null).AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        );
    builder.Services.AddDbContext<shopContext>(options =>
        options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.AddIdentity<User, Role>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 4;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequiredUniqueChars = 0;
        //  options.ClaimsIdentity.UserNameClaimType = JwtRegisteredClaimNames.Sub;
    })
    .AddEntityFrameworkStores<shopContext>()
        .AddRoleStore<ShopRoleStore>()
        .AddUserStore<ShopUserStore>()
        .AddSignInManager<ShopSignInManager<User>>()
        .AddDefaultTokenProviders();

    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.AccessDeniedPath = new PathString("/");
        options.Cookie.Name = "shop";
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        options.LoginPath = "/login";
        // ReturnUrlParameter requires 
        //using Microsoft.AspNetCore.Authentication.Cookies;
        options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
        options.SlidingExpiration = true;
    });

    //builder.Services.Configure<RazorViewEngineOptions>(
    //options => { options.ViewLocationExpanders.Add(new ThemeableViewLocationExpander());
    //});

    builder.Services.Configure<WebEncoderOptions>(options =>
    {
        options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
    });

   // builder.Services.AddScoped<ITagHelperComponent, LanguageDirectionTagHelperComponent>();
    builder.Services.AddTransient<IRazorViewRenderer, RazorViewRenderer>();
    builder.Services.AddTransient<IRazorViewRenderer, RazorViewRenderer>();
    builder.Services.AddTransient<IMediaService, MediaService>();
    builder.Services.AddTransient<IStorageService, LocalStorageService>();
    builder.Services.AddTransient<IProductService, ProductService>();
    builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
    builder.Services.AddTransient<ICategoryService, CategoryService>();
    builder.Services.AddTransient<IBrandService, BrandService>();
    builder.Services.AddScoped<IWishListService, WishListService>();
    builder.Services.AddTransient<IEntityService, EntityService>();
    builder.Services.AddTransient<IStockService, StockService>();
    builder.Services.AddTransient<IOrderService, OrderService>();
    builder.Services.AddTransient<ICouponService, CouponService>();
    builder.Services.AddTransient<ICurrencyService, CurrencyService>();
    builder.Services.AddTransient<IContentLocalizationService, ContentLocalizationService>();
    builder.Services.AddTransient<IProductPricingService, ProductPricingService>();
    builder.Services.AddTransient<IShippingPriceService, ShippingPriceService>();
    builder.Services.AddTransient<IShippingPriceServiceProvider, FreeShippingServiceProvider>();
    builder.Services.AddTransient<IShippingPriceServiceProvider, TableRateShippingServiceProvider>();
    builder.Services.AddTransient<IShipmentService, ShipmentService>();
    builder.Services.AddTransient<ITaxService, TaxService>();
    builder.Services.AddTransient<IPageService, PageService>();
    builder.Services.AddTransient<INewsItemService, NewsItemService>();
    builder.Services.AddTransient<INewsCategoryService, NewsCategoryService>();
    builder.Services.AddTransient<IWidgetInstanceService, WidgetInstanceService>();

    ///builder.Services.AddTransient<IEmailSender, EmailSender>();
    builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
    builder.Services.AddTransient(typeof(IRepositoryWithTypedId<,>), typeof(RepositoryWithTypedId<,>));
    builder.Services.AddTransient<IActivityTypeRepository, ActivityTypeRepository>();
    builder.Services.AddScoped<SlugRouteValueTransformer>();
    builder.Services.AddTransient<IWorkContext, WorkContext>();
    builder.Services.AddTransient<ICartService, CartService>();
    builder.Services.AddAntiforgery(options =>
    {
        options.HeaderName = "X-XSRF-Token";
    });

    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.Cookie.SameSite = SameSiteMode.None;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.IsEssential = true;
        });

    builder.Services.AddSession(options =>
    {
        options.Cookie.SameSite = SameSiteMode.None;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.IsEssential = true;
    });

    builder.Services.AddScoped<ServiceFactory>(p => p.GetService);
    builder.Services.AddScoped<IMediator, Mediator>();
    builder.Services.AddTransient<INotificationHandler<UserSignedIn>, UserSignedInHandler>();
    builder.Services.AddTransient<INotificationHandler<EntityViewed>, EntityViewedHandler>();
    builder.Services.AddTransient<INotificationHandler<EntityViewed>, ActiveViewdHandler>();
    builder.Services.AddTransient<INotificationHandler<ReviewSummaryChanged>, ReviewSummaryChangedHandler>();
    builder.Services.AddTransient<INotificationHandler<OrderChanged>, OrderChangedCreateOrderHistoryHandler>();
    builder.Services.AddTransient<INotificationHandler<OrderCreated>, OrderCreatedCreateOrderHistoryHandler>();

}
void Configure(){
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
       // app.UseExceptionHandler("/Home/Error");
        app.UseStatusCodePagesWithRedirects("/Home/ErrorWithCode/{0}");

        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseCookiePolicy();
    app.UseAuthentication();
    app.UseSession();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapDynamicControllerRoute<SlugRouteValueTransformer>("/{**slug}");
        endpoints.MapControllerRoute(
           name: "Areas",
           pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });
    app.Run(context =>
    {
        context.Response.StatusCode = 404;
        return Task.FromResult(0);
    });
}

  
 


