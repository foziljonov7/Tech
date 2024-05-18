using Tech.Domain.Helpers.Commons;

namespace Tech.Domain.Entities;

public class Payment : AudiTable
{
    public long StudentId { get; set; }
    public long CourseId { get; set; }
    public long RegistryId { get; set; }
    public decimal Amount { get; set; }
    public DateTimeOffset PaymentDate { get; set; }
    public User Student { get; set; }
    public Course Course { get; set; }
    public Registry Registry { get; set; }
}
