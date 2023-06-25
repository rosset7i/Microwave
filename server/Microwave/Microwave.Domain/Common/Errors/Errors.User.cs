using ErrorOr;

namespace Microwave.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail =>
            Error.Conflict(code: "User.DuplicateEmail", description: "Email Already In Use.");
    }
}