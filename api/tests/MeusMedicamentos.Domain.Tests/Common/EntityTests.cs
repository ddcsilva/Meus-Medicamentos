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

    private class FakeDomainEvent : IDomainEvent
    {
        public Guid EventId { get; } = Guid.NewGuid();

        public DateTime OcorridoEm => DateTime.UtcNow;
    }

    [Fact]
    public void Construtor_ObjetoCriadoComIdEspecifico_DeveAtribuirEsseIdAoId()
    {
        // Arrange
        var idEsperado = 123;

        // Act
        var entity = new FakeEntity(idEsperado);

        // Assert
        entity.Id.Should().Be(idEsperado);
    }

    [Fact]
    public void Construtor_ObjetoCriado_DeveAtribuirDataDeCriacao()
    {
        // Arrange
        var dataCriacaoEsperada = DateTime.UtcNow;

        // Act
        var entity = new FakeEntity(1);

        // Assert
        entity.CriadoEm.Should().BeCloseTo(dataCriacaoEsperada, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Construtor_ObjetoCriado_NaoDeveAtribuirDataDeAtualizacao()
    {
        // Arrange
        var entity = new FakeEntity(1);

        // Assert
        entity.AtualizadoEm.Should().BeNull();
    }

    [Fact]
    public void Construtor_ObjetoCriado_NaoDeveAtribuirEventos()
    {
        // Arrange
        var entity = new FakeEntity(1);

        // Assert
        entity.DomainEvents.Should().BeEmpty();
    }

    [Fact]
    public void MarcarComoAtualizado_QuandoChamado_DeveAtribuirDataDeAtualizacao()
    {
        // Arrange
        var entity = new FakeEntity(1);

        // Act
        entity.MarcarAtualizado();

        // Assert
        entity.AtualizadoEm.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void AdicionarEvento_QuandoChamado_DeveAdicionarEventoAoObjeto()
    {
        // Arrange
        var evento = new FakeDomainEvent();

        // Act
        var entity = new FakeEntity(1);
        entity.AdicionarEventoFake(evento);

        // Assert
        entity.DomainEvents.Should().Contain(evento);
        entity.DomainEvents.Count.Should().Be(1);
    }

    [Fact]
    public void RemoverEvento_QuandoChamado_DeveRemoverEventoDoObjeto()
    {
        // Arrange
        var evento = new FakeDomainEvent();
        var entity = new FakeEntity(1);
        entity.AdicionarEventoFake(evento);

        // Act
        entity.RemoverEvento(evento);

        // Assert
        entity.DomainEvents.Should().NotContain(evento);
        entity.DomainEvents.Count.Should().Be(0);
    }

    [Fact]
    public void LimparEventos_QuandoChamado_DeveLimparTodosOsEventosDoObjeto()
    {
        // Arrange
        var evento1 = new FakeDomainEvent();
        var evento2 = new FakeDomainEvent();
        var entity = new FakeEntity(1);
        entity.AdicionarEventoFake(evento1);
        entity.AdicionarEventoFake(evento2);

        // Act
        entity.LimparEventos();

        // Assert
        entity.DomainEvents.Should().BeEmpty();
        entity.DomainEvents.Count.Should().Be(0);
    }

    [Fact]
    public void Equals_DeveRetornarTrue_QuandoIdsForemIguais()
    {
        // Arrange
        var id = 123;
        var entity1 = new FakeEntity(id);
        var entity2 = new FakeEntity(id);

        // Act
        var resultado = entity1.Equals(entity2);

        // Assert
        resultado.Should().BeTrue();
    }

    [Fact]
    public void Equals_DeveRetornarFalse_QuandoIdsForemDiferentes()
    {
        // Arrange
        var id1 = 123;
        var id2 = 456;
        var entity1 = new FakeEntity(id1);
        var entity2 = new FakeEntity(id2);

        // Act
        var resultado = entity1.Equals(entity2);

        // Assert
        resultado.Should().BeFalse();
    }

    [Fact]
    public void Equals_DeveRetornarFalse_QuandoObjetoNaoForDoTipoEsperado()
    {
        // Arrange
        var entity = new FakeEntity(1);
        var outroObjeto = new object();

        // Act
        var resultado = entity.Equals(outroObjeto);

        // Assert
        resultado.Should().BeFalse();
    }

    [Fact]
    public void OperadorIgualdade_DeveRetornarTrue_ParaEntidadesComMesmoId()
    {
        // Arrange
        var id = 123;
        var entity1 = new FakeEntity(id);
        var entity2 = new FakeEntity(id);

        // Act
        var resultado = entity1 == entity2;

        // Assert
        resultado.Should().BeTrue();
    }

    [Fact]
    public void OperadorDesigualdade_DeveRetornarTrue_ParaEntidadesComIdsDiferentes()
    {
        // Arrange
        var id1 = 123;
        var id2 = 456;
        var entity1 = new FakeEntity(id1);
        var entity2 = new FakeEntity(id2);

        // Act
        var resultado = entity1 != entity2;

        // Assert
        resultado.Should().BeTrue();
    }

    [Fact]
    public void GetHashCode_DeveSerIgual_ParaEntidadesComMesmoId()
    {
        // Arrange
        var id = 123;
        var entity1 = new FakeEntity(id);
        var entity2 = new FakeEntity(id);

        // Act
        var hash1 = entity1.GetHashCode();
        var hash2 = entity2.GetHashCode();

        // Assert
        hash1.Should().Be(hash2);
    }

    [Fact]
    public void Equals_DeveRetornarFalse_QuandoEntidadeForNull()
    {
        // Arrange
        var entity = new FakeEntity(1);
        FakeEntity? outraEntity = null;

        // Act
        var resultado = entity.Equals(outraEntity);

        // Assert
        resultado.Should().BeFalse();
    }
}