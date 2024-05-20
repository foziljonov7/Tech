using System.Text.Json.Serialization;
using Tech.Domain.Entities;
using Tech.Domain.Enums.Courses;

namespace Tech.DAL.DTOs.CourseDTOs;

public class CourseDto
{
	[JsonPropertyName("id")]
    public long Id { get; set; }
	[JsonPropertyName("subject_id")]
    public long SubjectId { get; set; }
	[JsonPropertyName("teacher_id")]
	public long TeacherId { get; set; }
	[JsonPropertyName("starting_date")]
	public DateTimeOffset? StartingDate { get; set; }
	[JsonPropertyName("price")]
	public double Price { get; set; }
	[JsonPropertyName("status")]
	public Status Status { get; set; }
	[JsonPropertyName("students")]
	public ICollection<CourseEnrollment> Students { get; set; }
}
