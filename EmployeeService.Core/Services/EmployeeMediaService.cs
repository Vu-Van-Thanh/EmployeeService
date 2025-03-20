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
                mediaUrls = await _fileService.SaveMediaFilesAsync(employeeMedia.Images, "EmployeeMedia");
                
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
            List<string> mediaUrls = new List<string>();
            List<Guid> mediaId = new List<Guid>();

            // Lưu file vào server và lấy danh sách URL
            if (employeeMedia.Images != null && employeeMedia.Images.Length > 0)
            {
                mediaUrls = await _fileService.SaveMediaFilesAsync(employeeMedia.Images, "EmployeeMedia");
                mediaId.Add(await _repository.GetEmployeeMediaIdByType(employeeMedia.EmployeeId, employeeMedia.MediaType));

            }

            // Tạo đối tượng EmployeeMedia để lưu vào DB
            int count = 0;
            foreach (string url in mediaUrls)
            {
                
                EmployeeMedia media = new EmployeeMedia
                {
                    EmployeeMediaID = mediaId[count],
                    EmployeeID = employeeMedia.EmployeeId,
                    MediaUrl = url,
                    MediaType = employeeMedia.MediaType
                };
                await _repository.UpdateAsync(media);
                count++;
            }
            
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
