using Domain.Exceptions.Base;

namespace Domain.Exceptions.Employee;

public class EmployeeBadRequest : BadRequestException
{
    public EmployeeBadRequest(string message) : base(message)
    {
    }
}