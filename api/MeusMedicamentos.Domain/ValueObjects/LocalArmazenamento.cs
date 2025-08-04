using MeusMedicamentos.Domain.Common;

namespace MeusMedicamentos.Domain.ValueObjects;

/// <summary>
/// Value Object que representa onde o medicamento está armazenado
/// </summary>
public class LocalArmazenamento : ValueObject
{
    public string Descricao { get; }

    private LocalArmazenamento(string descricao)
    {
        Descricao = descricao;
    }

    public static LocalArmazenamento Criar(string descricao)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new DomainException("Local de armazenamento não pode ser vazio");

        if (descricao.Length > 100)
            throw new DomainException("Descrição do local muito longa (máximo 100 caracteres)");

        return new LocalArmazenamento(descricao.Trim());
    }

    /// <summary>
    /// Locais pré-definidos comuns
    /// </summary>
    public static LocalArmazenamento GavetaQuarto => new("Gaveta do Quarto");
    public static LocalArmazenamento ArmarioCozinha => new("Armário da Cozinha");
    public static LocalArmazenamento Geladeira => new("Geladeira");
    public static LocalArmazenamento ArmarinhoBanheiro => new("Armário do Banheiro");

    public override string ToString()
    {
        return Descricao;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Descricao.ToUpperInvariant();
    }
}