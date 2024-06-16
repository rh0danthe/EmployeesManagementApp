using System.Threading.Tasks;
using Application.Dto.Company;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Controller]
[Route("company")]
public class CompanyController(ICompanyService service) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CompanyRequest company)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(await service.CreateAsync(company));
    }
}