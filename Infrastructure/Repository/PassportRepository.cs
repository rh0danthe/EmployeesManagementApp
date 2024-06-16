using System.Threading.Tasks;
using Application.Abstractions.Factory;
using Application.Abstractions.Repository;
using Dapper;
using Domain.Entities;
using Domain.Entities.Update;

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
        
        var query = "INSERT INTO \"Passports\" (\"Type\", \"Number\", \"EmployeeId\") VALUES(@Type, @Number, @EmployeeId) RETURNING *";
        
        return await connection.QueryFirstOrDefaultAsync<Passport>(query, passport);
    }

    public async Task<Passport> GetByEmployeeIdAsync(int id)
    {
        using var connection = await _factory.CreateAsync();
        
        string query = "SELECT * FROM \"Passports\" WHERE \"EmployeeId\" = @Id";
        
        return await connection.QueryFirstOrDefaultAsync<Passport>(query, new {Id = id});
    }

    public async Task<Passport> UpdateAsync(PassportUpdate passport, int id)
    {
        using var connection = await _factory.CreateAsync();
        
        var query =
            "UPDATE \"Passports\" SET \"Type\" = COALESCE(@Type, \"Type\"), \"Number\" = COALESCE(@Number, \"Number\") WHERE \"Id\" = @Id RETURNING *";
        
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