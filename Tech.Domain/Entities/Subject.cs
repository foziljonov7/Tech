using Tech.Domain.Helpers.Commons;

namespace Tech.Domain.Entities;

public class Subject : AudiTable
{
    public string Name { get; set; }
    public long CategoryId { get; set; }
    public Category Category { get; set; }
}
