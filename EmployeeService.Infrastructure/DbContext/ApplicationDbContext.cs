using System.Reflection.Emit;
using System.Text.Json;
using EmployeeService.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace EmployeeService.Infrastructure.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeMedia> EmployeeMedias { get; set; }
        public DbSet<Relative> Relatives { get; set; }
        public DbSet<EmployeeContract> EmployeeContracts { get; set; }
        public DbSet<Education> Educations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            // Seed data
            var employees = LoadSeedData<Employee>("SeedData/Employees.json");
            builder.Entity<Employee>().HasData(employees);

            var employeeMedias = LoadSeedData<EmployeeMedia>("SeedData/EmployeeMedias.json");
            builder.Entity<EmployeeMedia>().HasData(employeeMedias);

            var relatives = LoadSeedData<Relative>("SeedData/Relatives.json");
            builder.Entity<Relative>().HasData(relatives);

            var employeeContracts = LoadSeedData<EmployeeContract>("SeedData/EmployeeContracts.json");
            builder.Entity<EmployeeContract>().HasData(employeeContracts);
            var educations = LoadSeedData<Education>("SeedData/Educations.json");
            builder.Entity<Education>().HasData(educations);
        }

        private static List<T> LoadSeedData<T>(string filePath)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string projectRoot = Directory.GetParent(baseDirectory).Parent.Parent.Parent.Parent.FullName;
            string fullPath = Path.Combine(projectRoot, "EmployeeService.Infrastructure", filePath);


            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"Không tìm thấy file seed data: {fullPath}");

            var jsonData = File.ReadAllText(fullPath);
            var items = JsonSerializer.Deserialize<List<T>>(jsonData) ?? new List<T>();

            return items;
        }

    }
}
