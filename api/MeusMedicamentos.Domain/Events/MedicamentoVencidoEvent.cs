using MeusMedicamentos.Domain.Entities.Ids;
using MeusMedicamentos.Domain.Interfaces;

namespace MeusMedicamentos.Domain.Events;

/// <summary>
/// Domain Event disparado quando um medicamento vence.
/// Pode ser usado para: alertas críticos, remoção automática do estoque ativo.
/// </summary>
public record MedicamentoVencidoEvent(
    MedicamentoId MedicamentoId,
    string NomeMedicamento,
    DateTime DataValidade,
    int QuantidadeVencida
) : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OcorridoEm { get; } = DateTime.UtcNow;
}