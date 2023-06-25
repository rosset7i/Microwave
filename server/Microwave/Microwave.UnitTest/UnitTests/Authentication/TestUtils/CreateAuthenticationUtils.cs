using Microwave.Application.Authentication.Dtos;
using Microwave.Domain.Entities;
using Microwave.UnitTest.TestUtils;

namespace Microwave.UnitTest.UnitTests.Authentication.TestUtils;

public static class CreateAuthenticationUtils
{
    public static RegisterRequestInput CreateRegisterRequestInput() =>
        new(Constants.User.FirstName,
            Constants.User.LastName,
            Constants.User.Email,
            Constants.User.Password);

    public static LoginRequestInput CreateLoginRequestInput() =>
        new(Constants.User.Email,
            Constants.User.Password);

    public static AuthenticationResponseOutput CreateLoginResponseOutput() =>
        new(Constants.User.Email,
            Constants.User.Token);

    public static User CreateUser() =>
        new(Guid.NewGuid(),
            Constants.User.FirstName,
            Constants.User.LastName,
            Constants.User.Email,
            new byte[64],
            new byte[64]);
}