using AutoMapper;
using Tech.Domain.Entities;
using Tech.Domain.Enums.Users;
using Tech.Infrastructure.Interfaces;
using Tech.Services.Commons.Exceptions;
using Tech.Services.Interfaces.Courses;

namespace Tech.Services.Services.Courses;

public class CourseEnrollmentService(
	IRepository<CourseEnrollment> repository,
	IRepository<User> userRepository,
	IRepository<Course> courseRepository,
	IMapper mapper) : ICourseEnrollment
{
	public async Task<bool> AddStudentByCourseAsync(long id, List<long> students, CancellationToken cancellation = default)
	{
		try
		{
			var course = await courseRepository.SelectAsync(x => x.Id == id, null, cancellation);

			if (course is null)
				return false;

			var courseStudents = await userRepository
				.SelectAllAsync(x => students.Contains(x.Id) && x.UserRole == Roles.User, null, cancellation);

			if (courseStudents is null || !courseStudents.Any())
				throw new CustomException(404, "Course students not found!");

			foreach (var student in courseStudents)
			{
				var enrollment = new CourseEnrollment
				{
					CourseId = course.Id,
					UserId = student.Id
				};

				await repository.AddAsync(enrollment, cancellation);
			}

			await repository.SaveAsync(cancellation);
			return true;
		}
		catch (Exception ex)
		{
			throw new CustomException(404, "Course not found" + ex.InnerException.Message);
		}
	}
}
