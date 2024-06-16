using Application.Abstractions.Repository;
using Application.Dto.Department;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Exceptions.Department;

namespace Application.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _repository;

    public DepartmentService(IDepartmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<DepartmentCreateResponse> CreateAsync(DepartmentCreateRequest department)
    {
        var dbDepartment = await _repository.CreateAsync(new Department()
        {
            Name = department.Name,
            Phone = department.Phone,
            CompanyId = department.CompanyId
        });
        if (dbDepartment is null) throw new DepartmentBadRequest("Error while saving the department");
        return DepartmentMapper.MapToCreateResponse(dbDepartment);
    }

    public async Task<DepartmentCreateResponse> UpdateAsync(DepartmentCreateRequest department, int id)
    {
        var dbDepartment = await _repository.UpdateAsync(new Department()
        {
            Name = department.Name,
            Phone = department.Phone,
            CompanyId = department.CompanyId
        }, id);
        if (dbDepartment is null) throw new DepartmentBadRequest($"Error while updating the department with id {id}");
        return DepartmentMapper.MapToCreateResponse(dbDepartment);
    }
}