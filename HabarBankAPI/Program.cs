
using HabarBankAPI.Application.Interfaces;
using HabarBankAPI.Application.Services;
using HabarBankAPI.Data;
using HabarBankAPI.Domain.Entities.Security;
using HabarBankAPI.Infrastructure.Repositories;
using HabarBankAPI.Infrastructure.Share;
using HabarBankAPI.Infrastructure.Uow;
using HabarBankAPI.Web.Services;
using Microsoft.EntityFrameworkCore;

namespace HabarBankAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            _ = builder.Services.AddControllers();

            builder.Services.AddApiVersioning();

            _ = builder.Services.AddEndpointsApiExplorer();
            _ = builder.Services.AddSwaggerGen();

            WebApplication app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                _ = app.UseSwagger();
                _ = app.UseSwaggerUI();
            }

            _ = app.UseHttpsRedirection();

            _ = app.UseAuthorization();

            AppInitializer.Init();

            _ = app.MapControllers();

            app.Run();
        }
    }
}