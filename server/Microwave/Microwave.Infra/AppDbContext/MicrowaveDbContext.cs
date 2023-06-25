using Microsoft.EntityFrameworkCore;
using Microwave.Domain.Entities;

namespace Microwave.Infra.AppDbContext;

public class MicrowaveDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<MicrowaveConfiguration> Microwave { get; set; }
    public DbSet<ExceptionLog> ExceptionsLog { get; set; }

    public MicrowaveDbContext(DbContextOptions option) : base(option)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MicrowaveConfiguration>()
            .HasData(
                new MicrowaveConfiguration(
                    Guid.NewGuid(),
                    "Pipoca",
                    "Pipoca de Microondas",
                    TimeSpan.FromSeconds(180),
                    7,
                    "Observar o barulho de estouros do milho, caso houver um intervalo de mais de 10 segundos entre um estouro e outro, interrompa o aquecimento.",
                    'P',
                    true),
                new MicrowaveConfiguration(
                    Guid.NewGuid(),
                    "Leite",
                    "Leite",
                    TimeSpan.FromSeconds(300),
                    5,
                    "Cuidado com aquecimento de líquidos, o choque térmico aliado ao movimento do recipiente pode causar fervura imediata causando risco de queimaduras.",
                    'L',
                    true),
                new MicrowaveConfiguration(
                    Guid.NewGuid(),
                    "Carnes de boi",
                    "Carne em pedaço ou fatias",
                    TimeSpan.FromSeconds(840),
                    4,
                    "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme.",
                    'C',
                    true),
                new MicrowaveConfiguration(
                    Guid.NewGuid(),
                    "Frango",
                    "Frango (qualquer corte)",
                    TimeSpan.FromSeconds(480),
                    7,
                    "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme.",
                    'F',
                    true),
                new MicrowaveConfiguration(
                    Guid.NewGuid(),
                    "Feijão",
                    "Feijão congelado",
                    TimeSpan.FromSeconds(480),
                    9,
                    "Deixe o recipiente destampado e em casos de plástico, cuidado ao retirar o recipiente pois o mesmo pode perder resistência em altas temperaturas.",
                    'Q',
                    true)
            );
        
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetTableName(entity.GetTableName().ToLower());

            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(property.GetColumnName().ToLower());
            }
        }
    }
}