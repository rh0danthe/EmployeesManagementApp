using Domain.Exceptions.Base;

namespace Domain.Exceptions.Company;

public class CompanyNotFound : NotFoundException
{
    public CompanyNotFound(string message) : base(message)
    {
    }
}