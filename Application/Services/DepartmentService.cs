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

    public async Task<DepartmentCreateResponse> CreateAsync(DepartmentCreateRequest department)
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
        return DepartmentMapper.MapToCreateResponse(dbDepartment);
    }

    public async Task<DepartmentCreateResponse> UpdateAsync(DepartmentCreateRequest department, int id)
    {
        var dbCompany = await _companyRepository.GetByIdAsync(department.CompanyId);

        if (dbCompany is null)
            throw new CompanyNotFound($"Company with id {department.CompanyId} does not exist");
        
        var dbDepartment = await _departmentRepository.UpdateAsync(new Department()
        {
            Name = StringCleaner.CleanInput(department.Name),
            Phone = StringCleaner.CleanInput(department.Phone),
            CompanyId = department.CompanyId
        }, id);
        
        if (dbDepartment is null) throw new DepartmentBadRequest($"Error while updating the department with id {id}");
        
        return DepartmentMapper.MapToCreateResponse(dbDepartment);
    }
}