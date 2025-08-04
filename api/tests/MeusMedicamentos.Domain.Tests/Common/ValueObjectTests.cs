using FluentAssertions;
using MeusMedicamentos.Domain.Common;

namespace MeusMedicamentos.Domain.Tests.Common;

public class ValueObjectTests
{
    private class TestValueObject(int numero, string texto) : ValueObject
    {
        public int Numero { get; } = numero;
        public string Texto { get; } = texto;

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Numero;
            yield return Texto;
        }
    }

    private class OutroValueObject(int numero) : ValueObject
    {
        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return numero;
        }
    }

    [Fact]
    public void Equals_QuandoObjetosSaoIguais_DeveRetornarTrue()
    {
        // Arrange
        var obj1 = new TestValueObject(1, "Teste");
        var obj2 = new TestValueObject(1, "Teste");

        // Act
        var result = obj1.Equals(obj2);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Equals_QuandoObjetosSaoDiferentes_DeveRetornarFalse()
    {
        // Arrange
        var obj1 = new TestValueObject(1, "Teste");
        var obj2 = new TestValueObject(2, "Teste");

        // Act
        var result = obj1.Equals(obj2);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void GetHashCode_QuandoObjetosSaoIguais_DeveRetornarMesmoHashCode()
    {
        // Arrange
        var obj1 = new TestValueObject(1, "Teste");
        var obj2 = new TestValueObject(1, "Teste");

        // Act
        var hashCode1 = obj1.GetHashCode();
        var hashCode2 = obj2.GetHashCode();

        // Assert
        hashCode1.Should().Be(hashCode2);
    }

    [Fact]
    public void GetHashCode_QuandoObjetosSaoDiferentes_DeveRetornarDiferentesHashCodes()
    {
        // Arrange
        var obj1 = new TestValueObject(1, "Teste");
        var obj2 = new TestValueObject(2, "Teste");

        // Act
        var hashCode1 = obj1.GetHashCode();
        var hashCode2 = obj2.GetHashCode();

        // Assert
        hashCode1.Should().NotBe(hashCode2);
    }

    [Fact]
    public void OperadorIgualdade_QuandoObjetosSaoIguais_DeveRetornarTrue()
    {
        // Arrange
        var obj1 = new TestValueObject(1, "Teste");
        var obj2 = new TestValueObject(1, "Teste");

        // Act
        var result = obj1 == obj2;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void OperadorDesigualdade_QuandoObjetosSaoDiferentes_DeveRetornarFalse()
    {
        // Arrange
        var obj1 = new TestValueObject(1, "Teste");
        var obj2 = new TestValueObject(2, "Teste");

        // Act
        var result = obj1 != obj2;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Equals_QuandoTiposSaoDiferentes_DeveRetornarFalse()
    {
        // Arrange
        ValueObject obj1 = new TestValueObject(1, "Teste");
        ValueObject obj2 = new OutroValueObject(1);

        // Act
        var result = obj1.Equals(obj2);

        // Assert
        result.Should().BeFalse();
    }
}