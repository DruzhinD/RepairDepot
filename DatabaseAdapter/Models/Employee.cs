﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DatabaseAdapter.Models;

/// <summary>
/// Сотрудник
/// </summary>
public partial class Employee : IdModel
{

    public int Experience { get; set; }

    public string Name { get; set; }

    public string BankCard { get; set; }

    public string Education { get; set; }

    public string Specialization { get; set; }

    public virtual Foreman Foreman { get; set; }

    public virtual Worker Worker { get; set; }

    public virtual ICollection<RepairTask> RepairTasks { get; set; } = new List<RepairTask>();
}