using MediatR;

namespace MeusMedicamentos.Application.Common;

/// <summary>
/// Interface base para Commands (operações que modificam estado).
/// Commands representam intenções: "Quero cadastrar um medicamento"
/// </summary>
public interface ICommand : IRequest<Result>
{
}

/// <summary>
/// Command que retorna um resultado específico
/// </summary>
/// <typeparam name="TResponse">Tipo do resultado</typeparam>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}