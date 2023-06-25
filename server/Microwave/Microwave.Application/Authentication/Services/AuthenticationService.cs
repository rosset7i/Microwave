using System.Security.Cryptography;
using System.Text;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Microwave.Application.Authentication.Dtos;
using Microwave.Domain.Common.Errors;
using Microwave.Domain.Entities;
using Microwave.Infra.AppDbContext;

namespace Microwave.Application.Authentication.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly MicrowaveDbContext _context;

    private readonly IJwtTokenGeneratorService _tokenGeneratorService;

    public AuthenticationService(MicrowaveDbContext context, IJwtTokenGeneratorService tokenGeneratorService)
    {
        _context = context;
        _tokenGeneratorService = tokenGeneratorService;
    }

    public async Task<ErrorOr<ValueTask>> Register(RegisterRequestInput registerRequestInput)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == registerRequestInput.Email);

        if (user is not null)
            return Errors.User.DuplicateEmail;

        using var hmac = new HMACSHA1();

        var newUser = new User(
            Guid.NewGuid(),
            registerRequestInput.FirstName,
            registerRequestInput.LastName,
            registerRequestInput.Email,
            hmac.ComputeHash(Encoding.UTF8.GetBytes(registerRequestInput.Password)),
            hmac.Key);

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();

        return ValueTask.CompletedTask;
    }

    public async Task<ErrorOr<AuthenticationResponseOutput>> Login(LoginRequestInput loginRequestInput)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == loginRequestInput.Email);

        if (user is null)
            return Errors.Authentication.WrongEmailOrPassword;

        if (!Verify(loginRequestInput, user))
            return Errors.Authentication.WrongEmailOrPassword;

        var token = _tokenGeneratorService.GenerateToken(user);

        var authResponse = new AuthenticationResponseOutput(
            user.Email,
            token);

        return authResponse;
    }

    private bool Verify(LoginRequestInput loginRequestInput, User user)
    {
        using var hmac = new HMACSHA1(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginRequestInput.Password));

        for (var i = 0; i < computedHash.Length; i++)
        {
            if (user.PasswordHash[i] != computedHash[i])
            {
                {
                    return false;
                }
            }
        }

        return true;
    }
}