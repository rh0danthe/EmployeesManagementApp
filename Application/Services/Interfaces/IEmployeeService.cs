using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dto.EmployeeDto;

namespace Application.Services.Interfaces;

public interface IEmployeeService
{
    Task<EmployeeCreateResponse> CreateAsync(EmployeeCreateRequest employee);
    Task<EmployeeViewResponse> UpdateAsync(EmployeeUpdateRequest employee, int id);
    Task<ICollection<EmployeeViewResponse>> GetByCompanyAsync(int companyId);
    Task<ICollection<EmployeeViewResponse>> GetByDepartmentAsync(int companyId, string departmentName);
    Task<bool> DeleteAsync(int id);
}