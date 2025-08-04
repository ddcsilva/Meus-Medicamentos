namespace MeusMedicamentos.Application.DTOs.Medicamentos;

/// <summary>
/// DTO resumido do medicamento para listagens e seleções.
/// Contém apenas informações essenciais para performance.
/// </summary>
public record MedicamentoResumoDto
{
    public int Id { get; init; }
    public string Nome { get; init; } = string.Empty;
    public string PrincipioAtivo { get; init; } = string.Empty;
    public string Dosagem { get; init; } = string.Empty;
    public DateTime DataValidade { get; init; }
    public int QuantidadeAtual { get; init; }
    public string LocalArmazenamento { get; init; } = string.Empty;
    public string? CategoriaNome { get; init; }
    public string? CategoriaCor { get; init; }

    // Status visuais para cards/listas
    public bool EstaVencido => DataValidade < DateTime.Today;
    public bool VenceEm30Dias => DataValidade <= DateTime.Today.AddDays(30) && !EstaVencido;
    public bool EstoqueEstaAlto => QuantidadeAtual > 10; // Valor padrão, pode ser configurável
}