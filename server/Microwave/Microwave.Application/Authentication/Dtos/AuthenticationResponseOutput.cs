namespace Microwave.Application.Authentication.Dtos;

public record AuthenticationResponseOutput(
    string Email,
    string Token);