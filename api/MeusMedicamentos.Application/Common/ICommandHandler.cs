using MediatR;

namespace MeusMedicamentos.Application.Common;

/// <summary>
/// Interface para handlers de Commands
/// </summary>
/// <typeparam name="TCommand">Tipo do command</typeparam>
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result> where TCommand : ICommand
{
}

/// <summary>
/// Handler para Commands que retornam resultado
/// </summary>
/// <typeparam name="TCommand">Tipo do command</typeparam>
/// <typeparam name="TResponse">Tipo da resposta</typeparam>
public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>> where TCommand : ICommand<TResponse>
{
}