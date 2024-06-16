namespace Application.Dto.Department;

public class DepartmentCreateRequest
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public int CompanyId { get; set; }
}