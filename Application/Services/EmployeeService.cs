using Application.Abstractions.Repository;
using Application.Dto.Employee;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Entities.Update;
using Domain.Exceptions.Department;
using Domain.Exceptions.Employee;
using Domain.Exceptions.Passport;

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
        var dbDepartment = await _departmentRepository.GetByNameAsync(employee.Department.Name, employee.CompanyId);

        if (dbDepartment is null) throw new DepartmentNotFound($"Validating input data error: Department '{employee.Department.Name}' does not exist in company with id {employee.CompanyId}");
        
        var createEmployee = new Employee()
        {
            Name = employee.Name,
            Surname = employee.Surname,
            Phone = employee.Phone,
            CompanyId = employee.CompanyId,
            DepartmentId = dbDepartment.Id
        };

        var dbEmployee = await _employeeRepository.CreateAsync(createEmployee);
        
        if (dbEmployee is null) throw new EmployeeBadRequest("Error while creating the employee");
        
        var passport = new Passport()
        {
            Number = employee.Passport.Number,
            Type = employee.Passport.Type,
            EmployeeId = dbEmployee.Id
        };

        var dbPassport = await _passportRepository.CreateAsync(passport);

        if (dbPassport is null) throw new PassportBadRequest($"Error while creating passport for employee with id {dbEmployee.Id}");
        
        var response = new EmployeeCreateResponse() { Id = dbEmployee.Id };
        return response;
    }

    public async Task<EmployeeViewResponse> UpdateAsync(EmployeeUpdateRequest employee, int id)
    {
        var dbEmployee = await _employeeRepository.GetByIdAsync(id);
        
        if (dbEmployee is null) throw new EmployeeNotFound($"Validating input data error: Employee with id {id} does not exist");
        
        var dbPassport = await _passportRepository.GetByEmployeeIdAsync(id);
        
        if (dbPassport is null) throw new PassportNotFound($"Validating input data error: Passport for employee with id {id} is not found");
        
        var dbDepartment = await _departmentRepository.GetByIdAsync(dbEmployee.DepartmentId);
        
        if (dbDepartment is null) throw new DepartmentNotFound($"Validating input data error: Department with id {dbEmployee.DepartmentId} in" +
                                                               $" company with id {dbEmployee.CompanyId} for employee with id {id} does not exist");
        
        Department updatedDepartment;
        
        if (employee.Department is not null)
        {
            var paramDepartment = new Department()
            {
                Name = employee.Department.Name ?? dbDepartment.Name,
                Phone = employee.Department.Phone ?? dbDepartment.Phone,
                CompanyId = employee.CompanyId ?? dbEmployee.CompanyId
            };
            updatedDepartment = await _departmentRepository.ReturnsIfExistsAsync(paramDepartment);
            
            if (updatedDepartment is null) throw new DepartmentBadRequest($"Validating input data error: " +
                                                                          $"Department with name {paramDepartment.Name}" +
                                                                          $"does not exist in company with id {paramDepartment.CompanyId}");
        }
        else updatedDepartment = dbDepartment;
        
        var updatedEmployee = await _employeeRepository.UpdateAsync(new EmployeeUpdate()
        {
            Name = employee.Name,
            Surname = employee.Surname,
            Phone = employee.Phone,
            CompanyId = employee.CompanyId,
            DepartmentId = updatedDepartment.Id
        }, id);
        
        if (updatedEmployee is null) throw new EmployeeBadRequest("Error while updating the employee");

        var updatedPassport = await _passportRepository.UpdateAsync(new PassportUpdate()
        {
            Number = employee.Passport.Number,
            Type = employee.Passport.Type
        }, dbPassport.Id);
        
        if (updatedPassport is null)
            throw new PassportBadRequest($"Error while updating passport for employee with id {updatedEmployee.Id}");
        
        return EmployeeMapper.MapToViewResponse(updatedEmployee, updatedDepartment, updatedPassport);
    }

    public async Task<ICollection<EmployeeViewResponse>> GetByCompanyAsync(int companyId)
    {
        var employees = await _employeeRepository.GetAllByCompanyAsync(companyId);
        
        var result = new List<EmployeeViewResponse>();
        
        foreach (var employee in employees)
        {
            var department = await _departmentRepository.GetByIdAsync(employee.DepartmentId);

            if (department is null) throw new DepartmentNotFound($"Department for employee with id {employee.Id} does not exist");
            
            var passport = await _passportRepository.GetByEmployeeIdAsync(employee.Id);
            
            if (passport is null) throw new PassportNotFound($"Passport for employee with id {employee.Id} does not exist");

            var employeeResponse = EmployeeMapper.MapToViewResponse(employee, department, passport);
            
            result.Add(employeeResponse);
        }

        return result;
    }


    public async Task<ICollection<EmployeeViewResponse>> GetByDepartmentAsync(int companyId, string departmentName)
    {
        var dbDepartment = _departmentRepository.GetByNameAsync(departmentName, companyId);
        
        if (dbDepartment is null) throw new DepartmentNotFound($"Department with name {departmentName} in " +
                                                               $"company with id {companyId} does not exist");

        var employees = await _employeeRepository.GetAllByDepartmentAsync(dbDepartment.Id);
        
        var result = new List<EmployeeViewResponse>();
        
        foreach (var employee in employees)
        {
            var department = await _departmentRepository.GetByIdAsync(employee.DepartmentId);
            
            if (department is null) throw new DepartmentNotFound($"Department for employee with id {employee.Id} does not exist");
            
            var passport = await _passportRepository.GetByEmployeeIdAsync(employee.Id);
            
            if (passport is null) throw new PassportNotFound($"Passport for employee with id {employee.Id} does not exist");

            var employeeResponse = EmployeeMapper.MapToViewResponse(employee, department, passport);
            
            result.Add(employeeResponse);
        }

        return result;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _employeeRepository.DeleteAsync(id);
    }
}