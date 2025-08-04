using MeusMedicamentos.Domain.Entities.Ids;
using MeusMedicamentos.Domain.Services.Models;

namespace MeusMedicamentos.Domain.Services;

/// <summary>
/// Domain Service para validações complexas de medicamentos.
/// Usado para regras que envolvem consultas ou lógicas que não cabem na entidade.
/// </summary>
public interface IValidacaoMedicamentoService
{
    /// <summary>
    /// Verifica se já existe medicamento similar (mesmo princípio ativo + dosagem)
    /// </summary>
    Task<bool> ExisteMedicamentoSimilarAsync(
        string principioAtivo,
        string dosagem,
        MedicamentoId? ignorarMedicamentoId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Valida se o código de barras é único no sistema
    /// </summary>
    Task<bool> CodigoBarrasEUnicoAsync(
        string codigoBarras,
        MedicamentoId? ignorarMedicamentoId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica compatibilidade entre medicamentos (futuro)
    /// </summary>
    Task<ResultadoValidacao> VerificarCompatibilidadeAsync(
        MedicamentoId medicamento1,
        MedicamentoId medicamento2,
        CancellationToken cancellationToken = default);
}