using Application.Dto.Company;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Controller]
[Route("company")]
public class CompanyController(ICompanyService companyService, IEmployeeService employeeService, IDepartmentService departmentService) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CompanyRequest company)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(await companyService.CreateAsync(company));
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync([FromBody] CompanyRequest company, int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(await companyService.UpdateAsync(company, id));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await companyService.GetAllAsync());
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        return Ok(await companyService.DeleteAsync(id));
    }
    
    [HttpGet("{id:int}/department/{departmentName:required}/employees")]
    public async Task<IActionResult> GetEmployeesByDepartmentAsync(int id, string departmentName)
    {
        return Ok(await employeeService.GetAllByDepartmentAsync(id, departmentName));
    }
    
    [HttpGet("{id:int}/employees")]
    public async Task<IActionResult> GetEmployeesByCompanyAsync(int id)
    {
        return Ok(await employeeService.GetAllByCompanyAsync(id));
    }
    
    [HttpGet("{id:int}/department")]
    public async Task<IActionResult> GetDepartmentByCompanyAsync(int id)
    {
        return Ok(await departmentService.GetAllByCompanyAsync(id));
    }
}