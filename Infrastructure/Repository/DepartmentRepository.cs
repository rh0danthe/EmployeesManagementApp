using Application.Abstractions.Factory;
using Application.Abstractions.Repository;
using Dapper;
using Domain.Entities;
using Infrastructure.Repository.Scripts;

namespace Infrastructure.Repository;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly IDbConnectionFactory _factory;

    public DepartmentRepository(IDbConnectionFactory factory)
    {
        _factory = factory;
    }
    
    public async Task<Department> CreateAsync(Department department)
    {
        using var connection = await _factory.CreateAsync();
        
        return await connection.QueryFirstOrDefaultAsync<Department>(Resourses.CreateDepartment, department);
    }

    public async Task<Department> GetByIdAsync(int id)
    {
        using var connection = await _factory.CreateAsync();
        
        return await connection.QueryFirstOrDefaultAsync<Department>(Resourses.GetDepartmentById, new {Id = id});
    }

    public async Task<Department> GetByNameAsync(string name, int companyId)
    {
        using var connection = await _factory.CreateAsync();
        
        return await connection.QueryFirstOrDefaultAsync<Department>(Resourses.GetDepartmentByName, new {Name = name, CompanyId = companyId});
    }

    public async Task<Department> UpdateAsync(Department department, int departmentId)
    {
        using var connection = await _factory.CreateAsync();
        
        var parameters = new
        {
            Id = departmentId,
            Name = department.Name,
            Phone = department.Phone,
            CompanyId = department.CompanyId
        };
        
        return await connection.QueryFirstOrDefaultAsync<Department>(Resourses.UpdateDepartment, parameters);
    }

    public async Task<ICollection<Department>> GetAllByCompanyAsync(int companyId)
    {
        using var connection = await _factory.CreateAsync();
        return (await connection.QueryAsync<Department>(Resourses.GetAllDepartmentsByCompanyId, new {CompanyId = companyId})).ToList();
    }

    public async Task<ICollection<Department>> GetAllAsync()
    {
        using var connection = await _factory.CreateAsync();
        return (await connection.QueryAsync<Department>(Resourses.GetAllDepartments)).ToList();
    }

    public async Task<bool> DeleteAsync(int departmentId)
    {
        using var connection = await _factory.CreateAsync();
        
        var res = await connection.ExecuteAsync(Resourses.DeleteDepartment, new { id = departmentId });
        
        return res > 0;
    }
}