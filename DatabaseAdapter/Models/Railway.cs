﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DatabaseAdapter.Models;

public partial class Railway : IdModel
{

    public bool External { get; set; }

    public long? Inn { get; set; }

    public string Bank { get; set; }

    public string BusinessAddress { get; set; }

    public virtual ICollection<Wagon> Wagons { get; set; } = new List<Wagon>();
}