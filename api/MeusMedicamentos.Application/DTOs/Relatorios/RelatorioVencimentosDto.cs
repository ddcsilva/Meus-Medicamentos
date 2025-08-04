namespace MeusMedicamentos.Application.DTOs.Relatorios;

/// <summary>
/// DTO para relatório de vencimentos
/// </summary>
public record RelatorioVencimentosDto
{
    public DateTime DataGeracao { get; init; } = DateTime.UtcNow;
    public int TotalMedicamentos { get; init; }
    public IEnumerable<MedicamentoVencimentoDto> Vencidos { get; init; } = Enumerable.Empty<MedicamentoVencimentoDto>();
    public IEnumerable<MedicamentoVencimentoDto> VencendoEm7Dias { get; init; } = Enumerable.Empty<MedicamentoVencimentoDto>();
    public IEnumerable<MedicamentoVencimentoDto> VencendoEm30Dias { get; init; } = Enumerable.Empty<MedicamentoVencimentoDto>();
}

public record MedicamentoVencimentoDto
{
    public int Id { get; init; }
    public string Nome { get; init; } = string.Empty;
    public string PrincipioAtivo { get; init; } = string.Empty;
    public DateTime DataValidade { get; init; }
    public int DiasParaVencimento { get; init; }
    public int QuantidadeAtual { get; init; }
    public string LocalArmazenamento { get; init; } = string.Empty;
    public string CategoriaNome { get; init; } = string.Empty;
}