using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace MeusMedicamentos.Application.Common;

/// <summary>
/// Pipeline Behaviour para logging automático de requests.
/// Registra tempo de execução e dados da request para auditoria.
/// </summary>
/// <typeparam name="TRequest">Tipo da request</typeparam>
/// <typeparam name="TResponse">Tipo da response</typeparam>
public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var stopwatch = Stopwatch.StartNew();

        _logger.LogInformation("Iniciando request {RequestName}", requestName);

        try
        {
            var response = await next();

            stopwatch.Stop();

            _logger.LogInformation(
                "Request {RequestName} concluída em {ElapsedMilliseconds}ms",
                requestName,
                stopwatch.ElapsedMilliseconds);

            return response;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();

            _logger.LogError(ex,
                "Erro na request {RequestName} após {ElapsedMilliseconds}ms",
                requestName,
                stopwatch.ElapsedMilliseconds);

            throw;
        }
    }
}