using MeusMedicamentos.Domain.Enums;

namespace MeusMedicamentos.Application.DTOs.Medicamentos;

/// <summary>
/// DTO para criação de novo medicamento.
/// Contém apenas campos necessários para cadastro.
/// </summary>
public record CriarMedicamentoDto
{
    public string Nome { get; init; } = string.Empty;
    public string PrincipioAtivo { get; init; } = string.Empty;
    public string Dosagem { get; init; } = string.Empty;
    public FormaFarmaceutica Forma { get; init; }
    public string Fabricante { get; init; } = string.Empty;
    public DateTime DataValidade { get; init; }
    public int QuantidadeAtual { get; init; }
    public int QuantidadeMinima { get; init; } = 5; // Valor padrão
    public string LocalArmazenamento { get; init; } = string.Empty;
    public string? Lote { get; init; }
    public string? CodigoBarras { get; init; }
    public string? Observacoes { get; init; }
    public int CategoriaId { get; init; }
}