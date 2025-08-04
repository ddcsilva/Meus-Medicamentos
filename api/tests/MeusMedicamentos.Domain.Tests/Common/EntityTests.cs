using FluentAssertions;
using MeusMedicamentos.Domain.Common;

namespace MeusMedicamentos.Domain.Tests.Common;

public class EntityTests
{
    private class FakeEntity : Entity<int>
    {
        public FakeEntity(int id) => Id = id;

        public void MarcarAtualizado() => MarcarComoAtualizado();

        public void AdicionarEventoFake(IDomainEvent e) => AdicionarEvento(e);
    }

    [Fact]
    public void Construtor_ObjetoCriadoComId42_DeveAtribuir42AoId()
    {
        // Arrange
        var idEsperado = 42;

        // Act
        var entity = new FakeEntity(idEsperado);

        // Assert
        entity.Id.Should().Be(idEsperado);
    }
}