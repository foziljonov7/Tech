using AutoMapper;
using Tech.DAL.DbContexts;
using Tech.DAL.DTOs.RegistryDTOs;
using Tech.Domain.Entities;
using Tech.Infrastructure.Interfaces;
using Tech.Services.Interfaces.Payments;

namespace Tech.Services.Services.Payments;

public class RegistryService(
    IRepository<Registry> repository,
    IMapper mapper) : IRegistry
{
    public async Task<Registry> PostRegistryAsync(RegistryDto dto, CancellationToken cancellation = default)
    {
        var mapped = mapper.Map<Registry>(dto);
        await repository.AddAsync(mapped, cancellation);
        await repository.SaveAsync(cancellation);
        
        return mapped;
    }
}
