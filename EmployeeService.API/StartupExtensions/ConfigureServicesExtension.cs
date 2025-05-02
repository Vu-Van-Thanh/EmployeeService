using EmployeeService.Core.RepositoryContracts;
using EmployeeService.Core.Services;
using EmployeeService.Infrastructure.AppDbContext;
using EmployeeService.Infrastructure.Kafka;
using EmployeeService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using EmployeeService.Core.Extension;
using EmployeeService.API.Kafka.Producer;
using EmployeeService.Infrastructure.Kafka.Consumers;
using EmployeeService.Core.DTO;
using EmployeeService.Infrastructure.Kafka.Handlers;
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

            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IEmployeeService, EmployeeServices>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeMediaRepository, EmployeeMediaRepository>();
            services.AddScoped<IEmployeeMediaService, EmployeeMediaService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IRelativeRepository, RelativeRepository>();
            services.AddScoped<IRelativeService, RelativeService>();
            services.AddScoped<IEmployeeContractRepository, EmployeeContractRepository>();
            services.AddScoped<IEmployeeContractService, EmployeeContractService>();
            services.AddScoped<IEventProducer, EmployeeProducer>();
            services.AddScoped<IKafkaHandler<KafkaRequest<EmployeeFilterDTO>>, GetEmployeeHandler>();
            services.AddScoped<IKafkaHandler<KafkaRequest<StartImportEmployee>>, EmployeeImportHandler>();
            services.AddScoped<IEventProducer, EmployeeProducer>();
            services.Configure<KafkaSettings>(configuration.GetSection("Kafka"));
           // services.AddHostedService<EmployeeConsumer>();  // Đăng ký EmployeeConsumer như một background service

            //services.AddHostedService<EmployeeConsumer>();

            // Cấu hình corse
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()      
                          .AllowAnyMethod()      
                          .AllowAnyHeader();     
                });
            });


            // cấu hình swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Employee Service API",
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
