namespace MeusMedicamentos.Application.DTOs.Common;

/// <summary>
/// DTO base com propriedades comuns de auditoria
/// </summary>
public abstract record BaseDto
{
    public DateTime CriadoEm { get; init; }
    public DateTime? AtualizadoEm { get; init; }
}