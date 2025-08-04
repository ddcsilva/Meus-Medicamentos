using MeusMedicamentos.Domain.Services.Models;

namespace MeusMedicamentos.Domain.Services;

/// <summary>
/// Domain Service para lógicas que envolvem múltiplas entidades
/// ou regras de negócio complexas que não pertencem a uma entidade específica.
/// </summary>
public interface IDomainNotificationService
{
    /// <summary>
    /// Verifica medicamentos que precisam de atenção (vencimento, estoque baixo)
    /// </summary>
    Task<NotificacoesMedicamentos> VerificarMedicamentosParaNotificacaoAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Calcula métricas do sistema para dashboard
    /// </summary>
    Task<MetricasSistema> CalcularMetricasAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gera sugestões de compra baseado em consumo histórico
    /// </summary>
    Task<IEnumerable<SugestaoCompra>> GerarSugestoesCompraAsync(
        CancellationToken cancellationToken = default);
}