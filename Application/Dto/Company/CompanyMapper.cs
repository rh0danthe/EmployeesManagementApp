namespace Application.Dto.Company;

public static class CompanyMapper
{
    public static CompanyResponse MapToResponse(Domain.Entities.Company company)
    {
        return new CompanyResponse()
        {
            Id = company.Id,
            Name = company.Name
        };
    }
}