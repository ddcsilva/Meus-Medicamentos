using AutoMapper;
using MeusMedicamentos.Application.DTOs.Dashboard;
using MeusMedicamentos.Domain.Services.Models;

namespace MeusMedicamentos.Application.Mapping;

/// <summary>
/// Profile para mapeamentos do Dashboard
/// </summary>
public class DashboardProfile : Profile
{
    public DashboardProfile()
    {
        // Domain Service result → DTO
        CreateMap<MetricasSistema, EstatisticasGerais>()
            .ForMember(dest => dest.UltimaAtualizacao, opt => opt.MapFrom(src => DateTime.UtcNow));
    }
}