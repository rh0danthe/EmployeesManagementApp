using Application.Abstractions.FactoryInterfaces;
using Application.Abstractions.RepositoryInterfaces;
using Dapper;
using Domain.Entities;

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
                    "\"CompanyId\", \"DepartmentId\", \"PassportId\") VALUES(@Name, @Surname, @Phone, " +
                    "@CompanyId, @DepartmentId, @PassportId) RETURNING *";
        return await connection.QueryFirstOrDefaultAsync<Employee>(query, employee);
    }

    public async Task<bool> DeleteAsync(int employeeId)
    {
        using var connection = await _factory.CreateAsync();
        var query = "DELETE FROM \"Employees\" WHERE \"Id\" = @id";
        var res = await connection.ExecuteAsync(query, new { id = employeeId });
        return res > 0;
    }

    public async Task<ICollection<Employee>> GetAllByCompanyAsync(int companyId)
    {
        using var connection = await _factory.CreateAsync();
        var query = "SELECT * FROM \"Employees\" WHERE \"Id\" = @id";
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

    public async Task<Employee> UpdateAsync(Employee employee, int employeeId)
    {
        using var connection = await _factory.CreateAsync();
        var query =
            "UPDATE \"Employees\" SET \"Name\" = @Name, \"Surname\" = @Surname, \"Phone\" = @Phone, " +
            "\"CompanyId\" = @CompanyId, \"DepartmentId\" = @DepartmentId, \"PassportId\" = @PassportId" +
            " WHERE \"Id\" = @id RETURNING *";
        return await connection.QueryFirstOrDefaultAsync<Employee>(query, new { id = employeeId });
    }
}