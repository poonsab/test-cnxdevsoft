using System;
using System.Collections.Generic;

namespace testing.AddModels;

public partial class User
{
    public int Id { get; set; }

    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public bool? Active { get; set; }

    public string? Address { get; set; }

    public int UserTypeId { get; set; }

    public virtual UserType UserType { get; set; } = null!;
}
