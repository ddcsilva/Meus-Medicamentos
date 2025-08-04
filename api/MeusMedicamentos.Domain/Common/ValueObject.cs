namespace MeusMedicamentos.Domain.Common;

/// <summary>
/// Classe base para Value Objects em DDD.
/// Value Objects são objetos imutáveis que são identificados apenas por seus valores,
/// não por identidade (ex: endereço, dinheiro, coordenadas).
/// </summary>
public abstract class ValueObject : IEquatable<ValueObject>
{
    /// <summary>
    /// Retorna os componentes que definem a igualdade do Value Object
    /// </summary>
    protected abstract IEnumerable<object?> ObterComponentesDeIgualdade();

    /// <summary>
    /// Verifica se o Value Object é igual a outro
    /// </summary>
    public bool Equals(ValueObject? other)
    {
        return other is not null &&
               GetType() == other.GetType() &&
               ObterComponentesDeIgualdade().SequenceEqual(other.ObterComponentesDeIgualdade());
    }

    /// <summary>
    /// Verifica se o Value Object é igual a outro
    /// </summary>
    public override bool Equals(object? obj)
    {
        return obj is ValueObject valueObject && Equals(valueObject);
    }

    /// <summary>
    /// Retorna o hash code do Value Object
    /// </summary>
    public override int GetHashCode()
    {
        return ObterComponentesDeIgualdade()
            .Where(x => x != null)
            .Aggregate(1, (current, obj) => current * 23 + obj!.GetHashCode());
    }

    /// <summary>
    /// Operador de igualdade
    /// </summary>
    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Operador de desigualdade
    /// </summary>
    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !Equals(left, right);
    }
}