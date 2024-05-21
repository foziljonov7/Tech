namespace Tech.Services.Interfaces.Courses;

public interface ICourseEnrollment
{
	Task<bool> AddStudentByCourseAsync(long id, List<long> students, CancellationToken cancellation = default);
}
