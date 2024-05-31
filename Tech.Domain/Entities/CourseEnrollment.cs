using Tech.Domain.Helpers.Commons;

namespace Tech.Domain.Entities;

public class CourseEnrollment : AudiTable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long CourseId { get; set; }
    public Course Course { get; set; }
}
