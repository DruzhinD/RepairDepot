﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DatabaseAdapter.Models;

/// <summary>
/// Бригадир
/// </summary>
public partial class Foreman : IdModel
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public virtual Employee Employee { get; set; }
}