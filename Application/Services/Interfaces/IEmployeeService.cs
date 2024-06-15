using Application.Dto.EmployeeDto;

namespace Application.Services.Interfaces;

public interface IEmployeeService
{
    public Task<EmployeeCreateResponse> CreateAsync(EmployeeCreateRequest employee);
    public Task<EmployeeViewResponse> UpdateAsync(EmployeeUpdateRequest employee, int id);
    public Task<ICollection<EmployeeViewResponse>> GetByCompanyAsync(int companyId);
    public Task<ICollection<EmployeeViewResponse>> GetByDepartmentAsync(int companyId, string departmentName);
    public Task<bool> DeleteAsync(int id);
}