using EmployeeService.Core.RepositoryContracts;
using EmployeeService.Core.Services;
using EmployeeService.Infrastructure.AppDbContext;
using EmployeeService.Infrastructure.MessageBroker;
using EmployeeService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
namespace EmployeeServiceRegistry
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });


            // thêm service
            services.AddScoped<IEmployeeService, EmployeeServices>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeMediaRepository, EmployeeMediaRepository>();
            services.AddScoped<IEmployeeMediaService, EmployeeMediaService>();
            services.AddSingleton<IMessageProducer, KafkaProducerService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IRelativeRepository, RelativeRepository>();
            services.AddScoped<IRelativeService, RelativeService>();

            // cấu hình swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "User Service API",
                    Version = "v1",
                    Description = "API quản lý người dùng trong hệ thống microservices",
                    Contact = new OpenApiContact
                    {
                        Name = "Hỗ trợ API",
                        Email = "support@example.com",
                        Url = new Uri("https://example.com")
                    }
                });
     
            });

            return services;
        }
    }
}
