using MeusMedicamentos.Application.DTOs.Common;

namespace MeusMedicamentos.Application.DTOs.Categorias;

/// <summary>
/// DTO completo da categoria
/// </summary>
public record CategoriaDto : BaseDto
{
    public int Id { get; init; }
    public string Nome { get; init; } = string.Empty;
    public string? Descricao { get; init; }
    public string? Cor { get; init; }
    public bool Ativo { get; init; }

    // Estatísticas (calculadas quando necessário)
    public int? TotalMedicamentos { get; init; }
    public int? MedicamentosAtivos { get; init; }
}