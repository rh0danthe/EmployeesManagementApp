using Domain.Exceptions.Base;

namespace Domain.Exceptions.Company;

public class CompanyBadRequest : BadRequestException
{
    public CompanyBadRequest(string message) : base(message)
    {
    }
}