global using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SprintApp.Core.IRepositories;
using SprintApp.Core.IServices;
using SprintApp.Infrastructure.Data;
using SprintApp.Infrastructure.Repositories;
using SprintApp.Services.Services;
using SprintApp.UI.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultString")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
IMapper mapper = (new MapperConfiguration(x => x.AddProfile(new MapInitializer()))).CreateMapper();
builder.Services.AddScoped<IProjectManagerService, ProjectManagerService>();
builder.Services.AddSingleton(mapper);

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
