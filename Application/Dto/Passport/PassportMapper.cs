namespace Application.Dto.Passport;

public static class PassportMapper
{
    public static PassportResponse MapToViewResponse(Domain.Entities.Passport passport)
    {
        return new PassportResponse()
        {
            Type = passport.Type,
            Number = passport.Number
        };
    }
}