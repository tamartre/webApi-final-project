﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Password { get; set; } = null!;

    [EmailAddress]
    public string? Email { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
