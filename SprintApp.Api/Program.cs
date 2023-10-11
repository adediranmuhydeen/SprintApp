using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SprintApp.Core.IRepositories;
using SprintApp.Core.IServices;
using SprintApp.Infrastructure.Data;
using SprintApp.Infrastructure.Repositories;
using SprintApp.Services.Services;
using SprintApp.UI.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultString")));
builder.Services.AddScoped<IProjectManagerService, ProjectManagerService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
IMapper mapper = (new MapperConfiguration(x=> x.AddProfile(new MapInitializer()))).CreateMapper();

builder.Services.AddSingleton(mapper);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
