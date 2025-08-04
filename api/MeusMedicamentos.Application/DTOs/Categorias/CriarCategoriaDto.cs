namespace MeusMedicamentos.Application.DTOs.Categorias;

/// <summary>
/// DTO para criação de categoria
/// </summary>
public record CriarCategoriaDto
{
    public string Nome { get; init; } = string.Empty;
    public string? Descricao { get; init; }
    public string? Cor { get; init; } // Formato: #RRGGBB
}