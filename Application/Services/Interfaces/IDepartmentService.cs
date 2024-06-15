using Application.Dto.DepartmentDto;

namespace Application.Services.Interfaces;

public interface IDepartmentService
{
    public Task<DepartmentCreateResponse> CreateAsync(DepartmentCreateRequest department);
    public Task<DepartmentCreateResponse> UpdateAsync(DepartmentCreateRequest department, int id); //todo dto name change
}