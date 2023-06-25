using ErrorOr;

namespace Microwave.Domain.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error WrongEmailOrPassword => Error.Validation(code: "Authentication.EmailOrPasswordNotFound",
            description: "Wrong Email or Password.");
    }
}