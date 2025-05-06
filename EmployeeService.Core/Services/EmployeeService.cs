using AutoMapper;
using EmployeeService.Core.Domain.Entities;
using EmployeeService.Core.DTO;
using EmployeeService.Core.Enums;
using EmployeeService.Core.RepositoryContracts;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;

namespace EmployeeService.Core.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeInfo> GetEmployeeById(Guid Id);
        Task<EmployeeUpdateResponse> UpdateEmployee(EmployeeUpdateRequest employee, Guid EmployeeId);
        Task<List<EmployeeInfo>> GetAllEmployees();
        Task<List<EmployeeInfo>> GetEmployeesByFeature(string feature, string value = "All");
        Task<Guid> AddEmployee(EmployeeAddRequest employee);
        Task<EmployeeInfo?> GetEmployeeIdByUserId(Guid Id);
        Task<List<EmployeeInfo>> GetEmployeeByFilter(EmployeeFilterDTO filter);
        Task<bool> DeleteEmployee(Guid employeeId);
        Task<EmployeeImportDTO> ImportProfileFromExcelAsync(byte[] filebytes, string filename);

    }
    public class EmployeeServices : IEmployeeService
    {
        private readonly IEmployeeRepository _employeesRepository;
        private readonly IEmployeeMediaRepository _employeeMediaRepository;
        private readonly IMapper _mapper;


        public EmployeeServices(IEmployeeRepository employeesRepository, IMapper mapper, IEmployeeMediaRepository employeeMediaRepository)
        {
            _employeesRepository = employeesRepository;
            _mapper = mapper;
            _employeeMediaRepository = employeeMediaRepository;
        }

        public async Task<Guid> AddEmployee(EmployeeAddRequest employee)
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
                InsuranceNumber = employee.InsuranceNumber,
                ManagerID = employee.ManagerId

            };
            await _employeesRepository.AddEmployee(newEmployee);
            return newEmployee.EmployeeID;
        }

        public async Task<bool> DeleteEmployee(Guid employeeId)
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
            foreach(var item in employeeInfo)
            {
                EmployeeMedia? avartar = await _employeeMediaRepository.GetEmployeeMediaIdByType(Guid.Parse(item.EmployeeID), "Avatar");
                EmployeeMedia? FIndentity = await _employeeMediaRepository.GetEmployeeMediaIdByType(Guid.Parse(item.EmployeeID), "FrontIdentityCard");
                EmployeeMedia? BIndentity = await _employeeMediaRepository.GetEmployeeMediaIdByType(Guid.Parse(item.EmployeeID), "BackIdentityCard");
                EmployeeMedia? FInsurance = await _employeeMediaRepository.GetEmployeeMediaIdByType(Guid.Parse(item.EmployeeID), "FrontInsuranceCard");
                EmployeeMedia? BInsurance = await _employeeMediaRepository.GetEmployeeMediaIdByType(Guid.Parse(item.EmployeeID), "BackInsuranceCard");
                item.avartar = avartar != null ? avartar.MediaUrl : null;
                item.identity.Add(FIndentity != null ? FIndentity.MediaUrl : null);
                item.identity.Add(BIndentity != null ? BIndentity.MediaUrl : null);
                item.insurance.Add(FInsurance != null ? FInsurance.MediaUrl : null);
                item.insurance.Add(BInsurance != null ? BInsurance.MediaUrl : null);
            }
            return employeeInfo;
        }

        public async Task<List<EmployeeInfo>> GetEmployeeByFilter(EmployeeFilterDTO filter)
        {
            List<Employee> employee = await  _employeesRepository.GetEmployeesByFilter(filter.ToExpression());
            return employee.Select(em => em.ToEmployeeInfo()).ToList();
        }

        public async Task<EmployeeInfo> GetEmployeeById(Guid Id)
        {
            Employee employee = await _employeesRepository.GetEmployeeById(Id);
            EmployeeMedia? empployeeMedia = await _employeeMediaRepository.GetEmployeeMediaIdByType(Id, "Avatar");
            if (employee == null) return null;
            return employee.ToEmployeeInfo();
        }

        public async Task<EmployeeInfo?> GetEmployeeIdByUserId(Guid Id)
        {
            Employee? employee = await _employeesRepository.GetEmployeeIdByUserId(Id);
            EmployeeInfo employeeInfo = new EmployeeInfo();
            employeeInfo = employee.ToEmployeeInfo();
            EmployeeMedia? avartar = await _employeeMediaRepository.GetEmployeeMediaIdByType(Id, "Avatar");
            EmployeeMedia? FIndentity = await _employeeMediaRepository.GetEmployeeMediaIdByType(Id, "FrontIdentityCard");
            EmployeeMedia? BIndentity = await _employeeMediaRepository.GetEmployeeMediaIdByType(Id, "BackIdentityCard");
            EmployeeMedia? FInsurance = await _employeeMediaRepository.GetEmployeeMediaIdByType(Id, "FrontInsuranceCard");
            EmployeeMedia? BInsurance = await _employeeMediaRepository.GetEmployeeMediaIdByType(Id, "BackInsuranceCard");
            employeeInfo.avartar = avartar != null ? avartar.MediaUrl : null;
            employeeInfo.identity.Add(FIndentity != null ? FIndentity.MediaUrl : null);
            employeeInfo.identity.Add(BIndentity != null ? BIndentity.MediaUrl : null);
            employeeInfo.insurance.Add(FInsurance != null ? FInsurance.MediaUrl : null);
            employeeInfo.insurance.Add(BInsurance != null ? BInsurance.MediaUrl : null);

            if (employee != null )
            {
                return employeeInfo;

            }
            return null;

        }

        public async Task<List<EmployeeInfo>> GetEmployeesByFeature(string feature, string value = "All")
        {
            List<Employee> em = await _employeesRepository.GetAllEmployeesByFeature(feature, value);
            return em.Select(em => em.ToEmployeeInfo()).ToList();
        }

        public async Task<EmployeeImportDTO> ImportProfileFromExcelAsync(byte[] fileBytes, string fileName)
        {

            if (fileBytes == null || fileBytes.Length == 0)
                throw new Exception("File Excel không hợp lệ.");

            // lưu file
            var tempFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "EmployeeProfile");
            if (!Directory.Exists(tempFolder))
                Directory.CreateDirectory(tempFolder);

            var tempFilePath = Path.Combine(tempFolder, $"{fileName}.xlsx");
            await File.WriteAllBytesAsync(tempFilePath, fileBytes);


            using var stream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            // ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage(stream);
            var sheet = package.Workbook.Worksheets[0];


            // Mapping dữ liệu từ từng ô
            var fullName = sheet.Cells["K3"].Text.Trim();
            var firstSpaceIndex = fullName.IndexOf(" ");
            string firstName = "";
            string lastName = "";

            if (firstSpaceIndex > 0)
            {
                firstName = fullName.Substring(0, firstSpaceIndex).Trim();
                lastName = fullName.Substring(firstSpaceIndex + 1).Trim();
            }
            else
            {
                firstName = fullName;
                lastName = "";
            }

            var email = sheet.Cells["AA8"].Text.Trim();
            var dayOfBirth = sheet.Cells["K4"].Text.Trim();
            var monthOfBirth = sheet.Cells["L4"].Text.Trim();
            var ethnic = sheet.Cells["T3"].Text.Trim();
            var yearOfBirth = sheet.Cells["M4"].Text.Trim();
            var identitycard = sheet.Cells["K6"].Value.ToString();
            var insuranceNumber = sheet.Cells["AB5"].Text.Trim();
            var phone = sheet.Cells["AB7"].Text.Trim();
            var address = sheet.Cells["K8"].Text.Trim();
            var placeIssued = sheet.Cells["V5"].Text.Trim();
            var placeOfBirth = sheet.Cells["P4"].Text.Trim();
            string idString = sheet.Cells["K5"].Text.Trim();
            Guid id = Guid.Empty;
            if (Guid.TryParse(idString, out var parsedId))
            {
                id = parsedId;
            }
           
            Guid newId = Guid.Empty;
            // xử lý quê quán
            string[] nativeLand = sheet.Cells["K7"].Text.Trim().Split("-");
            var commune = nativeLand[1].Trim();
            var district = nativeLand[2].Trim();
            var province = nativeLand[3].Trim();
            var country = nativeLand[4].Trim();
            var department = sheet.Cells["Q9"].Text.Trim();
            var CBQL = sheet.Cells["AA9"].Text.Trim();
            Guid CBQLID = Guid.Empty;
            if(Guid.TryParse(CBQL, out var parsedId1))
            {
                CBQLID = parsedId1;
            }
            var position = sheet.Cells["V5"].Text.Trim();
            var tax = sheet.Cells["H11"].Text.Trim();
            var bankAccountOwner = sheet.Cells["P12"].Text.Trim();
            var bankAccountName = sheet.Cells["Y12"].Text.Trim();
            var vehical = sheet.Cells["H13"].Text.Trim() + " " + sheet.Cells["U13"].Text.Trim();
            if (id == Guid.Empty) 
            {
                Console.WriteLine("Set id nhan vien moi  = {0} ", fileName);
                newId = Guid.Parse(fileName);
                id = newId;
            }
            // dữ liệu lương nhân viên
            SalaryDTO salary = new SalaryDTO();
            List<BonusDTO> bonus = new List<BonusDTO>();
            List<DeductionDTO> deduction = new List<DeductionDTO>();
            List<AdjustmentDTO> adjustment = new List<AdjustmentDTO>();
            var sheetSalary = package.Workbook.Worksheets[2];
            var baseSalary = sheetSalary.Cells["C3"].Text.Trim();
            var baseIndex = sheetSalary.Cells["C4"].Text.Trim();
            int bonusRow = 4;
            while (!string.IsNullOrEmpty(sheetSalary.Cells[$"E{bonusRow}"].Text))
            {
                var bonusItem = new BonusDTO
                {
                    BonusName = sheetSalary.Cells[$"E{bonusRow}"].Text.Trim(),
                    BonusPercentage = decimal.TryParse(sheetSalary.Cells[$"G{bonusRow}"].Text.Trim(), out var coef) ? coef : 0,
                    BonusAmount = decimal.TryParse(sheetSalary.Cells[$"I{bonusRow}"].Text.Trim(), out var qty) ? qty : 0
                };
                bonus.Add(bonusItem);
                bonusRow++;
            }
            int deductionRow = 4;
            while (!string.IsNullOrEmpty(sheetSalary.Cells[$"K{deductionRow}"].Text))
            {
                var deductionItem = new DeductionDTO
                {
                    DeductionName = sheetSalary.Cells[$"K{deductionRow}"].Text.Trim(),
                    DeductionPercentage = decimal.TryParse(sheetSalary.Cells[$"M{deductionRow}"].Text.Trim(), out var coef) ? coef : 0,
                    DeductionAmount = decimal.TryParse(sheetSalary.Cells[$"O{deductionRow}"].Text.Trim(), out var qty) ? qty : 0
                };
                deduction.Add(deductionItem);
                deductionRow++;
            }
            int adjustmentRow = 4;
            while (!string.IsNullOrEmpty(sheetSalary.Cells[$"Q{adjustmentRow}"].Text))
            {
                var adjustmentItem = new AdjustmentDTO
                {
                    AdjustmentName = sheetSalary.Cells[$"Q{adjustmentRow}"].Text.Trim(),
                    AdjustmentPercentage = decimal.TryParse(sheetSalary.Cells[$"S{adjustmentRow}"].Text.Trim(), out var coef) ? coef : 0,
                    AdjustmentAmount = decimal.TryParse(sheetSalary.Cells[$"U{adjustmentRow}"].Text.Trim(), out var qty) ? qty : 0
                };
                adjustment.Add(adjustmentItem);
                adjustmentRow++;
            }
            
            salary.BaseSalary = decimal.Parse(baseSalary);
            salary.BaseIndex = baseIndex;
            salary.Bonus = bonus;
            salary.Deduction = deduction;
            salary.Adjustment = adjustment;


            // ảnh hồ sơ
            var pictures = sheet.Drawings.OfType<ExcelPicture>().ToList();
            var path = string.Empty;
            string realPath = string.Empty;
            if (pictures.Any())
            {
                var picture = pictures[0];
                byte[] imageBytes = picture.Image.ImageBytes;

                // Lấy extension từ ảnh (nếu không có, mặc định là .png)
                string extension = Path.GetExtension(picture.Name);
                if (string.IsNullOrEmpty(extension))
                {
                    extension = ".png";
                }

                string imageName = $"{firstName}_{lastName}_{(id == null ? newId.ToString() : id.ToString())}{extension}";

                // Đường dẫn tuyệt đối và tương đối
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "EmployeeAvatarProfile", imageName);
                realPath = Path.Combine("Uploads", "EmployeeAvatarProfile", imageName);

                // Tạo thư mục nếu chưa có
                var folder = Path.GetDirectoryName(savePath);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                // Ghi file
                await File.WriteAllBytesAsync(savePath, imageBytes);

                path = savePath;
            }
            else
            {
                Console.WriteLine("❌ Không tìm thấy ảnh nào trong sheet.");
            }

            DateTime? birthDate = null;
            if (int.TryParse(dayOfBirth, out var day) &&
                int.TryParse(monthOfBirth, out var month) &&
                int.TryParse(yearOfBirth, out var year))
            {
                try
                {
                    birthDate = new DateTime(year, month, day);
                }
                catch
                {
                    birthDate = null;
                }
            }
            EmployeeImportDTO result = new EmployeeImportDTO
            {
                EmployeeId = newId != Guid.Empty ? newId : id,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                DateOfBirth = birthDate,
                Ethnic = ethnic,
                IndentityCard = identitycard,
                Phone = phone,
                Address = address,
                InsuranceNumber = insuranceNumber,
                Tax = tax,
                BankAccountOwner = bankAccountOwner,
                BankAccountName = bankAccountName,
                Vehicle = vehical,
                path = path,
                Commune = commune,
                District = district,
                Province = province,
                Country = country,
                PlaceIssued = placeIssued,
                PlaceOfBirth = placeOfBirth,
                Salaries = salary

            };
            Employee employee = new Employee
            {
                EmployeeID = newId != Guid.Empty ? newId : id,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = birthDate,
                AccountID = Guid.Empty,
                ManagerID = CBQLID != Guid.Empty ? CBQLID : Guid.Empty,
                DepartmentID = department,
                Position = position,
                Gender = GenderOptions.Male,
                Tax = tax,
                Address = address,
                Nationality = country,
                Ethnic = ethnic,
                Religion = "",
                PlaceOfBirth = placeOfBirth,
                IndentityCard = identitycard,
                PlaceIssued = placeIssued,
                Country = country,
                Province = province,
                District = district,
                Commune = commune,
                InsuranceNumber = insuranceNumber
            };
            EmployeeMedia media;
            
            if(newId != Guid.Empty)
            {
                await  _employeesRepository.AddEmployee(employee);
            }
            else
            {
               await  _employeesRepository.UpdateEmployee(employee);
            }
            if(!string.IsNullOrEmpty(path))
            {
                Guid existMedia = Guid.Empty;
                List<EmployeeMedia>? employeeMedia;
                try
                {

                    var employeeMediaResult = await _employeeMediaRepository.GetEmployeeMediaIdByType(employee.EmployeeID, "Avatar");

                    
                    if (employeeMediaResult != null && employeeMediaResult.EmployeeMediaID != Guid.Empty)
                    {
                        existMedia = employeeMediaResult.EmployeeMediaID; 
                    }
                    else
                    {
                        existMedia = Guid.Empty; 
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error: {ex.Message}");
                }
               
                if(existMedia == Guid.Empty)
                {
                    media = new EmployeeMedia
                    {
                        EmployeeMediaID = Guid.NewGuid(),
                        EmployeeID = employee.EmployeeID,
                        MediaUrl = realPath,
                        MediaType = "Avatar"
                    };
                    await _employeeMediaRepository.AddAsync(media);
                }
                else
                {
                    media = new EmployeeMedia
                    {
                        EmployeeMediaID = existMedia,
                        EmployeeID = employee.EmployeeID,
                        MediaUrl = realPath,
                        MediaType = "Avatar"
                    };
                    await _employeeMediaRepository.UpdateAsync(media);
                }
               
            }
            
            return result;

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
            if (!string.IsNullOrEmpty(employee.Position)) exist.Position = employee.Position;
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
            if(!string.IsNullOrEmpty(employee.Tax)) exist.Tax = employee.Tax;
            if(!string.IsNullOrEmpty(employee.ManagerID.ToString())) exist.ManagerID = employee.ManagerID;
            if (!string.IsNullOrEmpty(employee.Province)) exist.Province = employee.Province;
            if (!string.IsNullOrEmpty(employee.District)) exist.District = employee.District;
            if (!string.IsNullOrEmpty(employee.Commune)) exist.Commune = employee.Commune;
            if (!string.IsNullOrEmpty(employee.Address)) exist.Address = employee.Address;


            if (!string.IsNullOrEmpty(employee.InsuranceNumber)) exist.InsuranceNumber = employee.InsuranceNumber;




            // Cập nhật vào employee database
            Guid result = await _employeesRepository.UpdateEmployee(exist);
            if (result != EmployeeId)
            {

                throw new Exception("Update employee failed.");
            }

            // cập nhật thông tin các file ảnh
            if (employee.identityCardImage != null)
            {
                foreach(IdentityCard item in employee.identityCardImage)
                {
                    Guid existMedia = (await _employeeMediaRepository.GetEmployeeMediaIdByType(EmployeeId, "Avatar")).EmployeeMediaID;
                    if (existMedia != Guid.Empty)
                    {
                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "IdentityCard");
                        string extension = Path.GetExtension(item.identityImage.FileName);
                        string fileName = $"{EmployeeId}_{item.type}{extension}";
                        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "IdentityCard");
                        string fullPath = Path.Combine(folderPath, fileName);
                        string relativePath = Path.Combine("Uploads", "IdentityCard", fileName);
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await item.identityImage.CopyToAsync(stream);
                        }
                        EmployeeMedia media = new EmployeeMedia
                        {
                            EmployeeMediaID = existMedia,
                            EmployeeID = exist.EmployeeID,
                            MediaUrl = relativePath,
                            MediaType = item.type
                        };
                        await _employeeMediaRepository.UpdateAsync(media);
                    }
                    else
                    {
                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "IdentityCard");
                        string extension = Path.GetExtension(item.identityImage.FileName);
                        string fileName = $"{EmployeeId}_{item.type}{extension}";
                        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "IdentityCard");
                        string fullPath = Path.Combine(folderPath, fileName);
                        string relativePath = Path.Combine("Uploads", "IdentityCard", fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await item.identityImage.CopyToAsync(stream);
                        }
                        EmployeeMedia media = new EmployeeMedia
                        {
                            EmployeeMediaID = Guid.NewGuid(),
                            EmployeeID = exist.EmployeeID,
                            MediaUrl = relativePath,
                            MediaType = item.type
                        };
                        await _employeeMediaRepository.AddAsync(media);
                    }

                }
            }

            // ảnh căn cước công dân
            if (employee.insuranceCardImage != null)
            {
                foreach (InsuranceCard item in employee.insuranceCardImage)
                {

                    Guid existMedia = (await _employeeMediaRepository.GetEmployeeMediaIdByType(EmployeeId, "Avatar")).EmployeeMediaID;
                    if (existMedia != Guid.Empty)
                    {
                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "InsuranceCard");
                        string extension = Path.GetExtension(item.insuranceImage.FileName);
                        string fileName = $"{EmployeeId}_{item.type}{extension}";
                        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "InsuranceCard");
                        string fullPath = Path.Combine(folderPath, fileName);
                        string relativePath = Path.Combine("Uploads", "InsuranceCard", fileName);
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await item.insuranceImage.CopyToAsync(stream);
                        }
                        EmployeeMedia media = new EmployeeMedia
                        {
                            EmployeeMediaID = existMedia,
                            EmployeeID = exist.EmployeeID,
                            MediaUrl = relativePath,
                            MediaType = item.type
                        };
                        await _employeeMediaRepository.UpdateAsync(media);
                    }
                    else
                    {
                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "InsuranceCard");
                        string extension = Path.GetExtension(item.insuranceImage.FileName);
                        string fileName = $"{EmployeeId}_{item.type}{extension}";
                        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "InsuranceCard");
                        string fullPath = Path.Combine(folderPath, fileName);
                        string relativePath = Path.Combine("Uploads", "InsuranceCard", fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await item.insuranceImage.CopyToAsync(stream);
                        }
                        EmployeeMedia media = new EmployeeMedia
                        {
                            EmployeeMediaID = Guid.NewGuid(),
                            EmployeeID = exist.EmployeeID,
                            MediaUrl = relativePath,
                            MediaType = item.type
                        };
                        await _employeeMediaRepository.AddAsync(media);
                    }

                }
            }
            return new EmployeeUpdateResponse { Result = "Updated Employee" };
        }
    }
}
