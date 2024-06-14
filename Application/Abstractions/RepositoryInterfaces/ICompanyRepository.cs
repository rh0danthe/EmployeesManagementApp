using Domain.Entities;

namespace Application.Abstractions.RepositoryInterfaces;

public interface ICompanyRepository
{
    public Task<Company> CreateAsync(Company company);
}