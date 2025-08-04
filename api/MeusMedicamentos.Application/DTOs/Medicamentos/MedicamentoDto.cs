using MeusMedicamentos.Application.DTOs.Common;
using MeusMedicamentos.Domain.Enums;

namespace MeusMedicamentos.Application.DTOs.Medicamentos;

/// <summary>
/// DTO completo do medicamento para retorno de APIs.
/// Contém todas as informações necessárias para exibição.
/// </summary>
public record MedicamentoDto : BaseDto
{
    public int Id { get; init; }
    public string Nome { get; init; } = string.Empty;
    public string PrincipioAtivo { get; init; } = string.Empty;
    public string Dosagem { get; init; } = string.Empty;
    public FormaFarmaceutica Forma { get; init; }
    public string FormaDescricao => Forma.ToString();
    public string Fabricante { get; init; } = string.Empty;
    public DateTime DataValidade { get; init; }
    public int QuantidadeAtual { get; init; }
    public int QuantidadeMinima { get; init; }
    public string LocalArmazenamento { get; init; } = string.Empty;
    public string? Lote { get; init; }
    public string? CodigoBarras { get; init; }
    public string? Observacoes { get; init; }
    public bool Ativo { get; init; }

    // Categoria
    public int CategoriaId { get; init; }
    public string? CategoriaNome { get; init; }
    public string? CategoriaCor { get; init; }

    // Propriedades calculadas para facilitar uso no frontend
    public bool EstaVencido => DataValidade < DateTime.Today;
    public bool VenceEm7Dias => DataValidade <= DateTime.Today.AddDays(7) && !EstaVencido;
    public bool VenceEm30Dias => DataValidade <= DateTime.Today.AddDays(30) && !VenceEm7Dias && !EstaVencido;
    public bool EstoqueEstaAbaixoDoMinimo => QuantidadeAtual <= QuantidadeMinima;
    public int DiasParaVencimento => EstaVencido ? 0 : (DataValidade - DateTime.Today).Days;

    public string StatusVencimento
    {
        get
        {
            if (EstaVencido) return "Vencido";
            if (VenceEm7Dias) return "Vence em 7 dias";
            if (VenceEm30Dias) return "Vence em 30 dias";
            return "Normal";
        }
    }

    public string StatusEstoque
    {
        get
        {
            if (QuantidadeAtual == 0) return "Esgotado";
            if (EstoqueEstaAbaixoDoMinimo) return "Estoque baixo";
            return "Normal";
        }
    }
}