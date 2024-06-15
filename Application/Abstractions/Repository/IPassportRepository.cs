using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Abstractions.RepositoryInterfaces;

public interface IPassportRepository
{
    Task<Passport> CreateAsync(Passport passport);
    Task<Passport> GetByIdAsync(int id);
    Task<Passport> UpdateAsync(Passport passport, int id);
    Task<bool> DeleteAsync(int id);
}