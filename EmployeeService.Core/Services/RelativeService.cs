using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EmployeeService.Core.Domain.Entities;
using EmployeeService.Core.DTO;
using EmployeeService.Core.RepositoryContracts;

namespace EmployeeService.Core.Services
{
    public interface IRelativeService
    {
        Task<RelativeUpsertResponse> relativeUpsertResponse(RelativeUpsertRequest relativeUpsertRequest);
        Task<List<RelativeInfoResponse>> GetAllRelative();
        Task<List<RelativeInfoResponse>> GetRelativeByEmployee(Guid employeeId);
        Task<Guid> DeleteRelative(string CCCD);
    }
    public class RelativeService : IRelativeService
    {

        private readonly IRelativeRepository _relative;

        public RelativeService(IRelativeRepository relative)
        {
            _relative = relative;
        }

        public async Task<List<RelativeInfoResponse>> GetAllRelative()
        {
            List<Relative> reList = await _relative.GetAllRelative();
            return reList.Select(x => x.ToRelativeInfoResponse()).ToList();
        }

        public async Task<List<RelativeInfoResponse>> GetRelativeByEmployee(Guid employeeId)
        {
            List<Relative>? list = await _relative.GetRelativeById(employeeId);
            return list?.Select(x => x.ToRelativeInfoResponse()).ToList();
        }

        public async Task<Guid> DeleteRelative(string CCCD)
        {
            Relative? relative = (await _relative.GetRelativeByFilter(r => r.IndentityCard == CCCD)).FirstOrDefault();
            if (relative == null)
            {
                return Guid.Empty;
                
                
            }
            return await _relative.DeleteRelative(relative);
        }
        public async Task<RelativeUpsertResponse> relativeUpsertResponse(RelativeUpsertRequest relativeUpsertRequest)
        {
            List<Relative>? listRelative;
            if(!String.IsNullOrEmpty(relativeUpsertRequest.OldID))
            {
                listRelative = await _relative.GetRelativeByFilter(r => r.IndentityCard == relativeUpsertRequest.OldID);
            }
            else
            {
                listRelative = await _relative.GetRelativeByFilter(r => r.IndentityCard.ToString() == relativeUpsertRequest.IndentityCard);
            }    
            
            Guid existId = Guid.Empty;
            if (listRelative.Count > 0)
            {
                existId = listRelative[0].RelativeID;    
            }    
            
            Relative relative = await _relative.UpsertRelative(new Relative
            {
                RelativeID = existId != Guid.Empty ? existId : Guid.NewGuid(),
                EmployeeID = relativeUpsertRequest.EmployeeID,
                FirstName = relativeUpsertRequest.FirstName,
                RelativeType = relativeUpsertRequest.RelativeType,
                LastName = relativeUpsertRequest.LastName,
                DateOfBirth = relativeUpsertRequest.DateOfBirth,
                Address = relativeUpsertRequest.Address,
                Nationality = relativeUpsertRequest.Nationality,
                Ethnic = relativeUpsertRequest.Ethnic,
                Religion = relativeUpsertRequest.Religion,
                PlaceOfBirth = relativeUpsertRequest.PlaceOfBirth,
                IndentityCard = relativeUpsertRequest.IndentityCard,
                Country = relativeUpsertRequest.Country,
                Province = relativeUpsertRequest.Province,
                District = relativeUpsertRequest.District,
                Commune = relativeUpsertRequest.Commune,
                PhoneNumber = relativeUpsertRequest.PhoneNumber
            });
            await _relative.UpsertRelative(relative);
            return relative.ToRelativeUpsertResponse();
        }
    }
}
