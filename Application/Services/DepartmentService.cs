using Application.Abstractions.RepositoryInterfaces;
using Application.Dto.DepartmentDto;
using Application.Services.Interfaces;
using Domain.Entities;

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
        var dbDepartment = new Department()
        {
            Name = department.Name,
            Phone = department.Phone,
            CompanyId = department.CompanyId
        };
        return MapToResponse(await _repository.CreateAsync(dbDepartment));
    }

    public async Task<DepartmentCreateResponse> UpdateAsync(DepartmentCreateRequest department, int id)
    {
        var dbDepartment = new Department()
        {
            Name = department.Name,
            Phone = department.Phone,
            CompanyId = department.CompanyId
        };
        return MapToResponse(await _repository.UpdateAsync(dbDepartment, id));
    }

    private DepartmentCreateResponse MapToResponse(Department department)
    {
        return new DepartmentCreateResponse()
        {
            Id = department.Id,
            Name = department.Name,
            Phone = department.Phone,
            CompanyId = department.CompanyId
        };
    }
}