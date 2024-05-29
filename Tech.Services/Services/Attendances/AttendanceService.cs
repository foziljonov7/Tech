using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tech.DAL.DTOs.AttendanceDTOs;
using Tech.DAL.DTOs.CourseDTOs;
using Tech.DAL.DTOs.UserDTOs;
using Tech.Domain.Entities;
using Tech.Infrastructure.Interfaces;
using Tech.Services.Commons.Exceptions;
using Tech.Services.Helpers;
using Tech.Services.Interfaces.Attendaces;
using Tech.Services.Interfaces.Generics;

namespace Tech.Services.Services.Attendances;

public class AttendanceService(
    IRepository<Attendance> repository,
    IGettable<CourseDto> courseService,
    IRepository<User> userRepository,
    IMapper mapper) : IGettable<AttendanceDto>, IAttendance
{
    public async Task<bool> MarkAttendanceAsync(long courseId, List<long> studentIds, CancellationToken cancellation = default)
    {
        var course = await courseService.RetreiveByIdAsync(courseId, cancellation);

        if (course is null)
            throw new CustomException(404, "Attendace, course not found!");

        var students = await userRepository.SelectAllAsync(x => studentIds.Contains(x.Id), null, cancellation);

        if (students is null || !students.Any())
            throw new CustomException(404, "Attendance, students not found!");

        var attendaces = students.Select(s => new Attendance
        {
            UserId = s.Id,
            CourseId = courseId,
            Date = TimeHelper.GetServerTime(),
            IsActive = true
        }).ToList();

        foreach(var attendace in attendaces)
            await repository.AddAsync(attendace, cancellation);

        await repository.SaveAsync(cancellation);

        return true;
    }

    public async Task<AttendanceDto> ModifyAsync(long id, AttendanceForUpdateDto dto, CancellationToken cancellation = default)
    {
        if (!await repository.ExistAsync(id, cancellation))
            throw new CustomException(404, "Attendance not found!");

        var mapped = mapper.Map<Attendance>(dto);
        mapped.Id = id;

        await repository.UpdateAsync(mapped, cancellation);
        await repository.SaveAsync(cancellation);

        return mapper.Map<AttendanceDto>(mapped);
    }

    public async Task<IEnumerable<AttendanceDto>> RetreiveAllAsync(CancellationToken cancellation = default)
    {
        var attendaces = await repository.SelectAllAsync(null, null, cancellation);

        if (attendaces is null)
            throw new CustomException(404, "Attendance not found");

        return mapper.Map<IEnumerable<AttendanceDto>>(attendaces);
    }

    public async Task<AttendanceDto> RetreiveByIdAsync(long id, CancellationToken cancellation = default)
    {
        var attendace = await repository.SelectAsync(x => x.Id == id, null, cancellation);
        if (attendace is null)
            throw new CustomException(404, "Attendance not found!");

        return mapper.Map<AttendanceDto>(attendace);
    }

    public async Task<IEnumerable<AttendanceDto>> RetreiveByUserAsync(long userId, CancellationToken cancellation = default)
    {
        var studentAttendance = await repository.SelectAllAsync(x => x.UserId == userId, null, cancellation);

        if (studentAttendance is null)
            throw new CustomException(404, "Attendance not found!");

        return mapper.Map<IEnumerable<AttendanceDto>>(studentAttendance);
    }
}
