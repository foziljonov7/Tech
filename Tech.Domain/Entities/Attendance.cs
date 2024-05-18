using Tech.Domain.Helpers.Commons;

namespace Tech.Domain.Entities;

public class Attendance : AudiTable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public Course Course { get; set; }
    public long CourseId { get; set; }
    public DateTime Date { get; set; }
    public bool IsActive { get; set; }
}
