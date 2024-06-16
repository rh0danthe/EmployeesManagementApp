using Application.Abstractions.Repository;
using Application.Dto.Employee;
using Application.Services.Interfaces;
using Application.Utils;
using Domain.Entities;
using Domain.Entities.Update;
using Domain.Exceptions.Company;
using Domain.Exceptions.Department;
using Domain.Exceptions.Employee;
using Domain.Exceptions.Passport;

namespace Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IPassportRepository _passportRepository;
    private readonly ICompanyRepository _companyRepository;

    public EmployeeService(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, IPassportRepository passportRepository, ICompanyRepository companyRepository)
    {
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
        _passportRepository = passportRepository;
        _companyRepository = companyRepository;
    }
    
    public async Task<EmployeeCreateResponse> CreateAsync(EmployeeCreateRequest employee)
    {
        var dbCompany = await _companyRepository.GetByIdAsync(employee.CompanyId);

        if (dbCompany is null)
            throw new CompanyNotFound($"Company with id {employee.CompanyId} does not exist");
        
        var dbDepartment = await _departmentRepository.GetByNameAsync(StringCleaner.CleanInput(employee.Department.Name), employee.CompanyId);

        if (dbDepartment is null) throw new DepartmentNotFound($"Validating input data error: Department '{StringCleaner.CleanInput(employee.Department.Name)}' " +
                                                               $"does not exist in company with id {employee.CompanyId}");
        if (await _passportRepository.CheckIfExistsByNumber(StringCleaner.CleanInput(employee.Passport.Number)))
            throw new PassportBadRequest(
                $"Passport with number {StringCleaner.CleanInput(employee.Passport.Number)} already exists");
        
        var createEmployee = new Employee()
        {
            Name = StringCleaner.CleanInput(employee.Name),
            Surname = StringCleaner.CleanInput(employee.Surname),
            Phone = StringCleaner.CleanInput(employee.Phone),
            CompanyId = employee.CompanyId,
            DepartmentId = dbDepartment.Id
        };

        var dbEmployee = await _employeeRepository.CreateAsync(createEmployee);
        
        if (dbEmployee is null) throw new EmployeeBadRequest("Error while creating the employee");
        
        var passport = new Passport()
        {
            Number = StringCleaner.CleanInput(employee.Passport.Number),
            Type = StringCleaner.CleanInput(employee.Passport.Type),
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
        
        var dbCompany = await _companyRepository.GetByIdAsync(employee.CompanyId ?? dbEmployee.CompanyId);

        if (dbCompany is null)
            throw new CompanyNotFound($"Company with id {employee.CompanyId ?? dbEmployee.CompanyId} does not exist");
        
        var dbPassport = await _passportRepository.GetByEmployeeIdAsync(id);
        
        if (dbPassport is null) throw new PassportNotFound($"Validating input data error: Passport for employee with id {id} is not found");
        
        if (employee.Passport is not null && employee.Passport.Number != dbPassport.Number)
        {
            if (await _passportRepository.CheckIfExistsByNumber(StringCleaner.CleanInput(employee.Passport.Number)))
                throw new PassportBadRequest(
                    $"Passport with number {StringCleaner.CleanInput(employee.Passport.Number)} already exists");
        }
        
        var dbDepartment = await _departmentRepository.GetByIdAsync(dbEmployee.DepartmentId);
        
        if (dbDepartment is null) throw new DepartmentNotFound($"Validating input data error: Department with id {dbEmployee.DepartmentId} in" +
                                                               $" company with id {dbEmployee.CompanyId} for employee with id {id} does not exist");
        
        Department updatedDepartment;
        
        if (employee.Department is not null)
        {
            var name = StringCleaner.CleanInput(employee.Department.Name) ?? dbDepartment.Name;
            var companyId = employee.CompanyId ?? dbEmployee.CompanyId;
            updatedDepartment = await _departmentRepository.GetByNameAsync(name, companyId);
            
            if (updatedDepartment is null) throw new DepartmentBadRequest($"Validating input data error: " +
                                                                          $"Department with name {name} " +
                                                                          $"does not exist in company with id {companyId}");
        }
        else updatedDepartment = dbDepartment;
        
        var updatedEmployee = await _employeeRepository.UpdateAsync(new EmployeeUpdate()
        {
            Name = StringCleaner.CleanInput(employee.Name),
            Surname = StringCleaner.CleanInput(employee.Surname),
            Phone = StringCleaner.CleanInput(employee.Phone),
            CompanyId = employee.CompanyId,
            DepartmentId = updatedDepartment.Id
        }, id);
        
        if (updatedEmployee is null) throw new EmployeeBadRequest("Error while updating the employee");
        Passport updatedPassport;
        if (employee.Passport is not null)
        {
            updatedPassport = await _passportRepository.UpdateAsync(new PassportUpdate
            {
                Number = StringCleaner.CleanInput(employee.Passport.Number),
                Type = StringCleaner.CleanInput(employee.Passport.Type)
            }, dbPassport.Id);

            if (updatedPassport is null)
                throw new PassportBadRequest(
                    $"Error while updating passport for employee with id {updatedEmployee.Id}");
        }

        updatedPassport = dbPassport;

        return EmployeeMapper.MapToViewResponse(updatedEmployee, updatedDepartment, updatedPassport);
    }

    public async Task<ICollection<EmployeeViewResponse>> GetByCompanyAsync(int companyId)
    {
        var dbCompany = await _companyRepository.GetByIdAsync(companyId);

        if (dbCompany is null)
            throw new CompanyNotFound($"Company with id {companyId} does not exist");
        
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
        var dbDepartment = _departmentRepository.GetByNameAsync(StringCleaner.CleanInput(departmentName), companyId);
        
        if (dbDepartment is null) throw new DepartmentNotFound($"Department with name {StringCleaner.CleanInput(departmentName)} in " +
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