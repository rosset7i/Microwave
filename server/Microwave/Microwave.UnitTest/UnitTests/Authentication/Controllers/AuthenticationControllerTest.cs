using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Microwave.Application.Authentication.Controllers;
using Microwave.Application.Authentication.Services;
using Microwave.Domain.Common.Errors;
using Microwave.UnitTest.TestUtils;
using Microwave.UnitTest.UnitTests.Authentication.TestUtils;
using Xunit;

namespace Microwave.UnitTest.UnitTests.Authentication.Controllers;

public class AuthenticationControllerTest : IUnitTestBase<AuthenticationController, AuthenticationControllerMock>
{
    [Fact]
    public async Task Register_WhenRegisterIsSuccessful_ReturnsOk()
    {
        //Arrange
        var mocks = GetMocks();
        var controller = GetClass(mocks);

        var registerInput = CreateAuthenticationUtils.CreateRegisterRequestInput();

        //Act
        var result = await controller.Register(registerInput) as OkResult;

        //Assert
        result.Should().NotBeNull();
        Assert.Equal(200, result.StatusCode);
        await mocks.AuthenticationService
            .Received(1)
            .Register(registerInput);
    }

    [Fact]
    public async Task Register_WhenEmailExists_ReturnsConflict()
    {
        //Arrange
        var mocks = GetMocks();
        var controller = GetClass(mocks);

        var registerInput = CreateAuthenticationUtils.CreateRegisterRequestInput();

        mocks.AuthenticationService
            .Register(registerInput)
            .ReturnsForAnyArgs(Errors.User.DuplicateEmail);

        //Act
        var result = await controller.Register(registerInput) as ObjectResult;

        //Assert
        result.Should().NotBeNull();
        Assert.Equal(409, result.StatusCode);
        await mocks.AuthenticationService
            .Received(1)
            .Register(registerInput);
    }

    [Fact]
    public async Task Login_WhenPasswordMatch_ReturnsToken()
    {
        //Arrange
        var mocks = GetMocks();
        var controller = GetClass(mocks);

        var loginRequest = CreateAuthenticationUtils.CreateLoginRequestInput();
        var loginResponseOutput = CreateAuthenticationUtils.CreateLoginResponseOutput();

        mocks.AuthenticationService
            .Login(loginRequest)
            .Returns(loginResponseOutput);

        //Act
        var result = await controller.Login(loginRequest) as OkObjectResult;

        //Assert
        result.Should().NotBeNull();
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(loginResponseOutput, result.Value);
        await mocks.AuthenticationService
            .Received(1)
            .Login(loginRequest);
    }

    [Fact]
    public async Task Login_WhenEmailNotFound_ReturnsNotFound()
    {
        //Arrange
        var mocks = GetMocks();
        var controller = GetClass(mocks);

        var loginRequest = CreateAuthenticationUtils.CreateLoginRequestInput();

        mocks.AuthenticationService
            .Login(loginRequest)
            .Returns(Errors.Common.NotFound);

        //Act
        var result = await controller.Login(loginRequest) as ObjectResult;

        //Assert
        result.Should().NotBeNull();
        Assert.Equal(404, result.StatusCode);
        await mocks.AuthenticationService
            .Received(1)
            .Login(loginRequest);
    }

    [Fact]
    public async Task Login_WhenPasswordDoesntMatch_ReturnsBadRequest()
    {
        //Arrange
        var mocks = GetMocks();
        var controller = GetClass(mocks);

        var loginRequest = CreateAuthenticationUtils.CreateLoginRequestInput();

        mocks.AuthenticationService
            .Login(loginRequest)
            .Returns(Errors.Authentication.WrongEmailOrPassword);

        //Act
        var result = await controller.Login(loginRequest) as ObjectResult;

        //Assert
        result.Should().NotBeNull();
        Assert.Equal(400, result.StatusCode);
        await mocks.AuthenticationService
            .Received(1)
            .Login(loginRequest);
    }

    public AuthenticationControllerMock GetMocks()
    {
        return new AuthenticationControllerMock
        {
            AuthenticationService = Substitute.For<IAuthenticationService>()
        };
    }

    public AuthenticationController GetClass(AuthenticationControllerMock mocks)
    {
        return new AuthenticationController(mocks.AuthenticationService);
    }
}

public class AuthenticationControllerMock
{
    public IAuthenticationService AuthenticationService { get; init; }
}