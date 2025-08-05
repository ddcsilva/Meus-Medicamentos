using FluentAssertions;
using MeusMedicamentos.Domain.Entities.Ids;
using MeusMedicamentos.Domain.Events;

namespace MeusMedicamentos.Domain.Tests.Events;

public class MedicamentoCadastradoEventTests
{
    [Fact]
    public void Construtor_QuandoRecebeParametrosValidos_DeveInstanciarCorretamente()
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);
        var nomeMedicamento = "Amoxicilina 500mg";

        // Act
        var evento = new MedicamentoCadastradoEvent(medicamentoId, nomeMedicamento);

        // Assert
        evento.MedicamentoId.Should().Be(medicamentoId);
        evento.NomeMedicamento.Should().Be(nomeMedicamento);
    }

    [Fact]
    public void EventId_QuandoEventoEInstanciado_DeveGerarIdUnico()
    {
        // Arrange & Act
        var evento1 = new MedicamentoCadastradoEvent(new MedicamentoId(1), "Medicamento 1");
        var evento2 = new MedicamentoCadastradoEvent(new MedicamentoId(2), "Medicamento 2");

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
        var evento = new MedicamentoCadastradoEvent(new MedicamentoId(1), "Amoxicilina");

        // Assert
        var dataDepois = DateTime.UtcNow;
        evento.OcorridoEm.Should().BeOnOrAfter(dataAntes);
        evento.OcorridoEm.Should().BeOnOrBefore(dataDepois);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Construtor_QuandoRecebeNomeMedicamentoInvalido_DevePermitir(string nomeMedicamento)
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);

        // Act
        var evento = new MedicamentoCadastradoEvent(medicamentoId, nomeMedicamento);

        // Assert
        evento.NomeMedicamento.Should().Be(nomeMedicamento);
    }

    [Fact]
    public void Construtor_QuandoRecebeNomeMedicamentoComEspacos_DevePreservarEspacos()
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);
        var nomeMedicamento = "  Dipirona Sódica 500mg  ";

        // Act
        var evento = new MedicamentoCadastradoEvent(medicamentoId, nomeMedicamento);

        // Assert
        evento.NomeMedicamento.Should().Be(nomeMedicamento);
    }

    [Fact]
    public void Construtor_QuandoRecebeMedicamentoIdZero_DevePermitir()
    {
        // Arrange
        var medicamentoId = new MedicamentoId(0);
        var nomeMedicamento = "Teste";

        // Act
        var evento = new MedicamentoCadastradoEvent(medicamentoId, nomeMedicamento);

        // Assert
        evento.MedicamentoId.Should().Be(medicamentoId);
    }

    [Fact]
    public void Construtor_QuandoRecebeMedicamentoIdNegativo_DevePermitir()
    {
        // Arrange
        var medicamentoId = new MedicamentoId(-1);
        var nomeMedicamento = "Teste Negativo";

        // Act
        var evento = new MedicamentoCadastradoEvent(medicamentoId, nomeMedicamento);

        // Assert
        evento.MedicamentoId.Should().Be(medicamentoId);
    }
}