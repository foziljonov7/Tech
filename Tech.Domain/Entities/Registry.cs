using Tech.Domain.Helpers.Commons;

namespace Tech.Domain.Entities;

public class Registry : AudiTable
{
    public double Debit { get; set; }
    public double Credit { get; set; }
}
