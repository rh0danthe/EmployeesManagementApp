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
        var parameters = new
        {
            Id = id,
            Type = passport.Type,
            Number = passport.Number
        };
        return await connection.QueryFirstOrDefaultAsync<Passport>(query, parameters);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = await _factory.CreateAsync();
        var query = "DELETE FROM \"Passports\" WHERE \"Id\" = @Id";
        var res = await connection.ExecuteAsync(query, new { Id = id });
        return res > 0;
    }
}