using Tech.DAL.DTOs.AttendanceDTOs;

namespace Tech.Services.Interfaces.Attendaces;

public interface IAttendance
{
    Task<bool> MarkAttendanceAsync(long courseId, List<long> studentIds, CancellationToken cancellation = default);
    Task<IEnumerable<AttendanceDto>> RetreiveByUserAsync(long userId, CancellationToken cancellation = default);
    Task<AttendanceDto> ModifyAsync(long id, AttendanceForUpdateDto dto, CancellationToken cancellation = default);
}
