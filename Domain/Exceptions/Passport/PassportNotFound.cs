using Domain.Exceptions.Base;

namespace Domain.Exceptions.Passport;

public class PassportNotFound : NotFoundException
{
    public PassportNotFound(string message) : base(message)
    {
    }
}