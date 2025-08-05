using FluentAssertions;
using MeusMedicamentos.Domain.Entities.Ids;
using MeusMedicamentos.Domain.Events;

namespace MeusMedicamentos.Domain.Tests.Events;

public class LocalArmazenamentoAlteradoEventTests
{
    [Fact]
    public void Construtor_QuandoRecebeParametrosValidos_DeveInstanciarCorretamente()
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);
        var nomeMedicamento = "Omeprazol 20mg";
        var localAnterior = "Armário da Cozinha";
        var novoLocal = "Armário do Quarto";

        // Act
        var evento = new LocalArmazenamentoAlteradoEvent(medicamentoId, nomeMedicamento, localAnterior, novoLocal);

        // Assert
        evento.MedicamentoId.Should().Be(medicamentoId);
        evento.NomeMedicamento.Should().Be(nomeMedicamento);
        evento.LocalAnterior.Should().Be(localAnterior);
        evento.NovoLocal.Should().Be(novoLocal);
    }

    [Fact]
    public void EventId_QuandoEventoEInstanciado_DeveGerarIdUnico()
    {
        // Arrange & Act
        var evento1 = new LocalArmazenamentoAlteradoEvent(new MedicamentoId(1), "Medicamento 1", "Local A", "Local B");
        var evento2 = new LocalArmazenamentoAlteradoEvent(new MedicamentoId(2), "Medicamento 2", "Local C", "Local D");

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
        var evento = new LocalArmazenamentoAlteradoEvent(new MedicamentoId(1), "Omeprazol", "Cozinha", "Quarto");

        // Assert
        var dataDepois = DateTime.UtcNow;
        evento.OcorridoEm.Should().BeOnOrAfter(dataAntes);
        evento.OcorridoEm.Should().BeOnOrBefore(dataDepois);
    }

    [Fact]
    public void Construtor_QuandoLocaisSaoIguais_DevePermitir()
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);
        var nomeMedicamento = "Losartana 50mg";
        var local = "Armário Principal";

        // Act
        var evento = new LocalArmazenamentoAlteradoEvent(medicamentoId, nomeMedicamento, local, local);

        // Assert
        evento.LocalAnterior.Should().Be(local);
        evento.NovoLocal.Should().Be(local);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Construtor_QuandoRecebeNomeMedicamentoInvalido_DevePermitir(string nomeMedicamento)
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);
        var localAnterior = "Local A";
        var novoLocal = "Local B";

        // Act
        var evento = new LocalArmazenamentoAlteradoEvent(medicamentoId, nomeMedicamento, localAnterior, novoLocal);

        // Assert
        evento.NomeMedicamento.Should().Be(nomeMedicamento);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Construtor_QuandoRecebeLocalAnteriorInvalido_DevePermitir(string localAnterior)
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);
        var nomeMedicamento = "Medicamento Teste";
        var novoLocal = "Novo Local";

        // Act
        var evento = new LocalArmazenamentoAlteradoEvent(medicamentoId, nomeMedicamento, localAnterior, novoLocal);

        // Assert
        evento.LocalAnterior.Should().Be(localAnterior);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Construtor_QuandoRecebeNovoLocalInvalido_DevePermitir(string novoLocal)
    {
        // Arrange
        var medicamentoId = new MedicamentoId(1);
        var nomeMedicamento = "Medicamento Teste";
        var localAnterior = "Local Anterior";

        // Act
        var evento = new LocalArmazenamentoAlteradoEvent(medicamentoId, nomeMedicamento, localAnterior, novoLocal);

        // Assert
        evento.NovoLocal.Should().Be(novoLocal);
    }
}