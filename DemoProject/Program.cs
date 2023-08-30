using DemoProject.Data;
using DemoProject.Services.Abstraction;
using DemoProject.Services.Implementation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DemoProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DemoProjectContext")));
builder.Services.AddScoped<IRegistrationService, RegistrationService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

builder = WebApplication.CreateBuilder(args);


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
