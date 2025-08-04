namespace MeusMedicamentos.Domain.Services.Models;

/// <summary>
/// DTO com métricas do sistema
/// </summary>
public record MetricasSistema(
    int TotalMedicamentos,
    int MedicamentosAtivos,
    int MedicamentosVencidos,
    int MedicamentosVencendoEm30Dias,
    int MedicamentosComEstoqueBaixo,
    int TotalCategorias,
    decimal ValorEstimadoEstoque // Futuro: quando tiver preços
);