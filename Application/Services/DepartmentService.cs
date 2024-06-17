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

    public async Task<DepartmentDefaultResponse> CreateAsync(DepartmentDefaultRequest department)
    {
        var dbCompany = await _companyRepository.GetByIdAsync(department.CompanyId);

        if (dbCompany is null)
            throw new CompanyNotFound($"Company with id {department.CompanyId} does not exist");
        
        var existingDepartment = await _departmentRepository.GetByNameAsync(StringCleaner.CleanInput(department.Name), department.CompanyId);
        
        if (existingDepartment is not null) throw new DepartmentBadRequest($"Department with name '{StringCleaner.CleanInput(department.Name)}'" +
                                                                           $"for company with id {department.CompanyId} already exists");
        var dbDepartment = await _departmentRepository.CreateAsync(new Department()
        {
            Name = StringCleaner.CleanInput(department.Name),
            Phone = StringCleaner.CleanInput(department.Phone),
            CompanyId = department.CompanyId
        });
        if (dbDepartment is null) throw new DepartmentBadRequest("Error while saving the department");
        return DepartmentMapper.MapToDefaultResponse(dbDepartment);
    }

    public async Task<DepartmentDefaultResponse> UpdateAsync(DepartmentDefaultRequest department, int id)
    {
        var dbCompany = await _companyRepository.GetByIdAsync(department.CompanyId);

        if (dbCompany is null)
            throw new CompanyNotFound($"Company with id {department.CompanyId} does not exist");
        
        var dbDepartment =
            await this._departmentRepository.GetByNameAsync(department.Name, department.CompanyId);
        if (dbDepartment is not null)
            throw new DepartmentBadRequest(
                $"Department '{department.Name}' already exists in company with id {department.CompanyId}");

        dbDepartment = await _departmentRepository.GetByIdAsync(id);
        if (dbDepartment is null)
            throw new DepartmentBadRequest(
                $"Department with id {id} does not exist");

        var parameters = new Department()
        {
            Name = StringCleaner.CleanInput(department.Name),
            Phone = StringCleaner.CleanInput(department.Phone),
            CompanyId = department.CompanyId
        };
        
        var updatedDepartment = await _departmentRepository.UpdateAsync(parameters, id);
        
        if (updatedDepartment is null) throw new DepartmentBadRequest($"Error while updating the department with id {id}");
        
        return DepartmentMapper.MapToDefaultResponse(updatedDepartment);
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