using MeusMedicamentos.Domain.Entities;
using MeusMedicamentos.Domain.Entities.Ids;

namespace MeusMedicamentos.Domain.Repositories;

/// <summary>
/// Repository para Categorias
/// </summary>
public interface ICategoriaRepository : IRepository<Categoria, CategoriaId>
{
    /// <summary>
    /// Busca categoria por nome
    /// </summary>
    Task<Categoria?> BuscarPorNomeAsync(string nome, CancellationToken cancellationToken = default);

    /// <summary>
    /// Lista apenas categorias ativas
    /// </summary>
    Task<IEnumerable<Categoria>> ListarAtivasAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica se categoria tem medicamentos associados
    /// </summary>
    Task<bool> TemMedicamentosAsync(CategoriaId categoriaId, CancellationToken cancellationToken = default);
}