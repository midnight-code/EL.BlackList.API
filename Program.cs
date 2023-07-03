using EL.BlackList.API.Data;
using EL.BlackList.API.Services.Repositore;
using Microsoft.EntityFrameworkCore;

namespace EL.BlackList.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer
               (builder.Configuration.GetConnectionString("CommanderConnection")));

            builder.Services.AddTransient<IDriversRepositore, DriversRepositore>();
            builder.Services.AddTransient<IFeedBacksRepositore, FeedBacksRepositore>();

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
        }
    }
}