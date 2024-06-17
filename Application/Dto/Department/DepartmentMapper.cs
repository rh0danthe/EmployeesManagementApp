namespace Application.Dto.Department;

public static class DepartmentMapper
{
    public static DepartmentDefaultResponse MapToDefaultResponse(Domain.Entities.Department department)
    {
        return new DepartmentDefaultResponse()
        {
            Id = department.Id,
            Name = department.Name,
            Phone = department.Phone,
            CompanyId = department.CompanyId
        };
    }
    
    public static DepartmentViewResponse MapToViewResponse(Domain.Entities.Department department)
    {
        return new DepartmentViewResponse()
        {
            Name = department.Name,
            Phone = department.Phone
        };
    }
}