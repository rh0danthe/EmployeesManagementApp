using Domain.Entities;

namespace Application.Abstractions.RepositoryInterfaces;

public interface IPassportRepository
{
    public Task<Passport> CreateAsync(Passport passport);
    
    public Task<Passport> GetByIdAsync(int id);
    public Task<Passport> UpdateAsync(Passport passport, int id);
}