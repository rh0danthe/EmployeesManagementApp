using Application.Dto.Employee;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Controller]
[Route("employee")]
public class EmployeeController(IEmployeeService service) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] EmployeeCreateRequest employee)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(await service.CreateAsync(employee));
    }

    [HttpPatch("{employeeId}")]
    public async Task<IActionResult> UpdateAsync([FromBody] EmployeeUpdateRequest employee, [FromRoute] int employeeId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(await service.UpdateAsync(employee, employeeId));
    }

    [HttpGet("{companyId}")]
    public async Task<IActionResult> GetByCompanyAsync([FromRoute] int companyId)
    {
        return Ok(await service.GetByCompanyAsync(companyId));
    }
    
    [HttpGet("{companyId}/{departmentName}")]
    public async Task<IActionResult> GetByDepartmentAsync([FromRoute] int companyId, [FromRoute] string departmentName)
    {
        return Ok(await service.GetByDepartmentAsync(companyId, departmentName));
    }

    [HttpDelete("{employeeId}")]
    public async Task<IActionResult> Delete([FromRoute] int employeeId)
    {
        return Ok(await service.DeleteAsync(employeeId));
    }

}