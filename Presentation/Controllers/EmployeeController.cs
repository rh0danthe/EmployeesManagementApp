﻿using Application.Dto.EmployeeDto;
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

        var res = await service.CreateAsync(employee);
        return Ok(res);
    }

    [HttpPatch("{employeeId}")]
    public async Task<IActionResult> UpdateAsync([FromBody] EmployeeUpdateRequest employee, [FromRoute] int employeeId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await service.UpdateAsync(employee, employeeId);
        return Ok(res);
    }

    [HttpGet("{companyId}")]
    public async Task<IActionResult> GetByCompanyAsync([FromRoute] int companyId)
    {
        return Ok(await service.GetByCompanyAsync(companyId));
    }
    
    /*[HttpGet("{companyId}")]
    public async Task<IActionResult> GetByDepartmentAsync([FromRoute] int id)
    {
        return Ok(await service.GetByCompanyAsync(id));
    }*/

    [HttpDelete("{employeeId}")]
    public async Task<IActionResult> Delete([FromRoute] int employeeId)
    {
        return Ok(await service.DeleteAsync(employeeId));
    }

}