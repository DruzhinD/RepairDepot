﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DatabaseAdapter.Models;

public partial class UserLog : IdModel
{
    public int UserId { get; set; }

    public string Message { get; set; }

    public virtual User User { get; set; }
}