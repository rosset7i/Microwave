using ErrorOr;

namespace Microwave.Domain.Common.Errors;

public static partial class Errors
{
    public static class Common
    {
        public static Error NotFound => Error.NotFound(code: "Common.NotFound", description: "Data not found!");

        public static Error PersistEntityError => Error.Unexpected(code: "Common.PersistEntityError",
            description: "Error while saving the data!");
    }
}