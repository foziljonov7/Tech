using System.Text.Json.Serialization;

namespace Tech.DAL.DTOs.PaymentDTOs;

public class PaymentForCourseDto
{
    [JsonPropertyName("student_id")]
    public long StudentId { get; set; }
    [JsonPropertyName("course_id")]
    public long CourseId { get; set; }
    [JsonPropertyName("registry_id")]
    public long RegistryId { get; set; }
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }
}
