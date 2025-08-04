using FluentAssertions;
using MeusMedicamentos.Domain.Entities.Ids;

namespace MeusMedicamentos.Domain.Tests.Entities.Ids;

public class CategoriaIdTests
{
    [Fact]
    public void ConversaoImplicitaDeIntParaCategoriaId_QuandoRecebeValor_DeveAtribuirValorCorretamente()
    {
        // Arrange
        int valorEsperado = 1;

        // Act
        CategoriaId categoriaId = valorEsperado;

        // Assert
        categoriaId.Valor.Should().Be(valorEsperado);
    }

    [Fact]
    public void ConversaoImplicitaDeCategoriaIdParaInt_QuandoRecebeObjeto_DeveRetornarValor()
    {
        // Arrange
        var categoriaId = new CategoriaId(10);

        // Act
        int valor = categoriaId;

        // Assert
        valor.Should().Be(10);
    }

    [Fact]
    public void ToString_QuandoChamado_DeveRetornarValorComoString()
    {
        // Arrange
        var categoriaId = new CategoriaId(99);

        // Act
        var resultado = categoriaId.ToString();

        // Assert
        resultado.Should().Be("99");
    }

    [Fact]
    public void Equals_QuandoCategoriaIdPossuemMesmoValor_DeveRetornarTrue()
    {
        // Arrange
        var id1 = new CategoriaId(5);
        var id2 = new CategoriaId(5);

        // Act & Assert
        (id1 == id2).Should().BeTrue();
        id1.Equals(id2).Should().BeTrue();
    }

    [Fact]
    public void Equals_QuandoCategoriaIdPossuemValoresDiferentes_DeveRetornarFalse()
    {
        // Arrange
        var id1 = new CategoriaId(5);
        var id2 = new CategoriaId(10);

        // Act & Assert
        (id1 == id2).Should().BeFalse();
        id1.Equals(id2).Should().BeFalse();
    }
}