using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Abstractions.RepositoryInterfaces;

public interface IEmployeeRepository
{
    Task<Employee> CreateAsync(Employee employee);
    Task<bool> DeleteAsync(int employeeId);
    Task<Employee> GetByIdAsync(int id);
    Task<ICollection<Employee>> GetAllByCompanyAsync(int companyId);
    Task<ICollection<Employee>> GetAllByDepartmentAsync(int departmentId);
    Task<Employee> UpdateAsync(Employee employee, int employeeId);
}