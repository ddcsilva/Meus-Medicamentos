using AutoMapper;
using MeusMedicamentos.Application.DTOs.Common;

namespace MeusMedicamentos.Application.Mapping;

/// <summary>
/// Service para mapeamentos complexos que precisam de lógica adicional.
/// Usado quando AutoMapper sozinho não é suficiente.
/// </summary>
public interface IMappingService
{
    /// <summary>
    /// Mapeia entidade com estatísticas calculadas
    /// </summary>
    Task<TDto> MapWithStatsAsync<TEntity, TDto>(TEntity entity)
        where TEntity : class
        where TDto : class;

    /// <summary>
    /// Mapeia coleção para resultado paginado com contagem total
    /// </summary>
    Task<PaginatedResultDto<TDto>> MapToPaginatedAsync<TEntity, TDto>(
        IEnumerable<TEntity> entities,
        Func<Task<int>> totalCountFunc,
        int pagina,
        int itensPorPagina)
        where TEntity : class
        where TDto : class;
}

public class MappingService : IMappingService
{
    private readonly IMapper _mapper;

    public MappingService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<TDto> MapWithStatsAsync<TEntity, TDto>(TEntity entity)
        where TEntity : class
        where TDto : class
    {
        var dto = _mapper.Map<TDto>(entity);

        // Aqui podemos adicionar lógica para calcular estatísticas
        // Por exemplo, se TDto é CategoriaDto, calcular TotalMedicamentos

        return dto;
    }

    public async Task<PaginatedResultDto<TDto>> MapToPaginatedAsync<TEntity, TDto>(
        IEnumerable<TEntity> entities,
        Func<Task<int>> totalCountFunc,
        int pagina,
        int itensPorPagina)
        where TEntity : class
        where TDto : class
    {
        var items = _mapper.Map<IEnumerable<TDto>>(entities);
        var totalItens = await totalCountFunc();

        return new PaginatedResultDto<TDto>
        {
            Items = items,
            TotalItens = totalItens,
            Pagina = pagina,
            ItensPorPagina = itensPorPagina
        };
    }
}