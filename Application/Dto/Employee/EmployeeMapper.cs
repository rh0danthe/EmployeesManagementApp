using Application.Dto.Department;
using Application.Dto.Passport;

using DomainEmployee = Domain.Entities.Employee;
using DomainDepartment = Domain.Entities.Department;
using DomainPassport = Domain.Entities.Passport;

namespace Application.Dto.Employee;

public static class EmployeeMapper
{
    public static EmployeeViewResponse MapToViewResponse(DomainEmployee employee, DomainDepartment department, DomainPassport passport)
    {
        return new EmployeeViewResponse()
        {
            Id = employee.Id,
            Name = employee.Name,
            Surname = employee.Surname,
            Phone = employee.Phone,
            CompanyId = employee.CompanyId,
            Department = DepartmentMapper.MapToViewResponse(department),
            Passport = PassportMapper.MapToViewResponse(passport)
        };
    }
}