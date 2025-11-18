using Access.Application.Interfaces;
using Access.Application.Services;
using App.Infrastructure.Data;
using Card.Application.Interfaces;
using Card.Application.Services;
using Common.Constants;
using Common.Filters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Operations.Application.Interfaces;
using Operations.Application.Services;
using System;
using Users.Application.Interfaces;
using Users.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromDays(200);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
        options.Cookie.Name = Constants.AuthCookieName;
        // options.Cookie.HttpOnly = true; // Prevent XSS attacks - cookie not accessible from JavaScript
        // options.Cookie.SameSite = SameSiteMode.Lax; // CSRF protection
        // options.Cookie.Secure = builder.Environment.IsProduction() 
        //     ? CookieSecurePolicy.Always  // Require HTTPS in production
        //     : CookieSecurePolicy.SameAsRequest; // Allow HTTP in development
    });

builder.Services.AddAuthorization(options =>
{
    // ��������� ������ � Swagger ��� �����������
    options.FallbackPolicy = null; // �� ������� ����������� �� ���������
});

// Add Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register IDbContext
builder.Services.AddScoped<Common.Abstracts.IDbContext>(provider =>
    provider.GetRequiredService<ApplicationDbContext>());

// Add module services
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<IOperationService, OperationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccessService, AccessService>();

// Add services to the container.

builder.Services.AddControllers(options =>
    {
        options.Filters.Add<ApiExceptionFilter>();
    })
    .AddApplicationPart(typeof(Users.Application.Interfaces.IUserService).Assembly)
    .AddApplicationPart(typeof(Card.Application.Interfaces.ICardService).Assembly)
    .AddApplicationPart(typeof(Operations.Application.Interfaces.IOperationService).Assembly)
    .AddApplicationPart(typeof(Access.Application.Interfaces.IAccessService).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
