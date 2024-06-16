using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Abstractions.Factory;
using Application.Abstractions.Repository;
using Dapper;
using Domain.Entities;
using Domain.Entities.Update;

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
        
        var query = "INSERT INTO \"Employees\" (\"Name\", \"Surname\", \"Phone\", " +
                    "\"CompanyId\", \"DepartmentId\") VALUES(@Name, @Surname, @Phone, " +
                    "@CompanyId, @DepartmentId) RETURNING *";
        
        return await connection.QueryFirstOrDefaultAsync<Employee>(query, employee);
    }

    public async Task<bool> DeleteAsync(int employeeId)
    {
        using var connection = await _factory.CreateAsync();
        
        var query = "DELETE FROM \"Employees\" WHERE \"Id\" = @id";
        
        var res = await connection.ExecuteAsync(query, new { id = employeeId });
        
        return res > 0;
    }

    public async Task<Employee> GetByIdAsync(int id)
    {
        using var connection = await _factory.CreateAsync();
        
        var query = "SELECT * FROM \"Employees\" WHERE \"Id\" = @Id";
        
        return await connection.QueryFirstOrDefaultAsync<Employee>(query, new {Id = id});
    }
    
    public async Task<ICollection<Employee>> GetAllByCompanyAsync(int companyId)
    {
        using var connection = await _factory.CreateAsync();
        
        var query = "SELECT * FROM \"Employees\" WHERE \"CompanyId\" = @id";
        
        var employees = await connection.QueryAsync<Employee>(query, new { id = companyId });
        
        return employees.ToList();
    }

    public async Task<ICollection<Employee>> GetAllByDepartmentAsync(int departmentId)
    {
        using var connection = await _factory.CreateAsync();
        
        var query = "SELECT * FROM \"Employees\" WHERE \"Id\" = @id";
        
        var employees = await connection.QueryAsync<Employee>(query, new { id = departmentId });
        
        return employees.ToList();
    }

    public async Task<Employee> UpdateAsync(EmployeeUpdate employee, int employeeId)
    {
        using var connection = await _factory.CreateAsync();
        
        var query =
            "UPDATE \"Employees\" SET \"Name\" = COALESCE(@Name, \"Name\"), \"Surname\" = COALESCE(@Surname, \"Surname\"), " +
            "\"Phone\" = COALESCE(@Phone, \"Phone\"),\"CompanyId\" = COALESCE(@CompanyId, \"CompanyId\")," +
            " \"DepartmentId\" = COALESCE(@DepartmentId, \"DepartmentId\")" +
            " WHERE \"Id\" = @id RETURNING *";
        
        var parameters = new
        {
            Id = employeeId,
            Name = employee.Name,
            Surname = employee.Surname,
            Phone = employee.Phone,
            CompanyId = employee.CompanyId,
            DepartmentId = employee.DepartmentId
        };
        
        return await connection.QueryFirstOrDefaultAsync<Employee>(query, parameters);
    }
}