using FluentAssertions;
using MeusMedicamentos.Domain.Common;

namespace MeusMedicamentos.Domain.Tests.Common;

public class DomainExceptionTests
{
    [Fact]
    public void Construtor_QuandoExcecaoPossuiMensagem_DeveInstanciarComMensagem()
    {
        // Arrange
        var mensagem = "Mensagem de teste";

        // Act
        var exception = new DomainException(mensagem);

        // Assert
        exception.Message.Should().Be(mensagem);
        exception.InnerException.Should().BeNull();
    }

    [Fact]
    public void Construtor_QuandoExcecaoPossuiMensagemEExcecaoInterna_DeveInstanciarComMensagemEExcecaoInterna()
    {
        // Arrange
        var mensagem = "Mensagem de teste";
        var excecaoInterna = new Exception("Exceção interna de teste");

        // Act
        var exception = new DomainException(mensagem, excecaoInterna);

        // Assert
        exception.Message.Should().Be(mensagem);
        exception.InnerException.Should().Be(excecaoInterna);
    }
}
