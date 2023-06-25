namespace Microwave.Application.Authentication.Dtos;

public record RegisterRequestInput(
    string FirstName,
    string LastName,
    string Email,
    string Password);