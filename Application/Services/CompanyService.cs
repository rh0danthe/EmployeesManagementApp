using System.Threading.Tasks;
using Application.Abstractions.Repository;
using Application.Dto.Company;
using Application.Services.Interfaces;
using Application.Utils;
using Domain.Entities;
using Domain.Exceptions.Company;

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
        var dbCompany = await _repository.CreateAsync(new Company() { Name = StringCleaner.CleanInput(company.Name )});
        if (dbCompany is null) throw new CompanyBadRequest("Error while creating the company");
        return CompanyMapper.MapToResponse(dbCompany);
    }
}