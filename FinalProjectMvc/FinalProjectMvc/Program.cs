using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.Services;
using FinalProjectMvc.Services.Implementations;
using FinalProjectMvc.Helpers.Seeders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;
using Serilog;
using FinalProjectMvc.Helpers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using FinalProjectMvc.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Email config
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailConfig"));

// MVC
builder.Services.AddControllersWithViews();

// ? Multipart (upload) limit - 100MB
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100MB
});

// DB connection
var conString = builder.Configuration.GetConnectionString("Default")
    ?? throw new InvalidOperationException("Connection string 'Default' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(conString));

// Identity config
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
    options.User.RequireUniqueEmail = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Unauthorized/Index";
});

// ?? DI Services
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<IScrollingService, ScrollingService>();
builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IServiceItemService, ServiceItemService>();
builder.Services.AddScoped<ITopbarService, TopbarService>();
builder.Services.AddScoped<IAboutBannerService, AboutBannerService>();
builder.Services.AddScoped<IBlogBannerService, BlogBannerService>();
builder.Services.AddScoped<IMenuBannerService, MenuBannerService>();
builder.Services.AddScoped<ITeamBannerService, TeamBannerService>();
builder.Services.AddScoped<IFaqsBannerService, FaqsBannerService>();
builder.Services.AddScoped<IContactBannerService, ContactBannerService>();
builder.Services.AddScoped<IBlogDetailBannerService, BlogDetailBannerService>();
builder.Services.AddScoped<IReservationBannerService, ReservationBannerService>();
builder.Services.AddScoped<ITeamMemberService, TeamMemberService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IAboutRestaurantService, AboutRestaurantService>();
builder.Services.AddScoped<ILayoutService, LayoutService>();
builder.Services.AddScoped<IApproachService, ApproachService>();
builder.Services.AddScoped<IApproachItemService, ApproachItemService>();
builder.Services.AddScoped<ISettingService, SettingService>();
builder.Services.AddScoped<IContactUsService, ContactUsService>();
builder.Services.AddScoped<IContactMessageService, ContactMessageService>();
builder.Services.AddHostedService<ContactMessageCleanupService>();
builder.Services.AddScoped<IOurStoryService, OurStoryService>();
builder.Services.AddScoped<IStoryItemService, StoryItemService>();
builder.Services.AddScoped<IIntroVideoService, IntroVideoService>();
builder.Services.AddScoped<IIntroCounterService, IntroCounterService>();
builder.Services.AddScoped<IAboutUsService, AboutUsService>();
builder.Services.AddScoped<IAboutUsItemService, AboutUsItemService>();
builder.Services.AddScoped<IOpeningHourService, OpeningHourService>();
builder.Services.AddScoped<IOurOfferService, OurOfferService>();
builder.Services.AddScoped<IOfferItemService, OfferItemService>();
builder.Services.AddScoped<IOfferImageService, OfferImageService>();
builder.Services.AddScoped<IFaqCategoryService, FaqCategoryService>();
builder.Services.AddScoped<IFaqItemService, FaqItemService>();
builder.Services.AddScoped<ICategoryTypeService, CategoryTypeService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISizeService, SizeService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<ISubscribeService, SubscribeService>();
builder.Services.AddHostedService<ReservationCleanupService>();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

// Role seeding
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await RoleSeeder.SeedAsync(roleManager);
}

// Middleware
// app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

// Custom error redirection
app.UseStatusCodePages(context =>
{
    var response = context.HttpContext.Response;
    var path = response.StatusCode switch
    {
        400 => "/BadRequest/Index",
        401 => "/Unauthorized/Index",
        404 => "/NotFound/Index",
        _ => null
    };

    if (path != null)
    {
        response.Redirect(path);
    }

    return Task.CompletedTask;
});

app.UseAuthorization();

// Area routing
app.MapControllerRoute(
   name: "areas",
   pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

// Default routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
