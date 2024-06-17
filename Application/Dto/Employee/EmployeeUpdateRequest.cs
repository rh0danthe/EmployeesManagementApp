using Application.Dto.Department;
using Application.Dto.Passport;

namespace Application.Dto.Employee;

public class EmployeeUpdateRequest
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Phone { get; set; }
    public int? CompanyId { get; set; }
    public PassportUpdateRequest? Passport { get; set; }
}