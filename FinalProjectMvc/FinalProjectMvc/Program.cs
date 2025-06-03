using FinalProjectMvc.Data;
using FinalProjectMvc.Models;
using FinalProjectMvc.Services.Interfaces;
using FinalProjectMvc.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using FinalProjectMvc.Services.Implementations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var conString = builder.Configuration.GetConnectionString("Default") ??
    throw new InvalidOperationException("Connection string 'Default' not found.");

// Configure DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(conString));

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // User settings.
    options.User.RequireUniqueEmail = true;
});


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

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    // .WriteTo.Seq("http://localhost:5341") 
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Services.AddAutoMapper(typeof(Program));

builder.Host.UseSerilog();


var app = builder.Build();

// Global Exception Middleware ?lav? olunur
//app.UseMiddleware<FinalProjectMvc.Middlewares.GlobalExceptionHandlerMiddleware>();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
   name: "areas",
   pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
