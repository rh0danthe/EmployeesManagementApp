using System.Data;

namespace Application.Abstractions.Factory;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateAsync();

}