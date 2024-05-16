﻿using Tech.Domain.Enums;
using Tech.Domain.Helpers.Commons;

namespace Tech.Domain.Entities;

public class User : AudiTable
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public Roles UserRole { get; set; }
}