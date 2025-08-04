using MeusMedicamentos.Domain.Entities.Ids;
using MeusMedicamentos.Domain.Interfaces;

namespace MeusMedicamentos.Domain.Events;

/// <summary>
/// Domain Event disparado quando um novo medicamento é cadastrado.
/// Útil para: logs, notificações, cálculos de estatísticas, etc.
/// </summary>
public record MedicamentoCadastradoEvent(
    MedicamentoId MedicamentoId,
    string NomeMedicamento
) : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OcorridoEm { get; } = DateTime.UtcNow;
}