using ErrorOr;
using Microwave.Application.Authentication.Dtos;

namespace Microwave.Application.Authentication.Services;

public interface IAuthenticationService
{
    Task<ErrorOr<ValueTask>> Register(RegisterRequestInput registerRequestInput);
    Task<ErrorOr<AuthenticationResponseOutput>> Login(LoginRequestInput loginRequestInput);
}