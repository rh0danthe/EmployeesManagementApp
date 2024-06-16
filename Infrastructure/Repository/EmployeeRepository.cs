using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Abstractions.Factory;
using Application.Abstractions.Repository;
using Dapper;
using Domain.Entities;
using Domain.Entities.Update;
using Infrastructure.Repository.Scripts;

namespace Infrastructure.Repository;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IDbConnectionFactory _factory;

    public EmployeeRepository(IDbConnectionFactory factory)
    {
        _factory = factory;
    }
    
    public async Task<Employee> CreateAsync(Employee employee)
    {
        using var connection = await _factory.CreateAsync();
        
        return await connection.QueryFirstOrDefaultAsync<Employee>(Resourses.CreateEmployee, employee);
    }

    public async Task<bool> DeleteAsync(int employeeId)
    {
        using var connection = await _factory.CreateAsync();
        
        var res = await connection.ExecuteAsync(Resourses.DeleteEmployee, new { id = employeeId });
        
        return res > 0;
    }

    public async Task<Employee> GetByIdAsync(int id)
    {
        using var connection = await _factory.CreateAsync();
        
        return await connection.QueryFirstOrDefaultAsync<Employee>(Resourses.GetEmployeeById, new {Id = id});
    }
    
    public async Task<ICollection<Employee>> GetAllByCompanyAsync(int companyId)
    {
        using var connection = await _factory.CreateAsync();
        
        var employees = await connection.QueryAsync<Employee>(Resourses.GetAllEmployeesByCompanyId, new { id = companyId });
        
        return employees.ToList();
    }

    public async Task<ICollection<Employee>> GetAllByDepartmentAsync(int departmentId)
    {
        using var connection = await _factory.CreateAsync();
        
        var employees = await connection.QueryAsync<Employee>(Resourses.GetAllEmployeesByDepartment, new { id = departmentId });
        
        return employees.ToList();
    }

    public async Task<Employee> UpdateAsync(EmployeeUpdate employee, int employeeId)
    {
        using var connection = await _factory.CreateAsync();
        
        var parameters = new
        {
            Id = employeeId,
            Name = employee.Name,
            Surname = employee.Surname,
            Phone = employee.Phone,
            CompanyId = employee.CompanyId,
            DepartmentId = employee.DepartmentId
        };
        
        return await connection.QueryFirstOrDefaultAsync<Employee>(Resourses.UpdateEmployee, parameters);
    }
}