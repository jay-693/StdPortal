using BusinessLayer.Services;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Portal;
using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext for the application
builder.Services.AddDbContext<StudentPortalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/SignUp/Login"; 
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
    });

builder.Services.AddAuthorization();
builder.Services.AddScoped<IAcademicService, AcademicService>();
builder.Services.AddScoped<IBtechService, BtechService>();
builder.Services.AddScoped<IMbaService, MbaService>();
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<IClassAdminRepository, ClassAdminRepository>();
builder.Services.AddScoped<IBtechRepository, BtechRepository>();
builder.Services.AddScoped<IMbaRepository, MbaRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure the session storage to use distributed SQL cache
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true; 
    options.Cookie.IsEssential = true; 
});

var app = builder.Build();

RotativaConfiguration.Setup(app.Environment.WebRootPath);

// Apply automatic migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<StudentPortalDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Add middleware for session
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=HomePage}/{id?}");

app.Run();
