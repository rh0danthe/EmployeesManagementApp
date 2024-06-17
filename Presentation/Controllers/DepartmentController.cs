using System.Threading.Tasks;
using Application.Dto.Department;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Controller]
[Route("department")]
public class DepartmentController(IDepartmentService service) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] DepartmentDefaultRequest departmentDefault)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(await service.CreateAsync(departmentDefault));
    }

    [HttpPut("{departmentId}")]
    public async Task<IActionResult> UpdateAsync([FromBody] DepartmentDefaultRequest departmentDefault, [FromRoute] int departmentId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(await service.UpdateAsync(departmentDefault, departmentId));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await service.GetAllAsync());
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        return Ok(await service.DeleteAsync(id));
    }
}