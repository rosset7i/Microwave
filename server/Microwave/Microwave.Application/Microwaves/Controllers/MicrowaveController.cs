using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microwave.Application.ErrorHandling;
using Microwave.Application.Microwaves.Dtos;
using Microwave.Application.Microwaves.Services;

namespace Microwave.Application.Microwaves.Controllers;

[Route("api/v1/microwaves")]
public class MicrowaveController : ApiController
{
    private readonly IMicrowaveService _microwaveService;

    public MicrowaveController(IMicrowaveService microwaveService)
    {
        _microwaveService = microwaveService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllMicrowaves()
    {
        return Ok(await _microwaveService.GetAllMicrowaveTemplates());
    }

    [HttpGet("{idMicrowave:guid}")]
    [Authorize]
    public async Task<IActionResult> GetMicrowave([FromRoute] Guid idMicrowave)
    {
        var microwaveOutput = await _microwaveService.GetMicrowave(idMicrowave);

        return microwaveOutput.Match(
            result => Ok(result),
            error => Problem(error));
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateMicrowave([FromBody] MicrowaveOutput microwave)
    {
        await _microwaveService.CreateMicrowave(microwave);

        return NoContent();
    }
    
    [HttpPut("{idMicrowave:guid}")]
    [Authorize]
    public async Task<IActionResult> CreateMicrowave([FromRoute]Guid idMicrowave, [FromBody] MicrowaveOutput microwave)
    {
        var result = await _microwaveService.UpdateMicrowave(idMicrowave, microwave);

        return result.Match(
            result=> NoContent(),
            error => Problem(error));
    }
    
    [HttpDelete("{idMicrowave:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteMicrowave([FromRoute]Guid idMicrowave)
    {
        var result = await _microwaveService.DeleteMicrowave(idMicrowave);

        return result.Match(
            result=> NoContent(),
            error => Problem(error));
    }
}