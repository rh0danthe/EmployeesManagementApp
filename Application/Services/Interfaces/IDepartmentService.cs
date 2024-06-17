using Application.Dto.Department;

namespace Application.Services.Interfaces;

public interface IDepartmentService
{
    Task<DepartmentDefaultResponse> CreateAsync(DepartmentDefaultRequest departmentDefault);
    Task<DepartmentDefaultResponse> UpdateAsync(DepartmentDefaultRequest departmentDefault, int id);
    Task<ICollection<DepartmentDefaultResponse>> GetAllByCompanyAsync(int companyId);
    Task<ICollection<DepartmentDefaultResponse>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
}