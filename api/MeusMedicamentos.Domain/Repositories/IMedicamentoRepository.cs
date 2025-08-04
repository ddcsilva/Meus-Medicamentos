using MeusMedicamentos.Domain.Entities;
using MeusMedicamentos.Domain.Entities.Ids;
using MeusMedicamentos.Domain.Enums;

namespace MeusMedicamentos.Domain.Repositories;

/// <summary>
/// Repository específico para Medicamentos com consultas de domínio.
/// </summary>
public interface IMedicamentoRepository : IRepository<Medicamento, MedicamentoId>
{
    /// <summary>
    /// Busca medicamentos por nome (busca parcial)
    /// </summary>
    Task<IEnumerable<Medicamento>> BuscarPorNomeAsync(string nome, CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca medicamento por código de barras
    /// </summary>
    Task<Medicamento?> BuscarPorCodigoBarrasAsync(string codigoBarras, CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca medicamentos por princípio ativo
    /// </summary>
    Task<IEnumerable<Medicamento>> BuscarPorPrincipioAtivoAsync(string principioAtivo, CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca medicamentos por categoria
    /// </summary>
    Task<IEnumerable<Medicamento>> BuscarPorCategoriaAsync(CategoriaId categoriaId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca medicamentos que vencem em X dias
    /// </summary>
    Task<IEnumerable<Medicamento>> BuscarVencendoEmAsync(int dias, CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca medicamentos já vencidos
    /// </summary>
    Task<IEnumerable<Medicamento>> BuscarVencidosAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca medicamentos com estoque baixo
    /// </summary>
    Task<IEnumerable<Medicamento>> BuscarComEstoqueBaixoAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca medicamentos por local de armazenamento
    /// </summary>
    Task<IEnumerable<Medicamento>> BuscarPorLocalAsync(string local, CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca medicamentos com filtros combinados
    /// </summary>
    Task<IEnumerable<Medicamento>> BuscarComFiltrosAsync(
        string? nome = null,
        CategoriaId? categoriaId = null,
        string? local = null,
        bool? apenasAtivos = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Conta total de medicamentos
    /// </summary>
    Task<int> ContarAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Conta medicamentos por status
    /// </summary>
    Task<Dictionary<StatusVencimento, int>> ContarPorStatusVencimentoAsync(CancellationToken cancellationToken = default);
}