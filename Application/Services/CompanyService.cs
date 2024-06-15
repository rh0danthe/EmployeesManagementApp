using System.Threading.Tasks;
using Application.Abstractions.RepositoryInterfaces;
using Application.Dto.CompanyDto;
using Application.Services.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _repository;

    public CompanyService(ICompanyRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<CompanyResponse> CreateAsync(CompanyRequest company)
    {
        var dbCompany = new Company() { Name = company.Name };
        return MapToResponse(await _repository.CreateAsync(dbCompany));
    }
    
    private CompanyResponse MapToResponse(Company company)
    {
        return new CompanyResponse()
        {
            Id = company.Id,
            Name = company.Name
        };
    }
}