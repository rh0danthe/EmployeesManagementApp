using Domain.Exceptions.Base;

namespace Domain.Exceptions.Employee;

public class EmployeeNotFound : NotFoundException
{
    public EmployeeNotFound(string message) : base(message)
    {
    }
}