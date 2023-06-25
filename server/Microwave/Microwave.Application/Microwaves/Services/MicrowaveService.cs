using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Microwave.Application.Microwaves.Dtos;
using Microwave.Domain.Common.Errors;
using Microwave.Domain.Entities;
using Microwave.Infra.AppDbContext;

namespace Microwave.Application.Microwaves.Services;

public class MicrowaveService : IMicrowaveService
{
    private readonly MicrowaveDbContext _context;

    public MicrowaveService(MicrowaveDbContext context)
    {
        _context = context;
    }

    public async Task<List<MicrowaveOutput>> GetAllMicrowaveTemplates()
    {
        var result = await _context.Microwave
            .Select(microwave => MicrowaveOutput.From(microwave))
            .ToListAsync();

        return result;
    }
    
    public async Task<ErrorOr<MicrowaveOutput>> GetMicrowave(Guid idMicrowave)
    {
        var microwave = await _context.Microwave.FindAsync(idMicrowave);

        if (microwave is null)
            return Errors.Common.NotFound;

        return MicrowaveOutput.From(microwave);
    }
    
    public async Task CreateMicrowave(MicrowaveOutput microwave)
    {
        var newMicrowave = new MicrowaveConfiguration(
            Guid.NewGuid(),
            microwave.Name,
            microwave.Food,
            TimeSpan.FromSeconds(microwave.Time),
            microwave.Potency,
            microwave.Instructions,
            microwave.WarmingChar,
            false);

        await _context.Microwave.AddAsync(newMicrowave);
        await _context.SaveChangesAsync();
    }
    
    public async Task<ErrorOr<ValueTask>> UpdateMicrowave(Guid idMicrowave, MicrowaveOutput microwaveUpdated)
    {
        var microwaveConfiguration = await _context.Microwave.FindAsync(idMicrowave);

        if (microwaveConfiguration is null)
            return Errors.Common.NotFound;

        microwaveConfiguration.Name = microwaveUpdated.Name;
        microwaveConfiguration.Instructions = microwaveUpdated.Instructions;
        microwaveConfiguration.Potency = microwaveUpdated.Potency;
        microwaveConfiguration.Time = TimeSpan.FromSeconds(microwaveUpdated.Time);
        microwaveConfiguration.WarmingChar = microwaveUpdated.WarmingChar;
        microwaveConfiguration.Food = microwaveUpdated.Food;

        _context.Microwave.Update(microwaveConfiguration);
        await _context.SaveChangesAsync();

        return ValueTask.CompletedTask;
    }
    
    public async Task<ErrorOr<ValueTask>> DeleteMicrowave(Guid idMicrowave)
    {
        var microwave = await _context.Microwave.FindAsync(idMicrowave);

        if (microwave is null)
            return Errors.Common.NotFound;

        _context.Microwave.Remove(microwave);
        await _context.SaveChangesAsync();

        return ValueTask.CompletedTask;
    }
}