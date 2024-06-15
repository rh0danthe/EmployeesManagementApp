using System.Threading.Tasks;
using Application.Dto.CompanyDto;

namespace Application.Services.Interfaces;

public interface ICompanyService
{ 
    Task<CompanyResponse> CreateAsync(CompanyRequest company);
}