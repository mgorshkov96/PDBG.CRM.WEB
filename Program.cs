using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.EntityFrameworkCore;
using PDBG.CRM.WEB.Models;
using PDBG.CRM.WEB.Models.Repositories;

var builder = WebApplication.CreateBuilder(args);
string? connection = builder.Configuration.GetConnectionString("Default");
string? connectionIdentity = builder.Configuration.GetConnectionString("Identity");

// Add services to the container.
builder.Services.AddDbContext<PDBGContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 33))));
builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseMySql(connectionIdentity, new MySqlServerVersion(new Version(8, 0, 33))));

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IAgentStateRepository, EFAgentStateRepository>();
builder.Services.AddTransient<ILeadRepository, EFLeadRepository>();
builder.Services.AddTransient<ILocationLogRepository, EFLocationLogRepository>();
builder.Services.AddTransient<IEmployeeRepository, EFEmployeeRepository>();
builder.Services.AddTransient<IAgentSearchRepository, EFAgentSearchRepository>();
builder.Services.AddTransient<IAmoAuthRepository, EFAmoAuthRepository>(); 
builder.Services.AddTransient<IClientRepository, EFClientRepository>();
builder.Services.AddTransient<IApiKeyRepository, EFApiKeyRepository>();
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
app.UseStatusCodePages();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Map}/{action=AgentsOnMap}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
