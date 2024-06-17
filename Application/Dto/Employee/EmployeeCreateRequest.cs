using Application.Dto.Passport;

namespace Application.Dto.Employee;

public class EmployeeCreateRequest
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public int CompanyId { get; set; }
    public PassportRequest Passport { get; set; }
}