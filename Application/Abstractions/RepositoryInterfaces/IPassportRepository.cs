using Domain.Entities;

namespace Application.Abstractions.RepositoryInterfaces;

public interface IPassportRepository
{
    public Task<Passport> CreateAsync(Passport passport);
}