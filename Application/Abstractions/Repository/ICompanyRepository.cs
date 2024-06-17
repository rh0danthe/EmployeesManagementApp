using Domain.Entities;

namespace Application.Abstractions.Repository;

public interface ICompanyRepository
{
    Task<Company> CreateAsync(Company company);
    Task<Company> GetByIdAsync(int id);
    Task<Company> UpdateAsync(Company company, int id);
    Task<ICollection<Company>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
}