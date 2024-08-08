using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.BusinessLayer.Concrete;
using EVillaAgency.DataAccessLayer.Context;
using EVillaAgency.EntityLayer.Concrete;
using EVillaAgency.WebAPI.AutoMapper;
using EVillaAgency.WebAPI.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(opt =>
{
	opt.AddPolicy("CorsPolicy", builder =>
	{
		builder.AllowAnyHeader()
		.AllowAnyMethod()
		.SetIsOriginAllowed((host) => true)
		.AllowCredentials();
	});
});

builder.Services.AddSignalR();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
builder.Services.AddScoped<AppDbContext>();
builder.Services.AddScoped<IImageService,ImageManager>();
builder.Services.AddScoped<IFavoriteService,FavoriteManager>();
builder.Services.AddScoped<IHouseService,HouseManager>();
builder.Services.AddScoped<IHouseImageService, HouseImageManager>();
builder.Services.AddScoped<IHouseTypeService,HouseTypeManager>();
builder.Services.AddScoped<IDistrictService,DistrictManager>();
builder.Services.AddScoped<ICityService,CityManager>();
builder.Services.AddScoped<IHeatingTypeService,HeatingTypeManager>();
builder.Services.AddScoped<INotificationService,NotificationManager>();
builder.Services.AddScoped<IAppUserService,AppUserManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHub<SignalRHub>("/signalrhub");


app.MapControllers();

app.Run();

//localhost://1234/swagger/category/index
//localhost://1234/signalrhub