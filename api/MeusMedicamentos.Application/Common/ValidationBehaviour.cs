using FluentValidation;
using MediatR;

namespace MeusMedicamentos.Application.Common;

/// <summary>
/// Pipeline Behaviour que executa validações automaticamente antes dos handlers.
/// Intercepta todas as requests e executa os validators correspondentes.
/// </summary>
/// <typeparam name="TRequest">Tipo da request</typeparam>
/// <typeparam name="TResponse">Tipo da response</typeparam>
public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Se não há validators, continua normalmente
        if (!_validators.Any())
        {
            return await next();
        }

        // Executa todas as validações
        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        // Coleta todos os erros
        var failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        // Se há erros, retorna resultado de falha
        if (failures.Any())
        {
            var errors = failures.Select(f => f.ErrorMessage);

            // Verifica se TResponse é Result ou Result<T>
            if (typeof(TResponse).IsGenericType &&
                typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
            {
                var resultType = typeof(TResponse).GetGenericArguments()[0];
                var failureMethod = typeof(Result<>)
                    .MakeGenericType(resultType)
                    .GetMethod(nameof(Result<object>.Failure), new[] { typeof(IEnumerable<string>) });

                return (TResponse)failureMethod!.Invoke(null, new object[] { errors })!;
            }

            if (typeof(TResponse) == typeof(Result))
            {
                return (TResponse)(object)Result.Failure(errors);
            }
        }

        return await next();
    }
}