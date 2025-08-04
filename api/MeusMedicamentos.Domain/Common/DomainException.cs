namespace MeusMedicamentos.Domain.Common;

/// <summary>
/// Exceção específica do domínio.
/// Usada para representar violações de regras de negócio.
/// </summary>
public class DomainException : Exception
{
    /// <summary>
    /// Cria uma nova instância de DomainException com uma mensagem de erro.
    /// </summary>
    /// <param name="message">A mensagem de erro que descreve a violação de regra.</param>
    public DomainException(string message) : base(message) { }

    /// <summary>
    /// Cria uma nova instância de DomainException com uma mensagem de erro e uma exceção interna.
    /// </summary>
    /// <param name="message">A mensagem de erro que descreve a violação de regra.</param>
    /// <param name="innerException">A exceção interna que causou a violação de regra.</param>
    public DomainException(string message, Exception innerException) : base(message, innerException) { }
}