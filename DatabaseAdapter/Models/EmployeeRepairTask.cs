﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DatabaseAdapter.Models;

public partial class EmployeeRepairTask : IdModel
{
    [DisplayName("ID работника")]
    public int EmployeeId { get; set; }

    [DisplayName("ID задания")]
    public int RepairTaskId { get; set; }

    [Browsable(false)]
    public virtual Employee Employee { get; set; }

    [Browsable(false)]
    public virtual RepairTask RepairTask { get; set; }
}