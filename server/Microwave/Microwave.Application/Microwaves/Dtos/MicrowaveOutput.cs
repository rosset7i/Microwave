using Microwave.Domain.Entities;

namespace Microwave.Application.Microwaves.Dtos;

public class MicrowaveOutput
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Food { get; set; }
    public int Time { get; set; }
    public int Potency { get; set; } = 10;
    public string? Instructions { get; set; }
    public char WarmingChar { get; set; }
    public bool IsStatic { get; set; }
    
    public MicrowaveOutput(
        Guid id, 
        string name, 
        string food, 
        int time, 
        int potency, 
        string instructions,
        char warmingChar,
        bool isStatic)
    {
        Id = id;
        Name = name;
        Food = food;
        Time = time;
        Potency = potency;
        Instructions = instructions;
        WarmingChar = warmingChar;
        IsStatic = isStatic;
    }
    
    public static MicrowaveOutput From(MicrowaveConfiguration microwave)
    {
        return new MicrowaveOutput(
            microwave.Id,
            microwave.Name,
            microwave.Food,
            (int)microwave.Time.TotalSeconds,
            microwave.Potency,
            microwave.Instructions,
            microwave.WarmingChar,
            microwave.IsStatic);
    }
}