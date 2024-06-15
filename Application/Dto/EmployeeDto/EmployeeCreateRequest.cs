using Application.Dto.DepartmentDto;
using Application.Dto.PassportDto;

namespace Application.Dto.EmployeeDto;

public class EmployeeCreateRequest
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public int CompanyId { get; set; }
    public PassportRequest Passport { get; set; }
    public DepartmentViewRequest Department { get; set; }
}