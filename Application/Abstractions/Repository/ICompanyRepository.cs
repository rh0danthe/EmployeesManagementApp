using Domain.Entities;

namespace Application.Abstractions.Repository;

public interface ICompanyRepository
{
    Task<Company> CreateAsync(Company company);
}