namespace MeusMedicamentos.Application.DTOs.CodigoBarras;

/// <summary>
/// DTO com dados externos obtidos por código de barras (APIs externas)
/// </summary>
public record DadosExternosCodigoBarrasDto
{
    public string? Nome { get; init; }
    public string? PrincipioAtivo { get; init; }
    public string? Fabricante { get; init; }
    public string? Descricao { get; init; }
}