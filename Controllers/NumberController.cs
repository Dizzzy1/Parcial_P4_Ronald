using Microsoft.AspNetCore.Mvc;
using PrimerParcial1.Models;
using PrimerParcial1.Services;

namespace PrimerParcial1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NumberController(NumbersService numbersService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var result = await numbersService.GetListAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await numbersService.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Save(NumberRecord record)
    {
        record.Fecha = DateTime.Now;

        var id = await numbersService.SaveAsync(record);

        record.Id = id;

        return Ok(record);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        NumberRecord record)
    {
        record.Id = id;

        await numbersService.UpdateAsync(record);

        return Ok(record);
    }
}