using Domain.Entities;

namespace Application.Abstractions.RepositoryInterfaces;

public interface IEmployeeRepository
{
    public Task<Employee> CreateAsync(Employee employee);
    public Task<bool> DeleteAsync(int employeeId);
    public Task<ICollection<Employee>> GetAllByCompanyAsync(int companyId);
    public Task<ICollection<Employee>> GetAllByDepartmentAsync(int departmentId);
    public Task<Employee> UpdateAsync(Employee employee, int employeeId);
}