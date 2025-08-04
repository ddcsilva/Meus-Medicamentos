namespace MeusMedicamentos.Application.DTOs.Common;

/// <summary>
/// DTO genérico para resultados paginados
/// </summary>
/// <typeparam name="T">Tipo dos itens</typeparam>
public record PaginatedResultDto<T>
{
    public IEnumerable<T> Items { get; init; } = Enumerable.Empty<T>();
    public int TotalItens { get; init; }
    public int Pagina { get; init; }
    public int ItensPorPagina { get; init; }
    public int TotalPaginas => (int)Math.Ceiling((double)TotalItens / ItensPorPagina);
    public bool TemProximaPagina => Pagina < TotalPaginas;
    public bool TemPaginaAnterior => Pagina > 1;
}
