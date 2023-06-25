using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microwave.Domain.Entities;
using Microwave.Infra.AppDbContext;

namespace Microwave.Application.ErrorHandling;

[ApiController]
[Route("/error")]
public class ErrorController : ControllerBase
{
    private readonly MicrowaveDbContext _microwaveDbContext;

    public ErrorController(MicrowaveDbContext microwaveDbContext)
    {
        _microwaveDbContext = microwaveDbContext;
    }
    
    public async Task<IActionResult> Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        var exceptionLog = new ExceptionLog()
        {
            Id = Guid.NewGuid(),
            Exception = exception?.Message,
            InnerException = exception?.InnerException?.Message,
            Stacktrace = exception?.StackTrace,
        };

        await _microwaveDbContext.ExceptionsLog.AddAsync(exceptionLog);
        await _microwaveDbContext.SaveChangesAsync();
            
        return Problem();
    }
}