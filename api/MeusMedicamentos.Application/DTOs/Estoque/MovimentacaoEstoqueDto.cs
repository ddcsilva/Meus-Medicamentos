namespace MeusMedicamentos.Application.DTOs.Estoque;

/// <summary>
/// DTO para movimentação de estoque (entrada/saída)
/// </summary>
public record MovimentacaoEstoqueDto
{
    public int MedicamentoId { get; init; }
    public int Quantidade { get; init; } // Positivo = entrada, Negativo = saída
    public string Motivo { get; init; } = string.Empty;
    public string? Observacoes { get; init; }
}