using MeusMedicamentos.Application.DTOs.Medicamentos;

namespace MeusMedicamentos.Application.DTOs.CodigoBarras;

/// <summary>
/// DTO de resposta da busca por código de barras
/// </summary>
public record ResultadoBuscaCodigoBarrasDto
{
    public bool Encontrado { get; init; }
    public MedicamentoDto? Medicamento { get; init; }
    public DadosExternosCodigoBarrasDto? DadosExternos { get; init; }
}