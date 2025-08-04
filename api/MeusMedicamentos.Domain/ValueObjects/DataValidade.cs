using MeusMedicamentos.Domain.Common;
using MeusMedicamentos.Domain.Enums;

namespace MeusMedicamentos.Domain.ValueObjects;

/// <summary>
/// Value Object que representa uma data de validade com regras de negócio específicas
/// </summary>
public class DataValidade : ValueObject
{
    public DateTime Valor { get; }

    private DataValidade(DateTime valor)
    {
        Valor = valor.Date; // Garantir que é apenas a data, sem horário
    }

    /// <summary>
    /// Cria uma data de validade com validações de negócio
    /// </summary>
    public static DataValidade Criar(DateTime dataValidade)
    {
        // Regra de negócio: não aceitar datas muito antigas (medicamentos já vencidos há muito tempo)
        var limiteMinimoAnos = 2;
        var dataLimite = DateTime.Today.AddYears(-limiteMinimoAnos);

        if (dataValidade < dataLimite)
            throw new DomainException($"Data de validade muito antiga. Mínimo: {dataLimite:dd/MM/yyyy}");

        // Regra de negócio: não aceitar datas muito futuras (mais de 10 anos)
        var limiteMaximoAnos = 10;
        var dataMaxima = DateTime.Today.AddYears(limiteMaximoAnos);

        if (dataValidade > dataMaxima)
            throw new DomainException($"Data de validade muito futura. Máximo: {dataMaxima:dd/MM/yyyy}");

        return new DataValidade(dataValidade);
    }

    /// <summary>
    /// Verifica se o medicamento está vencido
    /// </summary>
    public bool EstaVencido()
    {
        return Valor < DateTime.Today;
    }

    /// <summary>
    /// Verifica se o medicamento vence dentro de X dias
    /// </summary>
    public bool VenceEm(int dias)
    {
        if (dias < 0)
            throw new ArgumentException("Número de dias deve ser positivo");

        return Valor <= DateTime.Today.AddDays(dias) && !EstaVencido();
    }

    /// <summary>
    /// Calcula quantos dias restam até o vencimento
    /// </summary>
    public int DiasParaVencimento()
    {
        if (EstaVencido())
            return 0;

        return (Valor - DateTime.Today).Days;
    }

    /// <summary>
    /// Retorna o status do vencimento para exibição
    /// </summary>
    public StatusVencimento ObterStatus()
    {
        if (EstaVencido())
            return StatusVencimento.Vencido;

        if (VenceEm(7))
            return StatusVencimento.VenceEm7Dias;

        if (VenceEm(30))
            return StatusVencimento.VenceEm30Dias;

        return StatusVencimento.Normal;
    }

    public override string ToString()
    {
        return Valor.ToString("dd/MM/yyyy");
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Valor;
    }
}