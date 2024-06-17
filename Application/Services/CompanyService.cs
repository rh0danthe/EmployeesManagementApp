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

    public async Task<CompanyResponse> UpdateAsync(CompanyRequest company, int id)
    {
        var dbCompany = await _repository.GetByIdAsync(id);
        
        if (dbCompany is null) throw new CompanyNotFound($"Company with id {id} does not exist");

        var res = await _repository.UpdateAsync(new Company() {Name = company.Name}, id);
        
        if (res is null) throw new CompanyBadRequest("Error while updating the company");
        
        return CompanyMapper.MapToResponse(res);
    }

    public async Task<ICollection<CompanyResponse>> GetAllAsync()
    {
        var res = await _repository.GetAllAsync();
        
        return res.Select(CompanyMapper.MapToResponse).ToList();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var dbCompany = await _repository.GetByIdAsync(id);
        
        if (dbCompany is null) throw new CompanyBadRequest($"Company with id {id} does not exist");

        return await _repository.DeleteAsync(id);
    }
}