using AutoMapper;
using MeusMedicamentos.Application.DTOs.Medicamentos;
using MeusMedicamentos.Application.DTOs.Relatorios;
using MeusMedicamentos.Domain.Entities;

namespace MeusMedicamentos.Application.Mapping;

/// <summary>
/// Profile do AutoMapper para mapeamentos relacionados a Medicamento.
/// Define como converter entre Domain entities e DTOs.
/// </summary>
public class MedicamentoProfile : Profile
{
    public MedicamentoProfile()
    {
        ConfigurarMapeamentosMedicamento();
    }

    private void ConfigurarMapeamentosMedicamento()
    {
        // Domain → DTO (para retornos de API)
        CreateMap<Medicamento, MedicamentoDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Valor))
            .ForMember(dest => dest.Dosagem, opt => opt.MapFrom(src => src.Dosagem.ToString()))
            .ForMember(dest => dest.DataValidade, opt => opt.MapFrom(src => src.DataValidade.Valor))
            .ForMember(dest => dest.QuantidadeAtual, opt => opt.MapFrom(src => src.QuantidadeAtual.Valor))
            .ForMember(dest => dest.QuantidadeMinima, opt => opt.MapFrom(src => src.QuantidadeMinima.Valor))
            .ForMember(dest => dest.LocalArmazenamento, opt => opt.MapFrom(src => src.Local.Descricao))
            .ForMember(dest => dest.CategoriaId, opt => opt.MapFrom(src => src.CategoriaId.Valor))
            .ForMember(dest => dest.CategoriaNome, opt => opt.MapFrom(src => src.Categoria != null ? src.Categoria.Nome : null))
            .ForMember(dest => dest.CategoriaCor, opt => opt.MapFrom(src => src.Categoria != null ? src.Categoria.Cor : null));

        // Domain → DTO Resumido (para listagens)
        CreateMap<Medicamento, MedicamentoResumoDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Valor))
            .ForMember(dest => dest.Dosagem, opt => opt.MapFrom(src => src.Dosagem.ToString()))
            .ForMember(dest => dest.DataValidade, opt => opt.MapFrom(src => src.DataValidade.Valor))
            .ForMember(dest => dest.QuantidadeAtual, opt => opt.MapFrom(src => src.QuantidadeAtual.Valor))
            .ForMember(dest => dest.LocalArmazenamento, opt => opt.MapFrom(src => src.Local.Descricao))
            .ForMember(dest => dest.CategoriaNome, opt => opt.MapFrom(src => src.Categoria != null ? src.Categoria.Nome : null))
            .ForMember(dest => dest.CategoriaCor, opt => opt.MapFrom(src => src.Categoria != null ? src.Categoria.Cor : null));

        // DTO de criação → parâmetros para factory method do Domain
        // Nota: Não mapeamos diretamente para Medicamento porque usa factory method
        CreateMap<CriarMedicamentoDto, CriarMedicamentoCommand>();
        CreateMap<AtualizarMedicamentoDto, AtualizarMedicamentoCommand>();

        // Para relatórios
        CreateMap<Medicamento, MedicamentoVencimentoDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Valor))
            .ForMember(dest => dest.DataValidade, opt => opt.MapFrom(src => src.DataValidade.Valor))
            .ForMember(dest => dest.DiasParaVencimento, opt => opt.MapFrom(src => src.DataValidade.DiasParaVencimento()))
            .ForMember(dest => dest.QuantidadeAtual, opt => opt.MapFrom(src => src.QuantidadeAtual.Valor))
            .ForMember(dest => dest.LocalArmazenamento, opt => opt.MapFrom(src => src.Local.Descricao))
            .ForMember(dest => dest.CategoriaNome, opt => opt.MapFrom(src => src.Categoria != null ? src.Categoria.Nome : "Sem categoria"));
    }
}