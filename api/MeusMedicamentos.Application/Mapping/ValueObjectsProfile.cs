using AutoMapper;
using MeusMedicamentos.Domain.ValueObjects;

namespace MeusMedicamentos.Application.Mapping;

/// <summary>
/// Profile para mapeamentos de Value Objects.
/// Value Objects geralmente são mapeados para tipos primitivos nas DTOs.
/// </summary>
public class ValueObjectsProfile : Profile
{
    public ValueObjectsProfile()
    {
        // Value Objects → Primitivos
        CreateMap<Dosagem, string>().ConvertUsing(src => src.ToString());
        CreateMap<DataValidade, DateTime>().ConvertUsing(src => src.Valor);
        CreateMap<Quantidade, int>().ConvertUsing(src => src.Valor);
        CreateMap<LocalArmazenamento, string>().ConvertUsing(src => src.Descricao);

        // Primitivos → Value Objects (usado em Commands)
        CreateMap<string, Dosagem>().ConvertUsing(src => Dosagem.Criar(src));
        CreateMap<DateTime, DataValidade>().ConvertUsing(src => DataValidade.Criar(src));
        CreateMap<int, Quantidade>().ConvertUsing(src => Quantidade.Criar(src));
        CreateMap<string, LocalArmazenamento>().ConvertUsing(src => LocalArmazenamento.Criar(src));
    }
}