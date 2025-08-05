using FluentAssertions;
using MeusMedicamentos.Domain.Entities.Ids;
using MeusMedicamentos.Domain.Events;

namespace MeusMedicamentos.Domain.Tests.Events;

public class MedicamentoVencidoEventTests
{
    [Fact]
    public void Construtor_QuandoRecebeParametrosValidos_DeveInstanciarCorretamente()
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);
        var nomeMedicamento = "Enalapril 10mg";
        var dataValidade = new DateTime(2023, 12, 31);
        var quantidadeVencida = 15;

        // Act
        var evento = new MedicamentoVencidoEvent(medicamentoId, nomeMedicamento, dataValidade, quantidadeVencida);

        // Assert
        evento.MedicamentoId.Should().Be(medicamentoId);
        evento.NomeMedicamento.Should().Be(nomeMedicamento);
        evento.DataValidade.Should().Be(dataValidade);
        evento.QuantidadeVencida.Should().Be(quantidadeVencida);
    }

    [Fact]
    public void EventId_QuandoEventoEInstanciado_DeveGerarIdUnico()
    {
        // Arrange & Act
        var dataValidade = DateTime.Today.AddDays(-1);
        var evento1 = new MedicamentoVencidoEvent(new MedicamentoId(1), "Medicamento 1", dataValidade, 10);
        var evento2 = new MedicamentoVencidoEvent(new MedicamentoId(2), "Medicamento 2", dataValidade, 20);

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
        var dataValidade = DateTime.Today.AddDays(-5);

        // Act
        var evento = new MedicamentoVencidoEvent(new MedicamentoId(1), "Enalapril", dataValidade, 10);

        // Assert
        var dataDepois = DateTime.UtcNow;
        evento.OcorridoEm.Should().BeOnOrAfter(dataAntes);
        evento.OcorridoEm.Should().BeOnOrBefore(dataDepois);
    }

    [Fact]
    public void Construtor_QuandoQuantidadeVencidaEZero_DevePermitir()
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);
        var nomeMedicamento = "Medicamento Zero";
        var dataValidade = DateTime.Today.AddDays(-1);
        var quantidadeVencida = 0;

        // Act
        var evento = new MedicamentoVencidoEvent(medicamentoId, nomeMedicamento, dataValidade, quantidadeVencida);

        // Assert
        evento.QuantidadeVencida.Should().Be(quantidadeVencida);
    }

    [Fact]
    public void Construtor_QuandoQuantidadeVencidaENegativa_DevePermitir()
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);
        var nomeMedicamento = "Medicamento Negativo";
        var dataValidade = DateTime.Today.AddDays(-1);
        var quantidadeVencida = -5;

        // Act
        var evento = new MedicamentoVencidoEvent(medicamentoId, nomeMedicamento, dataValidade, quantidadeVencida);

        // Assert
        evento.QuantidadeVencida.Should().Be(quantidadeVencida);
    }

    [Fact]
    public void Construtor_QuandoDataValidadeEFutura_DevePermitir()
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);
        var nomeMedicamento = "Medicamento Futuro";
        var dataValidade = DateTime.Today.AddDays(30);
        var quantidadeVencida = 5;

        // Act
        var evento = new MedicamentoVencidoEvent(medicamentoId, nomeMedicamento, dataValidade, quantidadeVencida);

        // Assert
        evento.DataValidade.Should().Be(dataValidade);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Construtor_QuandoRecebeNomeMedicamentoInvalido_DevePermitir(string nomeMedicamento)
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);
        var dataValidade = DateTime.Today.AddDays(-1);
        var quantidadeVencida = 10;

        // Act
        var evento = new MedicamentoVencidoEvent(medicamentoId, nomeMedicamento, dataValidade, quantidadeVencida);

        // Assert
        evento.NomeMedicamento.Should().Be(nomeMedicamento);
    }

    [Fact]
    public void Construtor_QuandoDataValidadeEMinValue_DevePermitir()
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);
        var nomeMedicamento = "Teste MinValue";
        var dataValidade = DateTime.MinValue;
        var quantidadeVencida = 1;

        // Act
        var evento = new MedicamentoVencidoEvent(medicamentoId, nomeMedicamento, dataValidade, quantidadeVencida);

        // Assert
        evento.DataValidade.Should().Be(dataValidade);
    }

    [Fact]
    public void Construtor_QuandoDataValidadeEMaxValue_DevePermitir()
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);
        var nomeMedicamento = "Teste MaxValue";
        var dataValidade = DateTime.MaxValue;
        var quantidadeVencida = int.MaxValue;

        // Act
        var evento = new MedicamentoVencidoEvent(medicamentoId, nomeMedicamento, dataValidade, quantidadeVencida);

        // Assert
        evento.DataValidade.Should().Be(dataValidade);
        evento.QuantidadeVencida.Should().Be(quantidadeVencida);
    }

    [Fact]
    public void Construtor_QuandoQuantidadeVencidaEMaxValue_DevePermitir()
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);
        var nomeMedicamento = "Teste Quantidade Máxima";
        var dataValidade = DateTime.Today.AddDays(-1);
        var quantidadeVencida = int.MaxValue;

        // Act
        var evento = new MedicamentoVencidoEvent(medicamentoId, nomeMedicamento, dataValidade, quantidadeVencida);

        // Assert
        evento.QuantidadeVencida.Should().Be(quantidadeVencida);
    }
}