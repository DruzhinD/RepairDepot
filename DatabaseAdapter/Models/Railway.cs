﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DatabaseAdapter.Models;

public partial class Railway
{
    public int Id { get; set; }

    public bool External { get; set; }

    public virtual ICollection<ExternalRailway> ExternalRailways { get; set; } = new List<ExternalRailway>();

    public virtual InternalRailway InternalRailway { get; set; }

    public virtual ICollection<Wagon> Wagons { get; set; } = new List<Wagon>();
}