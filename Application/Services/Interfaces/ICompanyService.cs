using System.Threading.Tasks;
using Application.Dto.Company;

namespace Application.Services.Interfaces;

public interface ICompanyService
{ 
    Task<CompanyResponse> CreateAsync(CompanyRequest company);
    Task<CompanyResponse> UpdateAsync(CompanyRequest company, int id);
    Task<ICollection<CompanyResponse>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
}