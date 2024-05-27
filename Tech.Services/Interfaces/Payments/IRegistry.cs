using Tech.DAL.DTOs.RegistryDTOs;
using Tech.Domain.Entities;

namespace Tech.Services.Interfaces.Payments;

public interface IRegistry
{
    Task<Registry> PostRegistryAsync(RegistryDto dto, CancellationToken cancellation = default);
}
