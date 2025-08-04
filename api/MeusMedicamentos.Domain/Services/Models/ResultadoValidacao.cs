namespace MeusMedicamentos.Domain.Services.Models;

/// <summary>
/// Resultado de uma validação de domínio
/// </summary>
public record ResultadoValidacao(
    bool EValido,
    string? MensagemErro = null,
    IEnumerable<string>? Avisos = null
);