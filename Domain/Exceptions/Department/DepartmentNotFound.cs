using Domain.Exceptions.Base;

namespace Domain.Exceptions.Department;

public class DepartmentNotFound : NotFoundException
{
    public DepartmentNotFound(string message) : base(message)
    {
    }
}