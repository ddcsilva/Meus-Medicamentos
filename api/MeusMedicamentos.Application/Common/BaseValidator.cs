using FluentValidation;
using System.Linq.Expressions;

namespace MeusMedicamentos.Application.Common;

/// <summary>
/// Classe base para validators com regras comuns.
/// Centraliza validações que são usadas em múltiplos lugares.
/// </summary>
/// <typeparam name="T">Tipo sendo validado</typeparam>
public abstract class BaseValidator<T> : AbstractValidator<T>
{
    /// <summary>
    /// Regra para validar nomes (não vazio, tamanho máximo)
    /// </summary>
    protected void ValidarNome(Expression<Func<T, string>> expression, int tamanhoMaximo = 100)
    {
        RuleFor(expression)
            .NotEmpty()
            .WithMessage("Nome é obrigatório")
            .MaximumLength(tamanhoMaximo)
            .WithMessage($"Nome deve ter no máximo {tamanhoMaximo} caracteres");
    }

    /// <summary>
    /// Regra para validar IDs (maior que zero)
    /// </summary>
    protected void ValidarId<TProperty>(Expression<Func<T, TProperty>> expression, string nomeEntidade = "ID")
    {
        RuleFor(expression)
            .NotEmpty()
            .WithMessage($"{nomeEntidade} é obrigatório")
            .Must(id => Convert.ToInt32(id) > 0)
            .WithMessage($"{nomeEntidade} deve ser maior que zero");
    }

    /// <summary>
    /// Regra para validar quantidades inteiras (não negativas)
    /// </summary>
    protected void ValidarQuantidade(Expression<Func<T, int>> expression)
    {
        RuleFor(expression)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Quantidade não pode ser negativa");
    }

    /// <summary>
    /// Regra para validar quantidades decimais (não negativas)
    /// </summary>
    protected void ValidarQuantidade(Expression<Func<T, decimal>> expression)
    {
        RuleFor(expression)
            .GreaterThanOrEqualTo(0m)
            .WithMessage("Quantidade não pode ser negativa");
    }

    /// <summary>
    /// Regra para validar datas (não muito antigas nem muito futuras)
    /// </summary>
    protected void ValidarDataValidade<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        RuleFor(expression)
            .NotEmpty()
            .WithMessage("Data de validade é obrigatória")
            .Must(data =>
            {
                var dataConvertida = Convert.ToDateTime(data);
                return dataConvertida >= DateTime.Today.AddYears(-2) &&
                       dataConvertida <= DateTime.Today.AddYears(10);
            })
            .WithMessage("Data de validade deve estar entre 2 anos atrás e 10 anos no futuro");
    }
}