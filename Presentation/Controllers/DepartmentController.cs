using Application.Dto.DepartmentDto;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Controller]
[Route("department")]
public class DepartmentController(IDepartmentService service) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] DepartmentCreateRequest department)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var res = await service.CreateAsync(department);
        return Ok(res);
    }

    [HttpPut("{departmentId}")]
    public async Task<IActionResult> UpdateAsync([FromBody] DepartmentCreateRequest department, [FromRoute] int departmentId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await service.UpdateAsync(department, departmentId);
        return Ok(res);
    }
}