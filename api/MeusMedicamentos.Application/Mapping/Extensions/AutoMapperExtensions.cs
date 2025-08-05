using AutoMapper;

namespace MeusMedicamentos.Application.Mapping.Extensions;

/// <summary>
/// Extensions para facilitar uso do AutoMapper
/// </summary>
public static class AutoMapperExtensions
{
    /// <summary>
    /// Mapeia com contexto personalizado
    /// </summary>
    public static TDestination Map<TDestination>(this IMapper mapper, object source, Action<IMappingOperationOptions> opts)
    {
        return mapper.Map<TDestination>(source, opts);
    }

    /// <summary>
    /// Mapeia incluindo categoria
    /// </summary>
    public static TDestination MapIncludingCategoria<TDestination>(this IMapper mapper, object source)
    {
        return mapper.Map<TDestination>(source, opt => opt.Items.Add("IncluirCategoria", true));
    }

    /// <summary>
    /// Mapeia para usuário com permissões específicas
    /// </summary>
    public static TDestination MapForUser<TDestination>(this IMapper mapper, object source, bool temPermissaoDetalhes)
    {
        return mapper.Map<TDestination>(source, opt => opt.Items.Add("UsuarioTemPermissaoDetalhes", temPermissaoDetalhes));
    }
}