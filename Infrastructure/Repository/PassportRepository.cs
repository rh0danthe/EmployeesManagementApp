using Application.Abstractions.FactoryInterfaces;
using Application.Abstractions.RepositoryInterfaces;
using Dapper;
using Domain.Entities;

namespace Infrastructure.Repository;

public class PassportRepository : IPassportRepository
{
    private readonly IDbConnectionFactory _factory;

    public PassportRepository(IDbConnectionFactory factory)
    {
        _factory = factory;
    }
    
    public async Task<Passport> CreateAsync(Passport passport)
    {
        using var connection = await _factory.CreateAsync();
        var query = "INSERT INTO \"Passports\" (\"Type\", \"Number\") VALUES(@Type, @Number) RETURNING *";
        return await connection.QueryFirstOrDefaultAsync<Passport>(query, passport);
    }

    public async Task<Passport> GetByIdAsync(int id)
    {
        using var connection = await _factory.CreateAsync();
        string query = "SELECT * FROM \"Passports\" WHERE \"Id\" = @Id";
        return await connection.QueryFirstOrDefaultAsync<Passport>(query, new {Id = id});
    }

    public async Task<Passport> UpdateAsync(Passport passport, int id)
    {
        using var connection = await _factory.CreateAsync();
        var query =
            "UPDATE \"Passports\" SET \"Type\" = @Type, \"Number\" = @Number WHERE \"Id\" = @Id RETURNING *";
        return await connection.QueryFirstOrDefaultAsync<Passport>(query, new { Id = id });
    }
}