using Domain.Exceptions.Base;

namespace Domain.Exceptions.Passport;

public class PassportBadRequest : BadRequestException
{
    public PassportBadRequest(string message) : base(message)
    {
    }
}