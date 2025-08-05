using AutoMapper;
using MeusMedicamentos.Application.DTOs.Categorias;
using MeusMedicamentos.Domain.Entities;

namespace MeusMedicamentos.Application.Mapping;

/// <summary>
/// Profile do AutoMapper para mapeamentos de Categoria
/// </summary>
public class CategoriaProfile : Profile
{
    public CategoriaProfile()
    {
        // Domain → DTO
        CreateMap<Categoria, CategoriaDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Valor));

        // DTO → Command
        CreateMap<CriarCategoriaDto, CriarCategoriaCommand>();
    }
}
