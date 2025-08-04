using System.Text.RegularExpressions;
using MeusMedicamentos.Domain.Common;
using MeusMedicamentos.Domain.Entities.Ids;

namespace MeusMedicamentos.Domain.Entities;

/// <summary>
/// Entity que representa uma categoria de medicamentos.
/// Exemplos: "Analgésicos", "Antibióticos", "Vitaminas"
/// </summary>
public class Categoria : Entity<CategoriaId>
{
    /// <summary>
    /// Nome da categoria
    /// </summary>
    public string Nome { get; private set; }

    /// <summary>
    /// Descrição opcional da categoria
    /// </summary>
    public string? Descricao { get; private set; }

    /// <summary>
    /// Cor para identificação visual (hexadecimal)
    /// </summary>
    public string? Cor { get; private set; }

    /// <summary>
    /// Se a categoria está ativa
    /// </summary>
    public bool Ativo { get; private set; }

    /// <summary>
    /// Construtor privado para forçar uso do factory method
    /// </summary>
    private Categoria(string nome, string? descricao, string? cor)
    {
        Nome = nome;
        Descricao = descricao;
        Cor = cor;
        Ativo = true;
    }

    /// <summary>
    /// Factory method para criar nova categoria
    /// </summary>
    public static Categoria Criar(string nome, string? descricao = null, string? cor = null)
    {
        ValidarNome(nome);
        ValidarCor(cor);

        return new Categoria(nome, descricao, cor);
    }

    /// <summary>
    /// Atualiza informações da categoria
    /// </summary>
    public void Atualizar(string nome, string? descricao = null, string? cor = null)
    {
        ValidarNome(nome);
        ValidarCor(cor);

        Nome = nome;
        Descricao = descricao;
        Cor = cor;

        MarcarComoAtualizado();
    }

    /// <summary>
    /// Desativa a categoria (soft delete)
    /// </summary>
    public void Desativar()
    {
        Ativo = false;
        MarcarComoAtualizado();
    }

    /// <summary>
    /// Reativa a categoria
    /// </summary>
    public void Ativar()
    {
        Ativo = true;
        MarcarComoAtualizado();
    }

    /// <summary>
    /// Validações de negócio para nome
    /// </summary>
    private static void ValidarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("Nome da categoria é obrigatório");

        if (nome.Length > 50)
            throw new DomainException("Nome da categoria deve ter no máximo 50 caracteres");
    }

    /// <summary>
    /// Validações para cor (formato hexadecimal)
    /// </summary>
    private static void ValidarCor(string? cor)
    {
        if (cor != null && !Regex.IsMatch(cor, @"^#[0-9A-Fa-f]{6}$"))
            throw new DomainException("Cor deve estar no formato hexadecimal (#RRGGBB)");
    }
}