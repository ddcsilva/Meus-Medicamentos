using MediatR;

namespace MeusMedicamentos.Application.Common;

/// <summary>
/// Interface base para Queries (operações que apenas leem dados).
/// Queries representam perguntas: "Quais medicamentos estão vencendo?"
/// </summary>
/// <typeparam name="TResponse">Tipo da resposta</typeparam>
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}