using MeusMedicamentos.Domain.Entities;

namespace MeusMedicamentos.Domain.Services;

/// <summary>
/// DTO para notificações de medicamentos
/// </summary>
public record NotificacoesMedicamentos(
    IEnumerable<Medicamento> VencendoEm7Dias,
    IEnumerable<Medicamento> VencendoEm30Dias,
    IEnumerable<Medicamento> Vencidos,
    IEnumerable<Medicamento> ComEstoqueBaixo
);