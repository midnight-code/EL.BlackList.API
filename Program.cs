using EL.BlackList.API.Data;
using EL.BlackList.API.Repositore.Interfaces;
using EL.BlackList.API.Repositore.Repositore;
using EL.BlackList.API.Services.Implementations;
using EL.BlackList.API.Services.Interfaces;
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

            builder.Services.AddScoped<IDriversRepositore, DriversRepositore>();
            builder.Services.AddScoped<IDriversServices, DriversServices>();    

            builder.Services.AddScoped<IFeedBacksRepositore, FeedBacksRepositore>();
            builder.Services.AddScoped<IFeedBackServices, FeedBackServices>();

            builder.Services.AddScoped<ICityRepositore, CityRepositore>();
            builder.Services.AddScoped<ICityServices, CityServices>();

            builder.Services.AddScoped<ITaxiPoolRepositore, TaxiPoolRepositore>();
            builder.Services.AddScoped<ITaxiPoolServices, TaxiPoolServices>();

            //builder.Services.AddTransient<IDriversRepositore, DriversRepositore>();
            //builder.Services.AddTransient<IFeedBacksRepositore, FeedBacksRepositore>();
            //builder.Services.AddTransient<ITaxiPoolRepositore, TaxiPoolRepositore>();

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