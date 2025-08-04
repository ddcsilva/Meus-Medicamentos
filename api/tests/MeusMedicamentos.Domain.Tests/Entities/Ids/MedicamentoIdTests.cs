using FluentAssertions;
using MeusMedicamentos.Domain.Entities.Ids;

namespace MeusMedicamentos.Domain.Tests.Entities.Ids;

public class MedicamentoIdTests
{
    [Fact]
    public void ConversaoImplicitaDeIntParaMedicamentoId_QuandoRecebeValor_DeveAtribuirValorCorretamente()
    {
        // Arrange
        int valorEsperado = 1;

        // Act
        MedicamentoId medicamentoId = valorEsperado;

        // Assert
        medicamentoId.Valor.Should().Be(valorEsperado);
    }

    [Fact]
    public void ConversaoImplicitaDeMedicamentoIdParaInt_QuandoRecebeObjeto_DeveRetornarValor()
    {
        // Arrange
        var medicamentoId = new MedicamentoId(10);

        // Act
        int valor = medicamentoId;

        // Assert
        valor.Should().Be(10);
    }

    [Fact]
    public void ToString_QuandoChamado_DeveRetornarValorComoString()
    {
        // Arrange
        var medicamentoId = new MedicamentoId(99);

        // Act
        var resultado = medicamentoId.ToString();

        // Assert
        resultado.Should().Be("99");
    }

    [Fact]
    public void Equals_QuandoMedicamentoIdPossuemMesmoValor_DeveRetornarTrue()
    {
        // Arrange
        var id1 = new MedicamentoId(5);
        var id2 = new MedicamentoId(5);

        // Act & Assert
        (id1 == id2).Should().BeTrue();
        id1.Equals(id2).Should().BeTrue();
    }

    [Fact]
    public void Equals_QuandoMedicamentoIdPossuemValoresDiferentes_DeveRetornarFalse()
    {
        // Arrange
        var id1 = new MedicamentoId(5);
        var id2 = new MedicamentoId(10);

        // Act & Assert
        (id1 == id2).Should().BeFalse();
        id1.Equals(id2).Should().BeFalse();
    }
}