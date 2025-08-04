using System.Linq.Expressions;
using MeusMedicamentos.Domain.Entities;

namespace MeusMedicamentos.Domain.Specifications;

/// <summary>
/// Specifications para consultas complexas de medicamentos.
/// Pattern útil para encapsular lógicas de consulta reutilizáveis.
/// </summary>
public static class MedicamentoSpecifications
{
    /// <summary>
    /// Medicamentos ativos
    /// </summary>
    public static Expression<Func<Medicamento, bool>> Ativos => medicamento => medicamento.Ativo;

    /// <summary>
    /// Medicamentos que vencem em X dias
    /// </summary>
    public static Expression<Func<Medicamento, bool>> VenceEm(int dias)
    {
        var dataLimite = DateTime.Today.AddDays(dias);
        return medicamento => medicamento.DataValidade.Valor <= dataLimite &&
                              medicamento.DataValidade.Valor >= DateTime.Today;
    }

    /// <summary>
    /// Medicamentos com estoque baixo
    /// </summary>
    public static Expression<Func<Medicamento, bool>> ComEstoqueBaixo => medicamento => medicamento.QuantidadeAtual.Valor <= medicamento.QuantidadeMinima.Valor;

    /// <summary>
    /// Medicamentos por local
    /// </summary>
    public static Expression<Func<Medicamento, bool>> PorLocal(string local)
    {
        return medicamento => medicamento.Local.Descricao.Contains(local);
    }

    /// <summary>
    /// Medicamentos por nome (busca parcial)
    /// </summary>
    public static Expression<Func<Medicamento, bool>> PorNome(string nome)
    {
        return medicamento => medicamento.Nome.Contains(nome) || medicamento.PrincipioAtivo.Contains(nome);
    }
}