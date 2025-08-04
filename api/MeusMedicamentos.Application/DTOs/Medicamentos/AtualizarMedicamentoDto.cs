using MeusMedicamentos.Domain.Enums;

namespace MeusMedicamentos.Application.DTOs.Medicamentos;

/// <summary>
/// DTO para atualização de medicamento existente.
/// Similar ao de criação, mas com ID obrigatório.
/// </summary>
public record AtualizarMedicamentoDto
{
    public int Id { get; init; }
    public string Nome { get; init; } = string.Empty;
    public string PrincipioAtivo { get; init; } = string.Empty;
    public string Dosagem { get; init; } = string.Empty;
    public FormaFarmaceutica Forma { get; init; }
    public string Fabricante { get; init; } = string.Empty;
    public DateTime DataValidade { get; init; }
    public int QuantidadeAtual { get; init; }
    public int QuantidadeMinima { get; init; }
    public string LocalArmazenamento { get; init; } = string.Empty;
    public string? Lote { get; init; }
    public string? CodigoBarras { get; init; }
    public string? Observacoes { get; init; }
    public int CategoriaId { get; init; }
}