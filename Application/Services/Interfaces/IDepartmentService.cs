using System.Threading.Tasks;
using Application.Dto.DepartmentDto;

namespace Application.Services.Interfaces;

public interface IDepartmentService
{
    Task<DepartmentCreateResponse> CreateAsync(DepartmentCreateRequest department);
    Task<DepartmentCreateResponse> UpdateAsync(DepartmentCreateRequest department, int id); //todo dto name change
}