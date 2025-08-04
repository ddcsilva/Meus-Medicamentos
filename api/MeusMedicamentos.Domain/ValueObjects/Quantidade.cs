using MeusMedicamentos.Domain.Common;

namespace MeusMedicamentos.Domain.ValueObjects;

/// <summary>
/// Value Object que representa uma quantidade com validações específicas
/// </summary>
public class Quantidade : ValueObject
{
    public int Valor { get; }

    private Quantidade(int valor)
    {
        Valor = valor;
    }

    /// <summary>
    /// Cria uma quantidade com validações
    /// </summary>
    public static Quantidade Criar(int valor)
    {
        if (valor < 0)
            throw new DomainException("Quantidade não pode ser negativa");

        // Regra de negócio: limite máximo razoável
        const int limiteMaximo = 10000;
        if (valor > limiteMaximo)
            throw new DomainException($"Quantidade muito alta. Máximo permitido: {limiteMaximo}");

        return new Quantidade(valor);
    }

    /// <summary>
    /// Cria quantidade zero (útil para inicializações)
    /// </summary>
    public static Quantidade Zero => new(0);

    /// <summary>
    /// Verifica se a quantidade está esgotada
    /// </summary>
    public bool EstaEsgotada => Valor == 0;

    /// <summary>
    /// Verifica se está abaixo do mínimo especificado
    /// </summary>
    public bool EstaBaixoDe(Quantidade quantidadeMinima)
    {
        return Valor <= quantidadeMinima.Valor;
    }

    /// <summary>
    /// Adiciona uma quantidade
    /// </summary>
    public Quantidade Adicionar(int valorAdicional)
    {
        return Criar(Valor + valorAdicional);
    }

    /// <summary>
    /// Subtrai uma quantidade
    /// </summary>
    public Quantidade Subtrair(int valorSubtraido)
    {
        return Criar(Valor - valorSubtraido);
    }

    /// <summary>
    /// Operadores para facilitar uso
    /// </summary>
    public static Quantidade operator +(Quantidade quantidade, int valor)
    {
        return quantidade.Adicionar(valor);
    }

    public static Quantidade operator -(Quantidade quantidade, int valor)
    {
        return quantidade.Subtrair(valor);
    }

    public static bool operator <=(Quantidade esquerda, Quantidade direita)
    {
        return esquerda.Valor <= direita.Valor;
    }

    public static bool operator >=(Quantidade esquerda, Quantidade direita)
    {
        return esquerda.Valor >= direita.Valor;
    }

    public static bool operator <(Quantidade esquerda, Quantidade direita)
    {
        return esquerda.Valor < direita.Valor;
    }

    public static bool operator >(Quantidade esquerda, Quantidade direita)
    {
        return esquerda.Valor > direita.Valor;
    }

    public override string ToString()
    {
        return Valor.ToString();
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Valor;
    }
}