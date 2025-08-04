namespace MeusMedicamentos.Domain.Common;

/// <summary>
/// Interface para eventos de domínio.
/// Domain Events são uma forma de comunicar mudanças importantes no domínio
/// sem criar acoplamento entre agregados.
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    /// Identificador único do evento
    /// </summary>
    Guid EventId { get; }

    /// <summary>
    /// Momento em que o evento ocorreu
    /// </summary>
    DateTime OcorridoEm { get; }
}