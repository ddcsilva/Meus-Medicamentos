using FluentAssertions;
using MeusMedicamentos.Domain.Entities.Ids;
using MeusMedicamentos.Domain.Events;

namespace MeusMedicamentos.Domain.Tests.Events;

public class EstoqueBaixoEventTests
{
    [Fact]
    public void Construtor_QuandoRecebeParametrosValidos_DeveInstanciarCorretamente()
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);
        var nomeMedicamento = "Paracetamol 750mg";
        var quantidadeAtual = 2;
        var quantidadeMinima = 5;

        // Act
        var evento = new EstoqueBaixoEvent(medicamentoId, nomeMedicamento, quantidadeAtual, quantidadeMinima);

        // Assert
        evento.MedicamentoId.Should().Be(medicamentoId);
        evento.NomeMedicamento.Should().Be(nomeMedicamento);
        evento.QuantidadeAtual.Should().Be(quantidadeAtual);
        evento.QuantidadeMinima.Should().Be(quantidadeMinima);
    }

    [Fact]
    public void EventId_QuandoEventoEInstanciado_DeveGerarIdUnico()
    {
        // Arrange & Act
        var evento1 = new EstoqueBaixoEvent(new MedicamentoId(1), "Medicamento 1", 1, 5);
        var evento2 = new EstoqueBaixoEvent(new MedicamentoId(2), "Medicamento 2", 2, 10);

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
        var evento = new EstoqueBaixoEvent(new MedicamentoId(1), "Paracetamol", 2, 5);

        // Assert
        var dataDepois = DateTime.UtcNow;
        evento.OcorridoEm.Should().BeOnOrAfter(dataAntes);
        evento.OcorridoEm.Should().BeOnOrBefore(dataDepois);
    }

    [Fact]
    public void Construtor_QuandoQuantidadeAtualEZero_DevePermitir()
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);
        var nomeMedicamento = "Ibuprofeno 600mg";
        var quantidadeAtual = 0;
        var quantidadeMinima = 3;

        // Act
        var evento = new EstoqueBaixoEvent(medicamentoId, nomeMedicamento, quantidadeAtual, quantidadeMinima);

        // Assert
        evento.QuantidadeAtual.Should().Be(quantidadeAtual);
    }

    [Fact]
    public void Construtor_QuandoQuantidadeAtualMaiorQueMinima_DevePermitir()
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);
        var nomeMedicamento = "Aspirina 100mg";
        var quantidadeAtual = 10;
        var quantidadeMinima = 5;

        // Act
        var evento = new EstoqueBaixoEvent(medicamentoId, nomeMedicamento, quantidadeAtual, quantidadeMinima);

        // Assert
        evento.QuantidadeAtual.Should().Be(quantidadeAtual);
        evento.QuantidadeMinima.Should().Be(quantidadeMinima);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Construtor_QuandoRecebeNomeMedicamentoInvalido_DevePermitir(string nomeMedicamento)
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);
        var quantidadeAtual = 2;
        var quantidadeMinima = 5;

        // Act
        var evento = new EstoqueBaixoEvent(medicamentoId, nomeMedicamento, quantidadeAtual, quantidadeMinima);

        // Assert
        evento.NomeMedicamento.Should().Be(nomeMedicamento);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-10)]
    public void Construtor_QuandoRecebeQuantidadesNegativas_DevePermitir(int quantidade)
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);
        var nomeMedicamento = "Medicamento Teste";

        // Act
        var evento = new EstoqueBaixoEvent(medicamentoId, nomeMedicamento, quantidade, quantidade);

        // Assert
        evento.QuantidadeAtual.Should().Be(quantidade);
        evento.QuantidadeMinima.Should().Be(quantidade);
    }
}