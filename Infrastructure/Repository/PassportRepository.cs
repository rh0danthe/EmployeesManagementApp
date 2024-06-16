using System.Threading.Tasks;
using Application.Abstractions.Factory;
using Application.Abstractions.Repository;
using Dapper;
using Domain.Entities;
using Domain.Entities.Update;
using Infrastructure.Repository.Scripts;

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
        
        return await connection.QueryFirstOrDefaultAsync<Passport>(Resourses.CreatePassport, passport);
    }

    public async Task<Passport> GetByEmployeeIdAsync(int id)
    {
        using var connection = await _factory.CreateAsync();
        
        return await connection.QueryFirstOrDefaultAsync<Passport>(Resourses.GetPassportByEmployeesId, new {Id = id});
    }

    public async Task<Passport> UpdateAsync(PassportUpdate passport, int id)
    {
        using var connection = await _factory.CreateAsync();
        
        var parameters = new
        {
            Id = id,
            Type = passport.Type,
            Number = passport.Number
        };
        
        return await connection.QueryFirstOrDefaultAsync<Passport>(Resourses.UpdatePassport, parameters);
    }

    public async Task<bool> CheckIfExistsByNumber(string number)
    {
        using var connection = await _factory.CreateAsync();
        var res = await connection.QueryFirstOrDefaultAsync<Passport>(Resourses.CheckPassportIfExistByNumber, new { Number = number });
        return res is not null;
    }
}