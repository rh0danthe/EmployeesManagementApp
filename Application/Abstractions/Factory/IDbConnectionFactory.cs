using System.Data;

namespace Application.Abstractions.FactoryInterfaces;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateAsync();

}