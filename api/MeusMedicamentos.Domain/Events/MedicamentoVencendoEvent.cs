using MeusMedicamentos.Domain.Entities.Ids;
using MeusMedicamentos.Domain.Common;

namespace MeusMedicamentos.Domain.Events;

/// <summary>
/// Domain Event disparado quando um medicamento está próximo do vencimento.
/// Processado por background services para alertas automáticos.
/// </summary>
public record MedicamentoVencendoEvent(
    MedicamentoId MedicamentoId,
    string NomeMedicamento,
    DateTime DataValidade,
    int DiasParaVencimento
) : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OcorridoEm { get; } = DateTime.UtcNow;
}