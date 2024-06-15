using Application.Dto.DepartmentDto;
using Application.Dto.PassportDto;

namespace Application.Dto.EmployeeDto;

public class EmployeeUpdateRequest
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Phone { get; set; }
    public int? CompanyId { get; set; }
    public PassportUpdateRequest? Passport { get; set; }
    public DepartmentUpdateRequest? Department { get; set; }
}