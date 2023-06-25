using System.ComponentModel.DataAnnotations;

namespace Microwave.Domain.Entities;

public class MicrowaveConfiguration
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Food { get; set; }
    public TimeSpan Time { get; set; }
    
    [Range(1,10, ErrorMessage = "Invalid Potency Setting!")]
    public int Potency { get; set; } = 10;
    public string? Instructions { get; set; }
    public char WarmingChar { get; set; }
    public bool IsStatic { get; set; }
    
    public MicrowaveConfiguration(
        Guid id, 
        string name, 
        string food, 
        TimeSpan time, 
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
}