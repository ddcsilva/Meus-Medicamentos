using System.Text.RegularExpressions;
using MeusMedicamentos.Domain.Common;

namespace MeusMedicamentos.Domain.ValueObjects;

/// <summary>
/// Value Object que representa a dosagem de um medicamento.
/// Exemplos: "500mg", "10ml", "1 comprimido"
/// </summary>
public class Dosagem : ValueObject
{
    public string Valor { get; }
    public string Unidade { get; }

    /// <summary>
    /// Construtor privado força criação através do método estático
    /// </summary>
    private Dosagem(string valor, string unidade)
    {
        Valor = valor;
        Unidade = unidade;
    }

    /// <summary>
    /// Factory method para criar dosagem a partir de string completa
    /// Ex: "500mg" → Valor="500", Unidade="mg"
    /// </summary>
    public static Dosagem Criar(string dosagemCompleta)
    {
        if (string.IsNullOrWhiteSpace(dosagemCompleta))
            throw new DomainException("Dosagem não pode ser vazia");

        // Regex para separar número da unidade
        var regex = new Regex(@"^(\d+(?:\.\d+)?)\s*([a-zA-Z]+)$");
        var match = regex.Match(dosagemCompleta.Trim());

        if (!match.Success)
            throw new DomainException($"Formato de dosagem inválido: {dosagemCompleta}");

        var valor = match.Groups[1].Value;
        var unidade = match.Groups[2].Value.ToLowerInvariant();

        // Validação de unidades aceitas
        var unidadesValidas = new[] { "mg", "g", "ml", "l", "ui", "mcg", "comprimido", "comprimidos", "cápsula", "cápsulas" };
        if (!unidadesValidas.Contains(unidade))
            throw new DomainException($"Unidade não reconhecida: {unidade}");

        return new Dosagem(valor, unidade);
    }

    /// <summary>
    /// Factory method para criar com valor e unidade separados
    /// </summary>
    public static Dosagem Criar(decimal valor, string unidade)
    {
        return Criar($"{valor}{unidade}");
    }

    /// <summary>
    /// Retorna a dosagem formatada para exibição
    /// </summary>
    public override string ToString()
    {
        return $"{Valor}{Unidade}";
    }

    /// <summary>
    /// Componentes usados para comparação de igualdade
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Valor;
        yield return Unidade;
    }
}
