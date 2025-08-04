namespace MeusMedicamentos.Application.DTOs.CodigoBarras;

/// <summary>
/// DTO para busca por código de barras
/// </summary>
public record BuscarPorCodigoBarrasDto
{
    public string CodigoBarras { get; init; } = string.Empty;
}