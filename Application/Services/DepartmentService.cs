using Application.Abstractions.Repository;
using Application.Dto.Department;
using Application.Services.Interfaces;
using Application.Utils;
using Domain.Entities;
using Domain.Exceptions.Company;
using Domain.Exceptions.Department;

namespace Application.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly ICompanyRepository _companyRepository;

    public DepartmentService(IDepartmentRepository departmentRepository, ICompanyRepository companyRepository)
    {
        _departmentRepository = departmentRepository;
        _companyRepository = companyRepository;
    }

    public async Task<DepartmentDefaultResponse> CreateAsync(DepartmentDefaultRequest departmentDefault)
    {
        var dbCompany = await _companyRepository.GetByIdAsync(departmentDefault.CompanyId);

        if (dbCompany is null)
            throw new CompanyNotFound($"Company with id {departmentDefault.CompanyId} does not exist");
        
        var existingDepartment = await _departmentRepository.GetByNameAsync(StringCleaner.CleanInput(departmentDefault.Name), departmentDefault.CompanyId);
        
        if (existingDepartment is not null) throw new DepartmentBadRequest($"Department with name '{StringCleaner.CleanInput(departmentDefault.Name)}'" +
                                                                           $"for company with id {departmentDefault.CompanyId} already exists");
        var dbDepartment = await _departmentRepository.CreateAsync(new Department()
        {
            Name = StringCleaner.CleanInput(departmentDefault.Name),
            Phone = StringCleaner.CleanInput(departmentDefault.Phone),
            CompanyId = departmentDefault.CompanyId
        });
        if (dbDepartment is null) throw new DepartmentBadRequest("Error while saving the departmentDefault");
        return DepartmentMapper.MapToDefaultResponse(dbDepartment);
    }

    public async Task<DepartmentDefaultResponse> UpdateAsync(DepartmentDefaultRequest departmentDefault, int id)
    {
        var dbCompany = await _companyRepository.GetByIdAsync(departmentDefault.CompanyId);

        if (dbCompany is null)
            throw new CompanyNotFound($"Company with id {departmentDefault.CompanyId} does not exist");
        
        var dbDepartment = await _departmentRepository.UpdateAsync(new Department()
        {
            Name = StringCleaner.CleanInput(departmentDefault.Name),
            Phone = StringCleaner.CleanInput(departmentDefault.Phone),
            CompanyId = departmentDefault.CompanyId
        }, id);
        
        if (dbDepartment is null) throw new DepartmentBadRequest($"Error while updating the departmentDefault with id {id}");
        
        return DepartmentMapper.MapToDefaultResponse(dbDepartment);
    }

    public async Task<ICollection<DepartmentDefaultResponse>> GetAllByCompanyAsync(int companyId)
    {
        var dbCompany = await _companyRepository.GetByIdAsync(companyId);

        if (dbCompany is null)
            throw new CompanyNotFound($"Company with id {companyId} does not exist");
        
        var res = await _departmentRepository.GetAllByCompanyAsync(companyId);
        
        return res.Select(DepartmentMapper.MapToDefaultResponse).ToList();
    }

    public async Task<ICollection<DepartmentDefaultResponse>> GetAllAsync()
    {
        var res = await _departmentRepository.GetAllAsync();
        
        return res.Select(DepartmentMapper.MapToDefaultResponse).ToList();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var dbDepartment = await _departmentRepository.GetByIdAsync(id);
        
        if (dbDepartment is null) throw new CompanyBadRequest($"Department with id {id} does not exist");

        return await _departmentRepository.DeleteAsync(id);
    }
}