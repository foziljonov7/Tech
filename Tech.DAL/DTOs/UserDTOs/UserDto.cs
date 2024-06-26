﻿using System.Text.Json.Serialization;
using Tech.Domain.Entities;
using Tech.Domain.Enums.Users;

namespace Tech.DAL.DTOs.UserDTOs;

public class UserDto
{
	[JsonPropertyName("id")]
    public long Id { get; set; }
	[JsonPropertyName("firstname")]
    public string Firstname { get; set; }
	[JsonPropertyName("lastname")]
	public string Lastname { get; set; }
	[JsonPropertyName("phone_number")]
	public string PhoneNumber { get; set; }
	[JsonPropertyName("password")]
	public string Password { get; set; }
	[JsonPropertyName("role")]
	public Roles Role { get; set; }
	[JsonPropertyName("courses")]
    public ICollection<CourseEnrollment> Courses { get; set; }
	[JsonPropertyName("attendaces")]
    public ICollection<Attendance> Attendaces { get; set; }
}
