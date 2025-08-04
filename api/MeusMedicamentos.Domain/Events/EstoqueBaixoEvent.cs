using MeusMedicamentos.Domain.Entities.Ids;
using MeusMedicamentos.Domain.Interfaces;

namespace MeusMedicamentos.Domain.Events;

/// <summary>
/// Domain Event disparado quando um medicamento fica com estoque baixo.
/// Pode ser usado para: alertas, notificações push, lista de compras automática.
/// </summary>
public record EstoqueBaixoEvent(
    MedicamentoId MedicamentoId,
    string NomeMedicamento,
    int QuantidadeAtual,
    int QuantidadeMinima
) : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OcorridoEm { get; } = DateTime.UtcNow;
}