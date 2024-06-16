using System.Threading.Tasks;
using Application.Dto.Company;

namespace Application.Services.Interfaces;

public interface ICompanyService
{ 
    Task<CompanyResponse> CreateAsync(CompanyRequest company);
}