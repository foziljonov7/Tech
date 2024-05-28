using AutoMapper;
using Tech.DAL.DTOs.AttendanceDTOs;
using Tech.Domain.Entities;
using Tech.Infrastructure.Interfaces;
using Tech.Services.Interfaces.Generics;

namespace Tech.Services.Services.Attendances;

public class AttendanceService(
    IRepository<Attendance> repository,
    IMapper mapper) : IGettable<AttendanceDto>
{
    public Task<IEnumerable<AttendanceDto>> RetreiveAllAsync(CancellationToken cancellation = default)
    {
        throw new NotImplementedException();
    }

    public Task<AttendanceDto> RetreiveByIdAsync(long id, CancellationToken cancellation = default)
    {
        throw new NotImplementedException();
    }
}
