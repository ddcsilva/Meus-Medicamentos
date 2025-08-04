using System.Text.RegularExpressions;
using MeusMedicamentos.Domain.Common;
using MeusMedicamentos.Domain.Entities.Ids;
using MeusMedicamentos.Domain.Enums;
using MeusMedicamentos.Domain.Events;
using MeusMedicamentos.Domain.ValueObjects;

namespace MeusMedicamentos.Domain.Entities;

/// <summary>
/// Aggregate Root - Entity principal que representa um medicamento
/// </summary>
public class Medicamento : Entity<MedicamentoId>
{
    /// <summary>
    /// Nome comercial do medicamento
    /// </summary>
    public string Nome { get; private set; }

    /// <summary>
    /// Princípio ativo (substância)
    /// </summary>
    public string PrincipioAtivo { get; private set; }

    /// <summary>
    /// Dosagem do medicamento (Value Object)
    /// </summary>
    public Dosagem Dosagem { get; private set; }

    /// <summary>
    /// Forma farmacêutica (comprimido, xarope, etc.)
    /// </summary>
    public FormaFarmaceutica Forma { get; private set; }

    /// <summary>
    /// Fabricante/laboratório
    /// </summary>
    public string Fabricante { get; private set; }

    /// <summary>
    /// Data de validade (Value Object)
    /// </summary>
    public DataValidade DataValidade { get; private set; }

    /// <summary>
    /// Quantidade atual em estoque (Value Object)
    /// </summary>
    public Quantidade QuantidadeAtual { get; private set; }

    /// <summary>
    /// Quantidade mínima para alerta (Value Object)
    /// </summary>
    public Quantidade QuantidadeMinima { get; private set; }

    /// <summary>
    /// Local onde está armazenado (Value Object)
    /// </summary>
    public LocalArmazenamento Local { get; private set; }

    /// <summary>
    /// Número do lote
    /// </summary>
    public string? Lote { get; private set; }

    /// <summary>
    /// Código de barras (EAN)
    /// </summary>
    public string? CodigoBarras { get; private set; }

    /// <summary>
    /// ID da categoria
    /// </summary>
    public CategoriaId CategoriaId { get; private set; }

    /// <summary>
    /// Navegação para categoria
    /// </summary>
    public Categoria? Categoria { get; private set; }

    /// <summary>
    /// Se o medicamento está ativo
    /// </summary>
    public bool Ativo { get; private set; }

    /// <summary>
    /// Observações gerais
    /// </summary>
    public string? Observacoes { get; private set; }

    /// <summary>
    /// Construtor privado - força uso do factory method
    /// </summary>
    private Medicamento(
        string nome,
        string principioAtivo,
        Dosagem dosagem,
        FormaFarmaceutica forma,
        string fabricante,
        DataValidade dataValidade,
        Quantidade quantidadeAtual,
        Quantidade quantidadeMinima,
        LocalArmazenamento local,
        CategoriaId categoriaId,
        string? lote = null,
        string? codigoBarras = null,
        string? observacoes = null)
    {
        Nome = nome;
        PrincipioAtivo = principioAtivo;
        Dosagem = dosagem;
        Forma = forma;
        Fabricante = fabricante;
        DataValidade = dataValidade;
        QuantidadeAtual = quantidadeAtual;
        QuantidadeMinima = quantidadeMinima;
        Local = local;
        CategoriaId = categoriaId;
        Lote = lote;
        CodigoBarras = codigoBarras;
        Observacoes = observacoes;
        Ativo = true;

        // Domain Event: medicamento foi cadastrado
        AdicionarEvento(new MedicamentoCadastradoEvent(Id, Nome));
    }

    /// <summary>
    /// Factory method para criar novo medicamento
    /// </summary>
    public static Medicamento Criar(
        string nome,
        string principioAtivo,
        string dosagem,
        FormaFarmaceutica forma,
        string fabricante,
        DateTime dataValidade,
        int quantidadeAtual,
        int quantidadeMinima,
        string localArmazenamento,
        CategoriaId categoriaId,
        string? lote = null,
        string? codigoBarras = null,
        string? observacoes = null)
    {
        // Validações básicas
        ValidarCamposObrigatorios(nome, principioAtivo, fabricante);
        ValidarCodigoBarras(codigoBarras);

        // Criação dos Value Objects com suas validações
        var dosagemVO = Dosagem.Criar(dosagem);
        var dataValidadeVO = DataValidade.Criar(dataValidade);
        var quantidadeAtualVO = Quantidade.Criar(quantidadeAtual);
        var quantidadeMinimaVO = Quantidade.Criar(quantidadeMinima);
        var localVO = LocalArmazenamento.Criar(localArmazenamento);

        return new Medicamento(
            nome.Trim(),
            principioAtivo.Trim(),
            dosagemVO,
            forma,
            fabricante.Trim(),
            dataValidadeVO,
            quantidadeAtualVO,
            quantidadeMinimaVO,
            localVO,
            categoriaId,
            lote?.Trim(),
            codigoBarras?.Trim(),
            observacoes?.Trim());
    }

    /// <summary>
    /// Atualiza o estoque do medicamento
    /// </summary>
    public void AtualizarEstoque(int quantidadeMovimentacao, string motivo = "Atualização manual")
    {
        var novaQuantidade = QuantidadeAtual.Adicionar(quantidadeMovimentacao);
        QuantidadeAtual = novaQuantidade;

        MarcarComoAtualizado();

        // Domain Event: estoque foi atualizado
        AdicionarEvento(new EstoqueAtualizadoEvent(Id, Nome, quantidadeMovimentacao, motivo));

        // Verifica se ficou com estoque baixo
        if (QuantidadeAtual <= QuantidadeMinima)
        {
            AdicionarEvento(new EstoqueBaixoEvent(Id, Nome, QuantidadeAtual.Valor, QuantidadeMinima.Valor));
        }
    }

    /// <summary>
    /// Consome medicamento (reduz estoque)
    /// </summary>
    public void Consumir(int quantidade, string motivo = "Consumo")
    {
        if (quantidade <= 0)
            throw new DomainException("Quantidade a consumir deve ser positiva");

        if (QuantidadeAtual.Valor < quantidade)
            throw new DomainException($"Estoque insuficiente. Disponível: {QuantidadeAtual.Valor}");

        AtualizarEstoque(-quantidade, motivo);
    }

    /// <summary>
    /// Adiciona medicamento ao estoque
    /// </summary>
    public void AdicionarAoEstoque(int quantidade, string motivo = "Compra")
    {
        if (quantidade <= 0)
            throw new DomainException("Quantidade a adicionar deve ser positiva");

        AtualizarEstoque(quantidade, motivo);
    }

    /// <summary>
    /// Verifica se o medicamento está vencido
    /// </summary>
    public bool EstaVencido()
    {
        return DataValidade.EstaVencido();
    }

    /// <summary>
    /// Verifica se vence dentro de X dias
    /// </summary>
    public bool VenceEm(int dias)
    {
        return DataValidade.VenceEm(dias);
    }

    /// <summary>
    /// Verifica se o estoque está baixo
    /// </summary>
    public bool EstoqueEstaAbaixoDoMinimo()
    {
        return QuantidadeAtual <= QuantidadeMinima;
    }

    /// <summary>
    /// Atualiza informações básicas do medicamento
    /// </summary>
    public void AtualizarInformacoes(
        string nome,
        string principioAtivo,
        string fabricante,
        string? observacoes = null)
    {
        ValidarCamposObrigatorios(nome, principioAtivo, fabricante);

        Nome = nome.Trim();
        PrincipioAtivo = principioAtivo.Trim();
        Fabricante = fabricante.Trim();
        Observacoes = observacoes?.Trim();

        MarcarComoAtualizado();
    }

    /// <summary>
    /// Muda o local de armazenamento
    /// </summary>
    public void MudarLocalArmazenamento(string novoLocal)
    {
        var localAntigo = Local.Descricao;
        Local = LocalArmazenamento.Criar(novoLocal);

        MarcarComoAtualizado();

        AdicionarEvento(new LocalArmazenamentoAlteradoEvent(Id, Nome, localAntigo, novoLocal));
    }

    /// <summary>
    /// Desativa o medicamento (soft delete)
    /// </summary>
    public void Desativar()
    {
        Ativo = false;
        MarcarComoAtualizado();
    }

    /// <summary>
    /// Reativa o medicamento
    /// </summary>
    public void Ativar()
    {
        Ativo = true;
        MarcarComoAtualizado();
    }

    /// <summary>
    /// Validações de campos obrigatórios
    /// </summary>
    private static void ValidarCamposObrigatorios(string nome, string principioAtivo, string fabricante)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("Nome do medicamento é obrigatório");

        if (string.IsNullOrWhiteSpace(principioAtivo))
            throw new DomainException("Princípio ativo é obrigatório");

        if (string.IsNullOrWhiteSpace(fabricante))
            throw new DomainException("Fabricante é obrigatório");

        if (nome.Length > 100)
            throw new DomainException("Nome do medicamento deve ter no máximo 100 caracteres");

        if (principioAtivo.Length > 100)
            throw new DomainException("Princípio ativo deve ter no máximo 100 caracteres");

        if (fabricante.Length > 50)
            throw new DomainException("Fabricante deve ter no máximo 50 caracteres");
    }

    /// <summary>
    /// Validação do código de barras (EAN-13)
    /// </summary>
    private static void ValidarCodigoBarras(string? codigoBarras)
    {
        if (codigoBarras != null && !Regex.IsMatch(codigoBarras, @"^\d{13}$"))
            throw new DomainException("Código de barras deve ter exatamente 13 dígitos");
    }
}