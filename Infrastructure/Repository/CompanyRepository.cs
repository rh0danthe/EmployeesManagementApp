using Application.Abstractions.FactoryInterfaces;
using Application.Abstractions.RepositoryInterfaces;
using Dapper;
using Domain.Entities;

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
        var query = "INSERT INTO \"Companies\" (\"Name\") VALUES(@Name) RETURNING *";
        return await connection.QueryFirstOrDefaultAsync<Company>(query, company);
    }
}