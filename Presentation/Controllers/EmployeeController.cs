using Application.Dto.Employee;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Presentation.Controllers;

[Controller]
[Route("employee")]
public class EmployeeController(IEmployeeService service) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] EmployeeCreateRequest employee, [FromQuery] [BindRequired] string departmentName)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(await service.CreateAsync(employee, departmentName));
    }

    [HttpPatch("{employeeId}")]
    public async Task<IActionResult> UpdateAsync([FromBody] EmployeeUpdateRequest employee, [FromRoute] int employeeId, [FromQuery] string? departmentName)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(await service.UpdateAsync(employee, employeeId, departmentName));
    }

    [HttpDelete("{employeeId}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int employeeId)
    {
        return Ok(await service.DeleteAsync(employeeId));
    }
}