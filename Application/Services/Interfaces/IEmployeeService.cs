using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dto.Employee;

namespace Application.Services.Interfaces;

public interface IEmployeeService
{
    Task<EmployeeCreateResponse> CreateAsync(EmployeeCreateRequest employee, string departmentName);
    Task<EmployeeViewResponse> UpdateAsync(EmployeeUpdateRequest employee, int id, string departmentName);
    Task<ICollection<EmployeeViewResponse>> GetAllByCompanyAsync(int companyId);
    Task<ICollection<EmployeeViewResponse>> GetAllByDepartmentAsync(int companyId, string departmentName);
    Task<bool> DeleteAsync(int id);
}