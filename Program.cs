
using ConfigurationLayer.ConfigurationServiceAuthentication;
using ConfigurationLayer.MapAppSettings;
using DBLayer.DAO.DAOImpl;
using DBLayer.DAO.IRepository;
using DBLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServiceLayer.AutoMapper;
using ServiceLayer.IService;
using ServiceLayer.Service.ServiceImp;
using System.Text;

namespace Northwind
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigurationManager configuration = builder.Configuration;

            // Add services to the container.
            


            //context
            builder.Services.AddDbContext<NorthwindDefContext>();

            //DI repositories
            builder.Services.AddScoped<IRepositoryEmployee, EmployeeRepository>();
            builder.Services.AddScoped<IRepositoryOrderr, OrderrRepository>();
            builder.Services.AddScoped<IRepositoryProduct, ProductRepository>();
            builder.Services.AddScoped<IRepositorySupplier, SupplierRepository>();
            builder.Services.AddScoped<IMapperConfig, MapperConfig>();

            //DI ConfigurationServices
            builder.Services.AddScoped<IConfigurationServiceAuthentication, ConfigurationServiceAuthentication>();

            //DI Services
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IOrderrService, OrderService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ISupplierService, SupplierService>();

            

            IConfigurationBuilder config = builder.Configuration.AddJsonFile("appsettings.json");

            //adding configuration options for JWAuth
            JWTAuthSettings optionsInstance = configuration.GetSection("JWT").Get<JWTAuthSettings>(); //ho istanziato un parametro
            IOptions<JWTAuthSettings> optionParameter = Options.Create(optionsInstance);

            ConfigurationServiceAuthentication configAuth = ConfigurationServiceAuthentication.GetIstance(optionParameter);

            // Adding Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configAuth.OnGetValidAudience(),
                    ValidIssuer = configAuth.OnGetValidIssuer(),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configAuth.OnGetSecretKey()))
                };
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MS Northwind API", Version = "v1" });
                c.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Type = SecuritySchemeType.Http,
                        Scheme = JwtBearerDefaults.AuthenticationScheme
                    });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                          Reference = new OpenApiReference
                          {
                              Id = JwtBearerDefaults.AuthenticationScheme,
                              Type = ReferenceType.SecurityScheme
                          }
                        }
                        ,new List<string>()
                    }
                });
            });

            var app = builder.Build();

            app.Services.GetRequiredService<IOptions<JWTAuthSettings>>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Authentication & Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}