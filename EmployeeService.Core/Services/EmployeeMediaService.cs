using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeService.Core.Domain.Entities;
using EmployeeService.Core.DTO;
using EmployeeService.Core.RepositoryContracts;

namespace EmployeeService.Core.Services
{
    public interface IEmployeeMediaService
    {
        Task<IEnumerable<EmployeeMediaInfo>> GetAllAsync();
        Task<EmployeeMediaInfo> GetByIdAsync(Guid id);
        Task<List<EmployeeMediaAddResponse>> AddAsync(EmployeeMediaAddRequest employeeMedia);
        Task UpdateAsync(EmployeeMediaUpdateRequest employeeMedia);
        Task DeleteAsync(Guid id);

    }
    public class EmployeeMediaService : IEmployeeMediaService
    {
        private readonly IEmployeeMediaRepository _repository;
        private readonly IFileService _fileService;

        public EmployeeMediaService(IEmployeeMediaRepository repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<IEnumerable<EmployeeMediaInfo>> GetAllAsync()
        {
            List<EmployeeMedia> medias =  await _repository.GetAllAsync();
            return medias.Select(e => e.ToEmployeeMediaInfo());

        }

        public async Task<EmployeeMediaInfo> GetByIdAsync(Guid id)
        {
           EmployeeMedia em = await _repository.GetByIdAsync(id);
            return em.ToEmployeeMediaInfo();
        }

        public async Task<List<EmployeeMediaAddResponse>> AddAsync(EmployeeMediaAddRequest employeeMedia)
        {
            List<string> mediaUrls = new List<string>();
            List<EmployeeMediaAddResponse> result =  new List<EmployeeMediaAddResponse>();

            // Lưu file vào server và lấy danh sách URL
            if (employeeMedia.Images != null && employeeMedia.Images.Length > 0)
            {
                mediaUrls = await _fileService.SaveMediaFilesAsync(employeeMedia.Images, "EmployeeMedia/" + employeeMedia.MediaType.ToString());
                
            }

            // Tạo đối tượng EmployeeMedia để lưu vào DB
            foreach(string url in mediaUrls)
            {
                EmployeeMedia media = new EmployeeMedia
                {
                    EmployeeMediaID = Guid.NewGuid(),
                    EmployeeID = employeeMedia.EmployeeId,
                    MediaUrl = url,
                    MediaType = employeeMedia.MediaType
                };
                result.Add(media.ToEmployeeMediaAddResponse());
                await _repository.AddAsync(media);
            }

            return result;
        }

        public async Task UpdateAsync(EmployeeMediaUpdateRequest employeeMedia)
        {
            if (employeeMedia.Images == null || employeeMedia.Images.Length == 0)
                return;

            // 1. Lưu file => chỉ lấy file đầu tiên (vì chỉ 1 ảnh mỗi loại)
            string mediaUrl = (await _fileService.SaveMediaFilesAsync(
                employeeMedia.Images,
                $"EmployeeMedia/{employeeMedia.MediaType}/{employeeMedia.EmployeeId}"
            )).FirstOrDefault();

            if (string.IsNullOrEmpty(mediaUrl))
                return;

            // 2. Kiểm tra media đã tồn tại chưa
            var existingMedia = await _repository.GetEmployeeMediaIdByType(
                employeeMedia.EmployeeId,
                employeeMedia.MediaType
            );

            if (existingMedia != null)
            {
                // Update
                existingMedia.MediaUrl = mediaUrl;
                await _repository.UpdateAsync(existingMedia);
            }
            else
            {
                // Add mới
                var newMedia = new EmployeeMedia
                {
                    EmployeeMediaID = Guid.NewGuid(),
                    EmployeeID = employeeMedia.EmployeeId,
                    MediaType = employeeMedia.MediaType,
                    MediaUrl = mediaUrl
                };
                await _repository.AddAsync(newMedia);
            }
        }


        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
