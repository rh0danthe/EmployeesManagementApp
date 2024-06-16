using System.Threading.Tasks;
using Application.Abstractions.Factory;
using Application.Abstractions.Repository;
using Dapper;
using Domain.Entities;
using Infrastructure.Repository.Scripts;

namespace Infrastructure.Repository;

public class CompanyRepository : ICompanyRepository
{
    private readonly IDbConnectionFactory _factory;

    public CompanyRepository(IDbConnectionFactory factory)
    {
        _factory = factory;
    }
    
    public async Task<Company> CreateAsync(Company company)
    {
        using var connection = await _factory.CreateAsync();
        
        return await connection.QueryFirstOrDefaultAsync<Company>(Resourses.CreateCompany, company);
    }

    public async Task<Company> GetByIdAsync(int id)
    {
        using var connection = await _factory.CreateAsync();
        
        return await connection.QueryFirstOrDefaultAsync<Company>(Resourses.GetCompanyById, new {Id = id});
    }
}