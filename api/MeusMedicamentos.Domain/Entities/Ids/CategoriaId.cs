namespace MeusMedicamentos.Domain.Entities.Ids;

/// <summary>
/// Strongly Typed ID para Categoria
/// </summary>
public record CategoriaId(int Valor)
{
    public static implicit operator int(CategoriaId id) => id.Valor;
    public static implicit operator CategoriaId(int valor) => new(valor);

    public override string ToString() => Valor.ToString();
}