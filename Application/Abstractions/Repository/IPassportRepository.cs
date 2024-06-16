using Domain.Entities;
using Domain.Entities.Update;

namespace Application.Abstractions.Repository;

public interface IPassportRepository
{
    Task<Passport> CreateAsync(Passport passport);
    Task<Passport> GetByEmployeeIdAsync(int id);
    Task<Passport> UpdateAsync(PassportUpdate passport, int id);
    Task<bool> DeleteAsync(int id);
}