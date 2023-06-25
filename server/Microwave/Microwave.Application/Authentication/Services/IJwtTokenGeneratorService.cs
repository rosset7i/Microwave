using Microwave.Domain.Entities;

namespace Microwave.Application.Authentication.Services;

public interface IJwtTokenGeneratorService
{
    string GenerateToken(User user);
}