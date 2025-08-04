using MeusMedicamentos.Domain.Entities.Ids;
using MeusMedicamentos.Domain.Interfaces;

namespace MeusMedicamentos.Domain.Events;

/// <summary>
/// Domain Event disparado quando o estoque de um medicamento é atualizado.
/// Contém informações sobre a movimentação para auditoria.
/// </summary>
public record EstoqueAtualizadoEvent(
    MedicamentoId MedicamentoId,
    string NomeMedicamento,
    int QuantidadeMovimentada,
    string Motivo
) : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OcorridoEm { get; } = DateTime.UtcNow;
}