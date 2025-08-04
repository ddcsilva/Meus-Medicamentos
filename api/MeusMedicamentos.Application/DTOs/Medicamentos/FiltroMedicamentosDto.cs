using MeusMedicamentos.Domain.Enums;

namespace MeusMedicamentos.Application.DTOs.Medicamentos;

/// <summary>
/// DTO para filtros de busca de medicamentos.
/// Todos os campos são opcionais para permitir combinações flexíveis.
/// </summary>
public record FiltroMedicamentosDto
{
    public string? Nome { get; init; }
    public string? PrincipioAtivo { get; init; }
    public FormaFarmaceutica? Forma { get; init; }
    public string? Fabricante { get; init; }
    public int? CategoriaId { get; init; }
    public string? LocalArmazenamento { get; init; }
    public bool? ApenasAtivos { get; init; } = true;
    public bool? ApenasVencendoEm30Dias { get; init; }
    public bool? ApenasComEstoqueBaixo { get; init; }
    public bool? ApenasVencidos { get; init; }

    // Paginação
    public int Pagina { get; init; } = 1;
    public int ItensPorPagina { get; init; } = 20;

    // Ordenação
    public string? OrdenarPor { get; init; } // "nome", "dataValidade", "quantidadeAtual"
    public bool OrdemDecrescente { get; init; } = false;
}