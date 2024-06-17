using Domain.Entities;
using Domain.Entities.Update;

namespace Application.Abstractions.Repository;

public interface IDepartmentRepository
{
    Task<Department> CreateAsync(Department department);
    Task<Department> GetByIdAsync(int id);
    Task<Department> GetByNameAsync(string name, int companyId);
    Task<Department> UpdateAsync(Department department, int id);
    Task<ICollection<Department>> GetAllByCompanyAsync(int companyId);
    Task<ICollection<Department>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
}