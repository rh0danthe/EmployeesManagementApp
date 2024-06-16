using Domain.Entities;
using Domain.Entities.Update;

namespace Application.Abstractions.Repository;

public interface IEmployeeRepository
{
    Task<Employee> CreateAsync(Employee employee);
    Task<bool> DeleteAsync(int employeeId);
    Task<Employee> GetByIdAsync(int id);
    Task<ICollection<Employee>> GetAllByCompanyAsync(int companyId);
    Task<ICollection<Employee>> GetAllByDepartmentAsync(int departmentId);
    Task<Employee> UpdateAsync(EmployeeUpdate employee, int employeeId);
}