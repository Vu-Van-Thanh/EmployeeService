using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EmployeeService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly IWebHostEnvironment _env;
        public DynamicController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _env = env;
        }

        // Endpoint để thực thi câu lệnh SQL động
        [HttpPost("execute-query")]
        public IActionResult ExecuteQuery([FromBody] string sqlQuery)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var result = connection.Query(sqlQuery); // Dùng Dapper để thực thi câu SQL
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Có lỗi xảy ra: {ex.Message}");
            }
        }

        [HttpGet("Statistic/{*subfolder}")]
        public IActionResult GetFiles(string subfolder = "")
        {
            var rootPath = Path.Combine(_env.WebRootPath, subfolder ?? "");

            if (!Directory.Exists(rootPath))
                return NotFound("Thư mục không tồn tại.");

            var files = Directory.GetFiles(rootPath, "*", SearchOption.AllDirectories)
                .Select(path => new
                {
                    name = Path.GetFileName(path),
                    fullPath = path.Replace(_env.WebRootPath, "").Replace("\\", "/"),
                    modified = System.IO.File.GetLastWriteTime(path),
                    size = new FileInfo(path).Length
                }).ToList();

            return Ok(files);
        }
    }
}
