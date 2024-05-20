﻿using System.Text.Json.Serialization;
using Tech.Domain.Enums.Courses;

namespace Tech.DAL.DTOs.CourseDTOs;

public class CourseForCreateDto
{
	public long SubjectId { get; set; }
	[JsonPropertyName("teacher_id")]
	public long TeacherId { get; set; }
	[JsonPropertyName("starting_date")]
	public DateTimeOffset? StartingDate { get; set; }
	[JsonPropertyName("price")]
	public double Price { get; set; }
}
