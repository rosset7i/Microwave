using ErrorOr;
using Microwave.Application.Microwaves.Dtos;

namespace Microwave.Application.Microwaves.Services;

public interface IMicrowaveService
{
    Task<List<MicrowaveOutput>> GetAllMicrowaveTemplates();
    Task<ErrorOr<MicrowaveOutput>> GetMicrowave(Guid idMicrowave);
    Task CreateMicrowave(MicrowaveOutput microwave);
    Task<ErrorOr<ValueTask>> UpdateMicrowave(Guid idMicrowave, MicrowaveOutput microwaveUpdated);
    Task<ErrorOr<ValueTask>> DeleteMicrowave(Guid idMicrowave);
}