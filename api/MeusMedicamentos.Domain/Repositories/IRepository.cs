namespace MeusMedicamentos.Domain.Repositories;

/// <summary>
/// Interface base para repositórios genéricos.
/// Define operações básicas de CRUD.
/// </summary>
/// <typeparam name="TEntity">Tipo da entidade</typeparam>
/// <typeparam name="TId">Tipo do identificador</typeparam>
public interface IRepository<TEntity, TId>
    where TEntity : class
    where TId : notnull
{
    /// <summary>
    /// Busca entidade por ID
    /// </summary>
    Task<TEntity?> BuscarPorIdAsync(TId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Lista todas as entidades ativas
    /// </summary>
    Task<IEnumerable<TEntity>> ListarAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Adiciona nova entidade
    /// </summary>
    Task AdicionarAsync(TEntity entidade, CancellationToken cancellationToken = default);

    /// <summary>
    /// Atualiza entidade existente
    /// </summary>
    void Atualizar(TEntity entidade);

    /// <summary>
    /// Remove entidade (pode ser soft delete)
    /// </summary>
    void Remover(TEntity entidade);

    /// <summary>
    /// Verifica se entidade existe
    /// </summary>
    Task<bool> ExisteAsync(TId id, CancellationToken cancellationToken = default);
}