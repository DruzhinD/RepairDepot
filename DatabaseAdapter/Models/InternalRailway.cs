﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DatabaseAdapter.Models;

/// <summary>
/// Внутренняя ЖД
/// </summary>
public partial class InternalRailway
{
    public int Id { get; set; }

    public int RailwayId { get; set; }

    public virtual Railway Railway { get; set; }
}