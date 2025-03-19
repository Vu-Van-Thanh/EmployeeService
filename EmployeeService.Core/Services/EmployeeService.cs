using EmployeeService.Core.Domain.Entities;
using EmployeeService.Core.DTO;
using EmployeeService.Core.Enums;
using EmployeeService.Core.RepositoryContracts;
using EmployeeService.Infrastructure.MessageBroker;

namespace EmployeeService.Core.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeInfo> GetEmployeeById(Guid Id);
        Task<EmployeeUpdateResponse> UpdateEmployee(EmployeeUpdateRequest employee, Guid EmployeeId);
        Task<List<EmployeeInfo>> GetAllEmployees();
        Task<bool> AddEmployee(EmployeeAddRequest employee);
        Task<Guid> GetEmployeeIdByUserId(Guid Id);
        Task<bool> DeleteEmployee(Guid employeeId);

    }
    public class EmployeeServices : IEmployeeService
    {
        private readonly IEmployeeRepository _employeesRepository;
        private readonly KafkaProducerService _kafkaProducer;

        public EmployeeServices(IEmployeeRepository employeesRepository, KafkaProducerService kafkaProducer)
        {
            _employeesRepository = employeesRepository;
            _kafkaProducer = kafkaProducer;
        }

        public async Task<bool> AddEmployee(EmployeeAddRequest employee)
        {
            Employee newEmployee = new Employee
            {
                EmployeeID = Guid.NewGuid(),
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                AccountID = employee.AccountId,
                DepartmentID = employee.DepartmentId,
                Gender = employee.Gender == "Nam" ? GenderOptions.Male : GenderOptions.Female,
                Tax = employee.Tax,
                Address = employee.Address,
                Nationality = employee.Nationality,
                Ethnic = employee.Ethnic,
                Religion = employee.Religion,
                PlaceOfBirth = employee.PlaceOfBirth,
                IndentityCard = employee.IndentityCard,
                PlaceIssued = employee.PlaceIssued,
                Country = employee.Country,
                Province = employee.Province,
                District = employee.District,
                Commune = employee.Commune,
                InsuranceNumber = employee.InsuranceNumber

            };
            return await _employeesRepository.AddEmployee(newEmployee);
        }

        public async  Task<bool> DeleteEmployee(Guid employeeId)
        {
            return await _employeesRepository.DeleteEmployee(employeeId);
        }

        public async Task<List<EmployeeInfo>> GetAllEmployees()
        {
            List<Employee> employee = await _employeesRepository.GetAll();
            List<EmployeeInfo> employeeInfo = new List<EmployeeInfo>();
            foreach (var item in employee)
            {
                employeeInfo.Add(item.ToEmployeeInfo());
            }
            return employeeInfo;
        }

        public async Task<EmployeeInfo> GetEmployeeById(Guid Id)
        {
           Employee employee = await _employeesRepository.GetEmployeeById(Id);
            if (employee == null) return null;
            return employee.ToEmployeeInfo();
        }

        public async Task<Guid> GetEmployeeIdByUserId(Guid Id)
        {
            Employee? employee = await _employeesRepository.GetEmployeeIdByUserId(Id);
            if(employee != null)
            {
                return employee.EmployeeID;
            }
            return Guid.Empty;
        }

        public async Task<EmployeeUpdateResponse> UpdateEmployee(EmployeeUpdateRequest employee, Guid EmployeeId)
        {
            Employee exist = await _employeesRepository.GetEmployeeById(EmployeeId);
            if (exist == null) throw new Exception("employee not found.");

            // Cập nhật Gender
            if (!string.IsNullOrEmpty(employee.Gender) && Enum.TryParse<GenderOptions>(employee.Gender, true, out var gender))
            {
                exist.Gender = gender;
            }

            if (!string.IsNullOrEmpty(employee.FirstName)) exist.FirstName = employee.FirstName;
            if (!string.IsNullOrEmpty(employee.LastName)) exist.LastName = employee.LastName;

            // Cập nhật các trường khác nếu khác null và không rỗng
            if (!string.IsNullOrEmpty(employee.Nationality)) exist.Nationality = employee.Nationality;
            if (!string.IsNullOrEmpty(employee.Ethnic)) exist.Ethnic = employee.Ethnic;
            if (!string.IsNullOrEmpty(employee.Religion)) exist.Religion = employee.Religion;
            if (!string.IsNullOrEmpty(employee.PlaceOfBirth)) exist.PlaceOfBirth = employee.PlaceOfBirth;
            if (employee.DateOfBirth.HasValue) exist.DateOfBirth = employee.DateOfBirth.Value;
            if (!string.IsNullOrEmpty(employee.IndentityCard)) exist.IndentityCard = employee.IndentityCard;
            if (!string.IsNullOrEmpty(employee.PlaceIssued)) exist.PlaceIssued = employee.PlaceIssued;
            if (!string.IsNullOrEmpty(employee.Country))
            {
                exist.Country = employee.Country;
            }

            if (!string.IsNullOrEmpty(employee.Province)) exist.Province = employee.Province;
            if (!string.IsNullOrEmpty(employee.District)) exist.District = employee.District;
            if (!string.IsNullOrEmpty(employee.Commune)) exist.Commune = employee.Commune;
            if (!string.IsNullOrEmpty(employee.Address)) exist.Address = employee.Address;

          
            if (!string.IsNullOrEmpty(employee.InsuranceNumber)) exist.InsuranceNumber = employee.InsuranceNumber;

           
            

            // Cập nhật vào employee database
            Guid result = await _employeesRepository.UpdateEmployee(exist);
            if(result != EmployeeId)
            {

               throw new Exception("Update employee failed.");
            }

            if(!string.IsNullOrEmpty(employee.Phone) || !string.IsNullOrEmpty(employee.Email))
            {
                // Cập nhật vào user database
                await _kafkaProducer.SendMessageAsync(new
                {
                    UserId = exist.AccountID,
                    Phone = employee.Phone,
                    Email = employee.Email,
                    UpdatedAt = DateTime.UtcNow
                });

            }
            

            return new EmployeeUpdateResponse { Result="Updated Employee"};
        }
    }
}
