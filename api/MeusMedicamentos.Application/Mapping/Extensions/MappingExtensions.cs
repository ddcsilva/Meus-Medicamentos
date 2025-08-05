using AutoMapper;
using MeusMedicamentos.Application.DTOs.Common;

namespace MeusMedicamentos.Application.Mapping.Extensions;

/// <summary>
/// Extensions para facilitar mapeamentos comuns
/// </summary>
public static class MappingExtensions
{
    /// <summary>
    /// Converte IEnumerable para resultado paginado
    /// </summary>
    public static PaginatedResultDto<TDto> ToPaginatedResult<TEntity, TDto>(
        this IEnumerable<TEntity> source,
        IMapper mapper,
        int totalItens,
        int pagina,
        int itensPorPagina)
    {
        var items = mapper.Map<IEnumerable<TDto>>(source);

        return new PaginatedResultDto<TDto>
        {
            Items = items,
            TotalItens = totalItens,
            Pagina = pagina,
            ItensPorPagina = itensPorPagina
        };
    }

    /// <summary>
    /// Mapeia Domain Entity para diferentes tipos de DTO baseado em contexto
    /// </summary>
    public static TDto MapToDto<TEntity, TDto>(
        this TEntity entity,
        IMapper mapper,
        bool incluirDetalhes = true)
        where TEntity : class
        where TDto : class
    {
        return mapper.Map<TDto>(entity);
    }
}