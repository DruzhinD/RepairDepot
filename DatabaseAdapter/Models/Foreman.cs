﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DatabaseAdapter.Models;

/// <summary>
/// Бригадир
/// </summary>
public partial class Foreman : IdModel
{
    [DisplayName("ID работника")]
    public int EmployeeId { get; set; }

    [Browsable(false)]
    public virtual Employee Employee { get; set; }
}