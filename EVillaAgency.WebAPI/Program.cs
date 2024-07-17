using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.BusinessLayer.Concrete;
using EVillaAgency.DataAccessLayer.Context;
using EVillaAgency.WebAPI.AutoMapper;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
builder.Services.AddScoped<AppDbContext>();
builder.Services.AddScoped<IImageService,ImageManager>();
builder.Services.AddScoped<IFavoriteService,FavoriteManager>();
builder.Services.AddScoped<IHouseService,HouseManager>();
builder.Services.AddScoped<IHouseImageService,HouseImageManager>();
builder.Services.AddScoped<IHouseTypeService,HouseTypeManager>();
builder.Services.AddScoped<IUserService,UserManager>();

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
