using Application.Dto.CompanyDto;

namespace Application.Services.Interfaces;

public interface ICompanyService
{
    public Task<CompanyResponse> CreateAsync(CompanyRequest company);
}