namespace MeusMedicamentos.Domain.Entities.Ids;

/// <summary>
/// Strongly Typed ID para Medicamento.
/// Evita erros de passar IDs errados entre métodos.
/// </summary>
public record MedicamentoId(int Valor)
{
    public static implicit operator int(MedicamentoId id) => id.Valor;
    public static implicit operator MedicamentoId(int valor) => new(valor);

    public override string ToString() => Valor.ToString();
}