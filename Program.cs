using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using AppRH.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppRHContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppRHContext") ?? throw new InvalidOperationException("Connection string 'AppRHContext' not found.")));

builder.Services.AddDbContext<AppRHIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppRHIdentityDbContext") ?? throw new InvalidOperationException("Connection string 'AppRHIdentityDbContext' not found.")));    

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppRHIdentityDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<IdentityOptions>(options =>
 {
     // Password settings.
     options.Password.RequireDigit = false;
     options.Password.RequireLowercase = false;
     options.Password.RequireNonAlphanumeric = true;
     options.Password.RequireUppercase = false;
     options.Password.RequiredLength = 6;
     options.Password.RequiredUniqueChars = 0;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
