using Domain.Exceptions.Base;

namespace Domain.Exceptions.Department;

public class DepartmentBadRequest : BadRequestException
{
    public DepartmentBadRequest(string message) : base(message)
    {
    }
}