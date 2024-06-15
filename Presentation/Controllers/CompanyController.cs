using Application.Dto.CompanyDto;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Controller]
[Route("company")]
public class CompanyController : Controller
{
    private readonly ICompanyService _service;

    public CompanyController(ICompanyService service)
    {
        _service = service;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CompanyRequest company)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var res = await _service.CreateAsync(company);
        return Ok(res);
    }
}