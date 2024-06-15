using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Abstractions.RepositoryInterfaces;

public interface ICompanyRepository
{
    Task<Company> CreateAsync(Company company);
}