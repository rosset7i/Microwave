using Microsoft.AspNetCore.Mvc;
using Microwave.Application.Authentication.Dtos;
using Microwave.Application.Authentication.Services;
using Microwave.Application.ErrorHandling;

namespace Microwave.Application.Authentication.Controllers;

[Route("api/v1/authentication")]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestInput registerRequestInput)
    {
        var response = await _authenticationService.Register(registerRequestInput);

        return response.Match(
            _ => Ok(),
            errors => Problem(errors));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestInput loginRequestInput)
    {
        var user = await _authenticationService.Login(loginRequestInput);

        return user.Match(
            result => Ok(result),
            errors => Problem(errors));
    }
}