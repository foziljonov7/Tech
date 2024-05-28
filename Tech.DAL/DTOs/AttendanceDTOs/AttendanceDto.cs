using System.Text.Json.Serialization;

namespace Tech.DAL.DTOs.AttendanceDTOs;

public class AttendanceDto
{
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }
    [JsonPropertyName("course_id")]
    public long CourseId { get; set; }
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }
    [JsonPropertyName("is_active")]
    public bool IsActive { get; set; }
}
