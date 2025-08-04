using System.ComponentModel.DataAnnotations.Schema;

namespace MeusMedicamentos.Domain.Common;

/// <summary>
/// Classe base para todas as entidades do domínio
/// Uma Entity em DDD é um objeto que tem identidade única e ciclo de vida próprio
/// </summary>
/// <typeparam name="TId">Tipo do identificador da entidade</typeparam>
public abstract class Entity<TId> : IEquatable<Entity<TId>> where TId : notnull
{
    private readonly List<IDomainEvent> _domainEvents = [];

    /// <summary>
    /// Identificador único da entidade
    /// </summary>
    public TId Id { get; protected set; } = default!;

    /// <summary>
    /// Data de criação da entidade
    /// </summary>
    public DateTime CriadoEm { get; private set; } = DateTime.UtcNow;

    /// <summary>
    /// Data da última atualização
    /// </summary>
    public DateTime? AtualizadoEm { get; private set; }

    /// <summary>
    /// Eventos de domínio que serão disparados quando a entidade for persistida
    /// </summary>
    [NotMapped]
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Adiciona um evento de domínio à entidade
    /// </summary>
    protected void AdicionarEvento(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    /// <summary>
    /// Remove um evento de domínio específico
    /// </summary>
    public void RemoverEvento(IDomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    /// <summary>
    /// Limpa todos os eventos de domínio
    /// </summary>
    public void LimparEventos()
    {
        _domainEvents.Clear();
    }

    /// <summary>
    /// Marca a entidade como atualizada
    /// </summary>
    protected void MarcarComoAtualizado()
    {
        AtualizadoEm = DateTime.UtcNow;
    }

    /// <summary>
    /// Igualdade baseada no Id (conceito fundamental de Entity em DDD)
    /// </summary>
    public bool Equals(Entity<TId>? other)
    {
        return other is not null && Id.Equals(other.Id);
    }

    /// <summary>
    /// Igualdade baseada no Id (conceito fundamental de Entity em DDD)
    /// </summary>
    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && Equals(entity);
    }

    /// <summary>
    /// Hash code baseado no Id (conceito fundamental de Entity em DDD)
    /// </summary>
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    /// <summary>
    /// Operador de igualdade
    /// </summary>
    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Operador de desigualdade
    /// </summary>
    public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
    {
        return !Equals(left, right);
    }
}   