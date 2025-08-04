using MeusMedicamentos.Domain.Entities.Ids;
using MeusMedicamentos.Domain.Interfaces;

namespace MeusMedicamentos.Domain.Events;

/// <summary>
/// Domain Event disparado quando um medicamento muda de local.
/// Útil para: rastreabilidade, organização, relatórios de movimentação.
/// </summary>
public record LocalArmazenamentoAlteradoEvent(
    MedicamentoId MedicamentoId,
    string NomeMedicamento,
    string LocalAnterior,
    string NovoLocal
) : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OcorridoEm { get; } = DateTime.UtcNow;
}