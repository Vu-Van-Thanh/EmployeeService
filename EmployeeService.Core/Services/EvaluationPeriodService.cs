using EmployeeService.Core.Domain.Entities;
using EmployeeService.Core.DTO;
using EmployeeService.Core.Extension;
using EmployeeService.Core.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Services
{
    public interface IEvaluationPeriodService
    {
        Task<List<EvaluationPeriodDTO>> GetAllPeriods();
        Task<EvaluationPeriodDTO?> GetPeriodById(Guid id);
        Task<List<EvaluationPeriodDTO>> GetActivePeriods();
        Task<List<EvaluationPeriodDTO>> GetPeriodsByFilter(EvaluationPeriodFilterDTO filter);
        Task<Guid> AddPeriod(EvaluationPeriodAddDTO period);
        Task<Guid> UpdatePeriod(EvaluationPeriodDTO period);
        Task<bool> DeletePeriod(Guid id);
    }

    public class EvaluationPeriodService : IEvaluationPeriodService
    {
        private readonly IEvaluationPeriodRepository _periodRepository;

        public EvaluationPeriodService(IEvaluationPeriodRepository periodRepository)
        {
            _periodRepository = periodRepository;
        }

        public async Task<Guid> AddPeriod(EvaluationPeriodAddDTO period)
        {
            EvaluationPeriod result = new EvaluationPeriod
            {
                Name = period.Name,
                StartDate = period.StartDate,
                EndDate = period.EndDate
            };

            return await _periodRepository.AddPeriod(result);
        }

        public async Task<bool> DeletePeriod(Guid id)
        {
            return await _periodRepository.DeletePeriod(id);
        }

        public async Task<List<EvaluationPeriodDTO>> GetActivePeriods()
        {
            List<EvaluationPeriod> periods = await _periodRepository.GetActivePeriods();
            return periods.Select(p => p.ToDTO()).ToList();
        }

        public async Task<List<EvaluationPeriodDTO>> GetAllPeriods()
        {
            List<EvaluationPeriod> periods = await _periodRepository.GetAll();
            return periods.Select(p => p.ToDTO()).ToList();
        }

        public async Task<EvaluationPeriodDTO?> GetPeriodById(Guid id)
        {
            EvaluationPeriod? period = await _periodRepository.GetPeriodById(id);
            if (period == null)
                return null;
            
            return period.ToDTO();
        }

        public async Task<List<EvaluationPeriodDTO>> GetPeriodsByFilter(EvaluationPeriodFilterDTO filter)
        {
            List<EvaluationPeriod> periods = await _periodRepository.GetPeriodsByFilter(filter.ToExpression());
            return periods.Select(p => p.ToDTO()).ToList();
        }

        public async Task<Guid> UpdatePeriod(EvaluationPeriodDTO period)
        {
            try
            {
                await _periodRepository.UpdatePeriod(new EvaluationPeriod
                {
                    PeriodID = period.PeriodID,
                    Name = period.Name,
                    StartDate = period.StartDate,
                    EndDate = period.EndDate
                });
                
                return period.PeriodID;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error updating period: {ex.Message}");
                return Guid.Empty;
            }
        }
    }
} 