using EL.BlackList.API.Data;
using EL.BlackList.API.IdentityAuth;
using EL.BlackList.API.Repositore.Interfaces;
using EL.BlackList.API.Repositore.Repositore;
using EL.BlackList.API.Services.Implementations;
using EL.BlackList.API.Services.Interfaces;
using EL.BlackList.API.Services.Repositore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace EL.BlackList.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigurationManager configuration = builder.Configuration;

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JWTAuht", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer'[space] and then you valid token in the text input below.\r\n\r\nExaple: \"Bearer agfasgsdgsdghsthshsghsrth\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference =new OpenApiReference
                            {
                                Type =ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer
               (builder.Configuration.GetConnectionString("CommanderConnection")));
            builder.Services.AddDbContext<IdentitysDbContext>(opt => opt.UseSqlServer
               (builder.Configuration.GetConnectionString("CommanderConnection")));


            builder.Services.AddIdentity<AppUser, IdentityRole>(option =>
            {
                option.SignIn.RequireConfirmedEmail = false;
                option.SignIn.RequireConfirmedPhoneNumber = false;
                option.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ123456789-._@+";
                option.Password.RequireDigit = true;
                option.Password.RequiredLength = 6;
                option.Password.RequireLowercase = true;
                option.Password.RequireNonAlphanumeric = true;
                option.Password.RequireUppercase = true;
            })
               .AddEntityFrameworkStores<IdentitysDbContext>()
               .AddDefaultTokenProviders();
            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(option =>
                {
                    option.SaveToken = true;
                    option.RequireHttpsMetadata = false;
                    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        // указывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,

                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,

                        // будет ли валидироваться время существования
                        ValidateLifetime = true,

                        // строка, представляющая издателя
                        ValidIssuer = configuration["JWT:ValidIssuer"],

                        // установка потребителя токена
                        ValidAudience = configuration["JWT:ValidAudience"],

                        // валидация ключа безопасности
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]))
                    };
                });


            InitDevDi(builder);

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

            static void InitDevDi(WebApplicationBuilder builder)
            {
                builder.Services.AddScoped<IDriversRepositore, DriversRepositore>();
                builder.Services.AddScoped<IDriversServices, DriversServices>();

                builder.Services.AddScoped<IFeedBacksRepositore, FeedBacksRepositore>();
                builder.Services.AddScoped<IFeedBackServices, FeedBackServices>();

                builder.Services.AddScoped<ICityRepositore, CityRepositore>();
                builder.Services.AddScoped<ICityServices, CityServices>();

                builder.Services.AddScoped<ITaxiPoolRepositore, TaxiPoolRepositore>();
                builder.Services.AddScoped<ITaxiPoolServices, TaxiPoolServices>();

                builder.Services.AddScoped<IDocumentRepositore, DocumentsRepositore>();
                builder.Services.AddScoped<IDocumentsService, DocumentsServices>();

                builder.Services.AddScoped<IAuthenticationUserServices, AuthenticationUserServices>();

            }
        }
    }
}