using Application.Abstractions.RepositoryInterfaces;
using Application.Dto.DepartmentDto;
using Application.Dto.EmployeeDto;
using Application.Dto.PassportDto;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Exceptions.Base;

namespace Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IPassportRepository _passportRepository;

    public EmployeeService(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, IPassportRepository passportRepository)
    {
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
        _passportRepository = passportRepository;
    }
    
    public async Task<EmployeeCreateResponse> CreateAsync(EmployeeCreateRequest employee)
    {
        var dbPassport = new Passport()
        {
            Number = employee.Passport.Number,
            Type = employee.Passport.Type
        };

        var dbDepartment = await _departmentRepository.GetByNameAsync(employee.Department.Name, employee.CompanyId);
        
        var dbEmployee = new Employee()
        {
            Name = employee.Name,
            Surname = employee.Surname,
            Phone = employee.Phone,
            CompanyId = employee.CompanyId,
            DepartmentId = dbDepartment.Id,
            PassportId = dbPassport.Id
        };
        var response = new EmployeeCreateResponse() { Id = (await _employeeRepository.CreateAsync(dbEmployee)).Id };
        return response;
    }

    public async Task<EmployeeViewResponse> UpdateAsync(EmployeeUpdateRequest employee, int id)
    {
        var dbEmployee = await _employeeRepository.GetByIdAsync(id);
        var dbPassport = await _passportRepository.GetByIdAsync(dbEmployee.PassportId);
        var dbDepartment = await _departmentRepository.GetByIdAsync(dbEmployee.DepartmentId);
        
        bool flag = false;
        var updatedDepartment = new Department();
        if (dbDepartment.Name != employee.Department.Name && employee.Department.Name != null)
        {
            flag = true;
            updatedDepartment.Name = employee.Department.Name;
        }
        else updatedDepartment.Name = dbDepartment.Name;

        if (dbDepartment.Phone != employee.Department.Phone && employee.Department.Phone != null)
        {
            flag = true;
            updatedDepartment.Phone = employee.Department.Phone;
        }
        else updatedDepartment.Phone = dbDepartment.Phone;

        updatedDepartment.CompanyId = employee.CompanyId ?? dbEmployee.CompanyId;
        if (flag)
        {
            var ifExists = await _departmentRepository.CheckIfExistsAsync(updatedDepartment);
            if (!ifExists) throw new Exception("sfgdgsdf");
        }

        var updatedEmployee = new Employee()
        {
            Id = id,
            Name = employee.Name ?? dbEmployee.Name,
            Surname = employee.Surname ?? dbEmployee.Surname,
            Phone = employee.Phone ?? dbEmployee.Phone,
            CompanyId = employee.CompanyId ?? dbEmployee.CompanyId,
            PassportId = dbPassport.Id,
            DepartmentId = updatedDepartment.CompanyId
        };

        var updatedPassport = new Passport();
        flag = false;
        if (dbPassport.Number != employee.Passport.Number && employee.Passport.Number != null)
        {
            flag = true;
            updatedPassport.Number = employee.Passport.Number;
        }
        else updatedPassport.Number = dbPassport.Number;

        if (dbPassport.Type != employee.Passport.Type && employee.Passport.Type != null)
        {
            flag = true;
            updatedPassport.Type = employee.Passport.Type;
        }
        else updatedPassport.Type = dbPassport.Type;

        if (flag) await _passportRepository.UpdateAsync(updatedPassport, dbPassport.Id);

        return MapEmployeeToResponse(await _employeeRepository.UpdateAsync(updatedEmployee, dbEmployee.Id),
            updatedDepartment, updatedPassport);
    }

    public async Task<ICollection<EmployeeViewResponse>> GetByCompanyAsync(int companyId)
    {
        var employees = await _employeeRepository.GetAllByCompanyAsync(companyId);
        var result = new List<EmployeeViewResponse>();
        foreach (var employee in employees)
        {
            var department = await _departmentRepository.GetByIdAsync(employee.DepartmentId);
            var passport = await _passportRepository.GetByIdAsync(employee.PassportId);

            var employeeResponse = MapEmployeeToResponse(employee, department, passport);
            result.Add(employeeResponse);
        }

        return result;
    }


    public async Task<ICollection<EmployeeViewResponse>> GetByDepartmentAsync(int companyId, string departmentName)
    {
        var dbDepartment = _departmentRepository.GetByNameAsync(departmentName, companyId);
        if (dbDepartment == null) throw new Exception("dsf");

        var employees = await _employeeRepository.GetAllByDepartmentAsync(dbDepartment.Id);
        var result = new List<EmployeeViewResponse>();
        foreach (var employee in employees)
        {
            var department = await _departmentRepository.GetByIdAsync(employee.DepartmentId);
            var passport = await _passportRepository.GetByIdAsync(employee.PassportId);

            var employeeResponse = MapEmployeeToResponse(employee, department, passport);
            result.Add(employeeResponse);
        }

        return result;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _employeeRepository.DeleteAsync(id);
    }
    
    private EmployeeViewResponse MapEmployeeToResponse(Employee employee, Department department, Passport passport)
    {
        return new EmployeeViewResponse()
        {
            Id = employee.Id,
            Name = employee.Name,
            Surname = employee.Surname,
            Phone = employee.Phone,
            CompanyId = employee.CompanyId,
            Department = MapDepartmentToResponse(department),
            Passport = MapPassportToResponse(passport)
        };
    }
    
    private DepartmentViewResponse MapDepartmentToResponse(Department department)
    {
        return new DepartmentViewResponse()
        {
            Name = department.Name,
            Phone = department.Phone
        };
    }
    
    private PassportResponse MapPassportToResponse(Passport passport)
    {
        return new PassportResponse()
        {
            Type = passport.Type,
            Number = passport.Number
        };
    }
}