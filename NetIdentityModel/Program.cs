
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NetIdentityModel.Data.DbContext;
using NetIdentityModel.Models;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();

// For Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>( options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))); 

// For Identity
builder.Services.AddIdentity<ApplicationUser, //IdentityUser, precisa usar mesmo data model(customizada), não identityuser original(DI Native Funcionar)
                             IdentityRole>()
                             .AddEntityFrameworkStores<ApplicationDbContext>()
                             .AddDefaultTokenProviders();

// Adding Authentication
// Adding Jwt Bearer
// configure DI for application services

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) //colocar !app. para mostrar erro de producao
{
    //alterado para mostrar erros em produção
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

