using MediatR;

namespace MeusMedicamentos.Application.Common;

/// <summary>
/// Interface para handlers de Queries
/// </summary>
/// <typeparam name="TQuery">Tipo da query</typeparam>
/// <typeparam name="TResponse">Tipo da resposta</typeparam>
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> where TQuery : IQuery<TResponse>
{
}