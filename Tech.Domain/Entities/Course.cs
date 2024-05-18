using System.Collections.ObjectModel;
using Tech.Domain.Enums.Courses;
using Tech.Domain.Helpers.Commons;

namespace Tech.Domain.Entities;

public class Course : AudiTable
{
    public long SubjectId { get; set; }
    public Subject Subject { get; set; }
    public long TeacherId { get; set; }
    public User Teacher { get; set; }
    public DateTimeOffset? StartingDate { get; set; }  
    public double Price { get; set; }
    public Roles Status { get; set; }
    public Collection<User> Students { get; set; } = new Collection<User>();
}
