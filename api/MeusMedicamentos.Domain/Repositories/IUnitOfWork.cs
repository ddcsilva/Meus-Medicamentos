namespace MeusMedicamentos.Domain.Repositories;

/// <summary>
/// Unit of Work pattern para controlar transações.
/// Garante que várias operações sejam salvas atomicamente.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Salva todas as mudanças e publica Domain Events
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Inicia uma transação explícita
    /// </summary>
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Confirma a transação
    /// </summary>
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancela a transação
    /// </summary>
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}