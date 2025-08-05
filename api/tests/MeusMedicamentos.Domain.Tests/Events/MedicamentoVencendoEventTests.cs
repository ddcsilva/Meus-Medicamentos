using FluentAssertions;
using MeusMedicamentos.Domain.Entities.Ids;
using MeusMedicamentos.Domain.Events;

namespace MeusMedicamentos.Domain.Tests.Events;

public class MedicamentoVencendoEventTests
{
    [Fact]
    public void Construtor_QuandoRecebeParametrosValidos_DeveInstanciarCorretamente()
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);
        var nomeMedicamento = "Captopril 25mg";
        var dataValidade = new DateTime(2024, 12, 31);
        var diasParaVencimento = 30;

        // Act
        var evento = new MedicamentoVencendoEvent(medicamentoId, nomeMedicamento, dataValidade, diasParaVencimento);

        // Assert
        evento.MedicamentoId.Should().Be(medicamentoId);
        evento.NomeMedicamento.Should().Be(nomeMedicamento);
        evento.DataValidade.Should().Be(dataValidade);
        evento.DiasParaVencimento.Should().Be(diasParaVencimento);
    }

    [Fact]
    public void EventId_QuandoEventoEInstanciado_DeveGerarIdUnico()
    {
        // Arrange & Act
        var dataValidade = DateTime.Today.AddDays(30);
        var evento1 = new MedicamentoVencendoEvent(new MedicamentoId(1), "Medicamento 1", dataValidade, 30);
        var evento2 = new MedicamentoVencendoEvent(new MedicamentoId(2), "Medicamento 2", dataValidade, 30);

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
        var dataValidade = DateTime.Today.AddDays(15);

        // Act
        var evento = new MedicamentoVencendoEvent(new MedicamentoId(1), "Captopril", dataValidade, 15);

        // Assert
        var dataDepois = DateTime.UtcNow;
        evento.OcorridoEm.Should().BeOnOrAfter(dataAntes);
        evento.OcorridoEm.Should().BeOnOrBefore(dataDepois);
    }

    [Fact]
    public void Construtor_QuandoDataValidadeJaVencida_DevePermitir()
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);
        var nomeMedicamento = "Medicamento Vencido";
        var dataValidade = DateTime.Today.AddDays(-10);
        var diasParaVencimento = -10;

        // Act
        var evento = new MedicamentoVencendoEvent(medicamentoId, nomeMedicamento, dataValidade, diasParaVencimento);

        // Assert
        evento.DataValidade.Should().Be(dataValidade);
        evento.DiasParaVencimento.Should().Be(diasParaVencimento);
    }

    [Fact]
    public void Construtor_QuandoDiasParaVencimentoEZero_DevePermitir()
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);
        var nomeMedicamento = "Medicamento Hoje";
        var dataValidade = DateTime.Today;
        var diasParaVencimento = 0;

        // Act
        var evento = new MedicamentoVencendoEvent(medicamentoId, nomeMedicamento, dataValidade, diasParaVencimento);

        // Assert
        evento.DiasParaVencimento.Should().Be(diasParaVencimento);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Construtor_QuandoRecebeNomeMedicamentoInvalido_DevePermitir(string nomeMedicamento)
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);
        var dataValidade = DateTime.Today.AddDays(30);
        var diasParaVencimento = 30;

        // Act
        var evento = new MedicamentoVencendoEvent(medicamentoId, nomeMedicamento, dataValidade, diasParaVencimento);

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
        var diasParaVencimento = -1000;

        // Act
        var evento = new MedicamentoVencendoEvent(medicamentoId, nomeMedicamento, dataValidade, diasParaVencimento);

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
        var diasParaVencimento = int.MaxValue;

        // Act
        var evento = new MedicamentoVencendoEvent(medicamentoId, nomeMedicamento, dataValidade, diasParaVencimento);

        // Assert
        evento.DataValidade.Should().Be(dataValidade);
        evento.DiasParaVencimento.Should().Be(diasParaVencimento);
    }
}