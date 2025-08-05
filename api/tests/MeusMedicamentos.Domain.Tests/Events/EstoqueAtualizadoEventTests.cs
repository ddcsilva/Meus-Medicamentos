using FluentAssertions;
using MeusMedicamentos.Domain.Entities.Ids;
using MeusMedicamentos.Domain.Events;

namespace MeusMedicamentos.Domain.Tests.Events;

public class EstoqueAtualizadoEventTests
{
    [Fact]
    public void Construtor_QuandoRecebeParametrosValidos_DeveInstanciarCorretamente()
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);
        var nomeMedicamento = "Dipirona 500mg";
        var quantidadeMovimentada = 10;
        var motivo = "Compra de estoque";

        // Act
        var evento = new EstoqueAtualizadoEvent(medicamentoId, nomeMedicamento, quantidadeMovimentada, motivo);

        // Assert
        evento.MedicamentoId.Should().Be(medicamentoId);
        evento.NomeMedicamento.Should().Be(nomeMedicamento);
        evento.QuantidadeMovimentada.Should().Be(quantidadeMovimentada);
        evento.Motivo.Should().Be(motivo);
    }

    [Fact]
    public void EventId_QuandoEventoEInstanciado_DeveGerarIdUnico()
    {
        // Arrange & Act
        var evento1 = new EstoqueAtualizadoEvent(new MedicamentoId(1), "Medicamento 1", 5, "Motivo 1");
        var evento2 = new EstoqueAtualizadoEvent(new MedicamentoId(2), "Medicamento 2", 10, "Motivo 2");

        // Assert
        evento1.EventId.Should().NotBe(Guid.Empty);
        evento2.EventId.Should().NotBe(Guid.Empty);
        evento1.EventId.Should().NotBe(evento2.EventId);
    }

    [Fact]
    public void OcorridoEm_QuandoEventoEInstanciado_DeveDefinirDataAtual()
    {
        // Arrange
        var dataAntes = DateTime.UtcNow;

        // Act
        var evento = new EstoqueAtualizadoEvent(new MedicamentoId(1), "Dipirona", 5, "Teste");

        // Assert
        var dataDepois = DateTime.UtcNow;
        evento.OcorridoEm.Should().BeOnOrAfter(dataAntes);
        evento.OcorridoEm.Should().BeOnOrBefore(dataDepois);
    }

    [Fact]
    public void Construtor_QuandoRecebeQuantidadeNegativa_DevePermitir()
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);
        var nomeMedicamento = "Dipirona 500mg";
        var quantidadeMovimentada = -5;
        var motivo = "Uso do medicamento";

        // Act
        var evento = new EstoqueAtualizadoEvent(medicamentoId, nomeMedicamento, quantidadeMovimentada, motivo);

        // Assert
        evento.QuantidadeMovimentada.Should().Be(quantidadeMovimentada);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Construtor_QuandoRecebeNomeMedicamentoInvalido_DevePermitir(string nomeMedicamento)
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);
        var quantidadeMovimentada = 10;
        var motivo = "Teste";

        // Act
        var evento = new EstoqueAtualizadoEvent(medicamentoId, nomeMedicamento, quantidadeMovimentada, motivo);

        // Assert
        evento.NomeMedicamento.Should().Be(nomeMedicamento);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Construtor_QuandoRecebeMotivoInvalido_DevePermitir(string motivo)
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);
        var nomeMedicamento = "Dipirona";
        var quantidadeMovimentada = 10;

        // Act
        var evento = new EstoqueAtualizadoEvent(medicamentoId, nomeMedicamento, quantidadeMovimentada, motivo);

        // Assert
        evento.Motivo.Should().Be(motivo);
    }
}