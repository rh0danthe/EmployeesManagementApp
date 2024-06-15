using Application.Dto.DepartmentDto;
using Application.Dto.PassportDto;

namespace Application.Dto.EmployeeDto;

public class EmployeeViewResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public int CompanyId { get; set; }
    public PassportResponse Passport { get; set; }
    public DepartmentViewResponse Department { get; set; }
}