using MeusMedicamentos.Application.DTOs.Medicamentos;

namespace MeusMedicamentos.Application.DTOs.Dashboard;

/// <summary>
/// DTO com métricas para dashboard
/// </summary>
public record DashboardDto
{
    public EstatisticasGerais Geral { get; init; } = new();
    public EstatisticasVencimento Vencimento { get; init; } = new();
    public EstatisticasEstoque Estoque { get; init; } = new();
    public IEnumerable<CategoriaEstatistica> Categorias { get; init; } = Enumerable.Empty<CategoriaEstatistica>();
    public IEnumerable<MedicamentoResumoDto> MedicamentosAlerta { get; init; } = Enumerable.Empty<MedicamentoResumoDto>();
}

public record EstatisticasGerais
{
    public int TotalMedicamentos { get; init; }
    public int MedicamentosAtivos { get; init; }
    public int TotalCategorias { get; init; }
    public DateTime UltimaAtualizacao { get; init; } = DateTime.UtcNow;
}

public record EstatisticasVencimento
{
    public int VencidosHoje { get; init; }
    public int VencendoEm7Dias { get; init; }
    public int VencendoEm30Dias { get; init; }
    public int VencendoEm90Dias { get; init; }
}

public record EstatisticasEstoque
{
    public int ComEstoqueNormal { get; init; }
    public int ComEstoqueBaixo { get; init; }
    public int Esgotados { get; init; }
}

public record CategoriaEstatistica
{
    public int Id { get; init; }
    public string Nome { get; init; } = string.Empty;
    public string? Cor { get; init; }
    public int TotalMedicamentos { get; init; }
    public int MedicamentosVencendoEm30Dias { get; init; }
    public int MedicamentosComEstoqueBaixo { get; init; }
}