using MeusMedicamentos.Domain.Entities.Ids;

namespace MeusMedicamentos.Domain.Services.Models;

/// <summary>
/// DTO para sugestão de compra
/// </summary>
public record SugestaoCompra(
    MedicamentoId MedicamentoId,
    string NomeMedicamento,
    int QuantidadeAtual,
    int QuantidadeMinima,
    int QuantidadeSugerida,
    string Motivo // "Estoque baixo", "Vencendo em breve", "Consumo alto"
);