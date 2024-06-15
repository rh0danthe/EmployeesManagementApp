using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Abstractions.RepositoryInterfaces;

public interface IDepartmentRepository
{
    Task<Department> CreateAsync(Department department);
    Task<Department> GetByIdAsync(int id);
    Task<Department> GetByNameAsync(string name, int companyId);
    Task<Department> ReturnsIfExistsAsync(Department department);
    Task<Department> UpdateAsync(Department department, int id);
}