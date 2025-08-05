using AutoMapper;
using MeusMedicamentos.Application.DTOs.Medicamentos;
using MeusMedicamentos.Domain.Entities;

namespace MeusMedicamentos.Application.Mapping;

/// <summary>
/// Profile com mapeamentos condicionais baseados no contexto.
/// Útil para quando o mesmo Entity precisa ser mapeado diferente dependendo da situação.
/// </summary>
public class ConditionalMappingProfile : Profile
{
    public ConditionalMappingProfile()
    {
        // Mapeamento condicional: incluir ou não dados da categoria
        CreateMap<Medicamento, MedicamentoDto>()
            .ForMember(dest => dest.CategoriaNome, opt =>
                opt.MapFrom((src, dest, destMember, context) =>
                {
                    // Se contexto especifica incluir categoria
                    if (context.Items.ContainsKey("IncluirCategoria") &&
                        (bool)context.Items["IncluirCategoria"])
                    {
                        return src.Categoria?.Nome;
                    }
                    return null;
                }));

        // Mapeamento baseado em permissões do usuário
        CreateMap<Medicamento, MedicamentoDto>()
            .ForMember(dest => dest.Lote, opt =>
                opt.MapFrom((src, dest, destMember, context) =>
                {
                    // Se usuário tem permissão para ver detalhes técnicos
                    if (context.Items.ContainsKey("UsuarioTemPermissaoDetalhes") &&
                        (bool)context.Items["UsuarioTemPermissaoDetalhes"])
                    {
                        return src.Lote;
                    }
                    return null;
                }));
    }
}