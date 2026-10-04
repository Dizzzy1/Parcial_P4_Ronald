using Microsoft.AspNetCore.Mvc;
using PrimerParcial1.Models;
using PrimerParcial1.Services;

namespace PrimerParcial1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NumberController(NumbersService numbersService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<NumberRecordGet>>> GetList()
    {
        var records = await numbersService.GetListAsync();

        return Ok(records);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NumberRecordGet>> GetById(int id)
    {
        var record = await numbersService.GetByIdAsync(id);

        if (record is null)
            return NotFound();

        return Ok(record);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Save(NumberRecordSet record)
    {
        var id = await numbersService.SaveAsync(record);

        return Ok(id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        NumberRecordSet record)
    {
        await numbersService.UpdateAsync(id, record);

        return NoContent();
    }
}