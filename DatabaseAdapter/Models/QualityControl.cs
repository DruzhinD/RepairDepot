﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DatabaseAdapter.Models;

/// <summary>
/// Акт контроля качества
/// </summary>
public partial class QualityControl : IdModel
{
    [DisplayName("ID отчета")]
    public int CompleteReportId { get; set; }

    [DisplayName("Качество")]
    public bool Quality { get; set; }

    [DisplayName("Комментарий")]
    public string Comment { get; set; }

    [Browsable(false)]
    public virtual AwardOrder AwardOrder { get; set; }

    [Browsable(false)]
    public virtual CompleteReport CompleteReport { get; set; }
}