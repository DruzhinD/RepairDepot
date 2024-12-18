﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAdapter.Models;

public partial class RepairDepotContext : DbContext
{
    public RepairDepotContext(DbContextOptions<RepairDepotContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AwardOrder> AwardOrders { get; set; }

    public virtual DbSet<CompleteReport> CompleteReports { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Foreman> Foremen { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<QualityControl> QualityControls { get; set; }

    public virtual DbSet<Railway> Railways { get; set; }

    public virtual DbSet<RepairOrder> RepairOrders { get; set; }

    public virtual DbSet<RepairRequest> RepairRequests { get; set; }

    public virtual DbSet<RepairTask> RepairTasks { get; set; }

    public virtual DbSet<RepairType> RepairTypes { get; set; }

    public virtual DbSet<ServiceDirectorate> ServiceDirectorates { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserLog> UserLogs { get; set; }

    public virtual DbSet<Wagon> Wagons { get; set; }

    public virtual DbSet<WagonType> WagonTypes { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AwardOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AwardOrder_pkey");

            entity.ToTable("AwardOrder", tb => tb.HasComment("Приказ о начислении премии"));

            entity.HasIndex(e => e.QualityControlId, "AwardOrder_quality_control_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Bonus)
                .HasColumnType("money")
                .HasColumnName("bonus");
            entity.Property(e => e.BonusPercent).HasColumnName("bonus_percent");
            entity.Property(e => e.QualityControlId).HasColumnName("quality_control_id");

            entity.HasOne(d => d.QualityControl).WithOne(p => p.AwardOrder)
                .HasForeignKey<AwardOrder>(d => d.QualityControlId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_award_order_quality_control");
        });

        modelBuilder.Entity<CompleteReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CompleteReport_pkey");

            entity.ToTable("CompleteReport", tb => tb.HasComment("Отчет о выполнении работ"));

            entity.HasIndex(e => e.RepairTaskId, "CompleteReport_repair_task_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateStartFact).HasColumnName("date_start_fact");
            entity.Property(e => e.DateStopFact).HasColumnName("date_stop_fact");
            entity.Property(e => e.RepairTaskId).HasColumnName("repair_task_id");

            entity.HasOne(d => d.RepairTask).WithOne(p => p.CompleteReport)
                .HasForeignKey<CompleteReport>(d => d.RepairTaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_complete_report_repair_task");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Employee_pkey");

            entity.ToTable("Employee", tb => tb.HasComment("Сотрудник"));

            entity.Property(e => e.BankCard)
                .IsRequired()
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("bank_card");
            entity.Property(e => e.Education)
                .HasMaxLength(100)
                .HasColumnName("education");
            entity.Property(e => e.Experience).HasColumnName("experience");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(80)
                .HasColumnName("name");
            entity.Property(e => e.Specialization)
                .HasMaxLength(100)
                .HasColumnName("specialization");

            entity.HasMany(d => d.RepairTasks).WithMany(p => p.Employees)
                .UsingEntity<Dictionary<string, object>>(
                    "EmployeeRepairTask",
                    r => r.HasOne<RepairTask>().WithMany()
                        .HasForeignKey("RepairTaskId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Employee_RepairTask_RepairTask_Id_fkey"),
                    l => l.HasOne<Employee>().WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Employee_RepairTask_Employee_Id_fkey"),
                    j =>
                    {
                        j.HasKey("EmployeeId", "RepairTaskId").HasName("Employee_RepairTask_pkey");
                        j.ToTable("Employee_RepairTask");
                        j.IndexerProperty<int>("EmployeeId")
                            .ValueGeneratedOnAdd()
                            .HasColumnName("Employee_Id");
                        j.IndexerProperty<int>("RepairTaskId")
                            .ValueGeneratedOnAdd()
                            .HasColumnName("RepairTask_Id");
                    });
        });

        modelBuilder.Entity<Foreman>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Foreman_pkey");

            entity.ToTable("Foreman", tb => tb.HasComment("Бригадир"));

            entity.HasIndex(e => e.EmployeeId, "Foreman_employee_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

            entity.HasOne(d => d.Employee).WithOne(p => p.Foreman)
                .HasForeignKey<Foreman>(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_foreman_employee");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("permission_pkey");

            entity.ToTable("permission");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Admin)
                .HasDefaultValue(false)
                .HasColumnName("admin");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.PlaningDepartment)
                .HasDefaultValue(false)
                .HasColumnName("planing_department");
            entity.Property(e => e.RepairDepartment)
                .HasDefaultValue(false)
                .HasColumnName("repair_department");
            entity.Property(e => e.StaffDepartment)
                .HasDefaultValue(false)
                .HasColumnName("staff_department");
            entity.Property(e => e.TechnicalDepartment)
                .HasDefaultValue(false)
                .HasColumnName("technical_department");
        });

        modelBuilder.Entity<QualityControl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("QualityControl_pkey");

            entity.ToTable("QualityControl", tb => tb.HasComment("Акт контроля качества"));

            entity.HasIndex(e => e.CompleteReportId, "QualityControl_complete_report_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comment)
                .HasMaxLength(100)
                .HasColumnName("comment");
            entity.Property(e => e.CompleteReportId).HasColumnName("complete_report_id");
            entity.Property(e => e.Quality).HasColumnName("quality");

            entity.HasOne(d => d.CompleteReport).WithOne(p => p.QualityControl)
                .HasForeignKey<QualityControl>(d => d.CompleteReportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_quality_control_complete_report");
        });

        modelBuilder.Entity<Railway>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Railway_pkey");

            entity.ToTable("Railway");

            entity.Property(e => e.Bank)
                .HasMaxLength(60)
                .HasColumnName("bank");
            entity.Property(e => e.BusinessAddress)
                .HasMaxLength(80)
                .HasColumnName("business_address");
            entity.Property(e => e.External).HasDefaultValue(false);
            entity.Property(e => e.Inn).HasColumnName("inn");
        });

        modelBuilder.Entity<RepairOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RepairOrder_pkey");

            entity.ToTable("RepairOrder", tb => tb.HasComment("Наряд на ремонт"));

            entity.HasIndex(e => e.RepairRequestId, "RepairOrder_repair_request_id_key").IsUnique();

            entity.Property(e => e.DateStart).HasColumnName("date_start");
            entity.Property(e => e.DateStop).HasColumnName("date_stop");
            entity.Property(e => e.Money)
                .HasColumnType("money")
                .HasColumnName("money");
            entity.Property(e => e.RepairRequestId).HasColumnName("repair_request_id");

            entity.HasOne(d => d.RepairRequest).WithOne(p => p.RepairOrder)
                .HasForeignKey<RepairOrder>(d => d.RepairRequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_repair_order_repair_request");
        });

        modelBuilder.Entity<RepairRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RepairRequest_pkey");

            entity.ToTable("RepairRequest", tb => tb.HasComment("Запрос на ремонт"));

            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.RepairTypeId).HasColumnName("repair_type_id");
            entity.Property(e => e.WagonId).HasColumnName("wagon_id");

            entity.HasOne(d => d.RepairType).WithMany(p => p.RepairRequests)
                .HasForeignKey(d => d.RepairTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_repair_request_repair_type");

            entity.HasOne(d => d.Wagon).WithMany(p => p.RepairRequests)
                .HasForeignKey(d => d.WagonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_repair_request_wagon");
        });

        modelBuilder.Entity<RepairTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RepairTask_pkey");

            entity.ToTable("RepairTask", tb => tb.HasComment("Задание на ремонт"));

            entity.HasIndex(e => e.RepairOrderId, "RepairTask_repair_order_id_key").IsUnique();

            entity.HasIndex(e => e.RepairOrderId, "RepairTask_repair_order_id_key1").IsUnique();

            entity.Property(e => e.ForemanId).HasColumnName("foreman_id");
            entity.Property(e => e.RepairOrderId).HasColumnName("repair_order_id");

            entity.HasOne(d => d.Foreman).WithMany(p => p.RepairTasks)
                .HasForeignKey(d => d.ForemanId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_repair_task_foreman");

            entity.HasOne(d => d.RepairOrder).WithOne(p => p.RepairTask)
                .HasForeignKey<RepairTask>(d => d.RepairOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_repair_task_repair_order");
        });

        modelBuilder.Entity<RepairType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RepairType_pkey");

            entity.ToTable("RepairType", tb => tb.HasComment("Справочник тип ремонта"));

            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("type");
        });

        modelBuilder.Entity<ServiceDirectorate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("service_directorate_pkey");

            entity.ToTable("service_directorate", tb => tb.HasComment("Дирекция по обслуживанию пассажиров"));

            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.Directorate)
                .IsRequired()
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("directorate");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pkey");

            entity.ToTable("user", tb => tb.HasComment("пользователи приложения"));

            entity.HasIndex(e => e.Login, "login_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("last_name");
            entity.Property(e => e.Login)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("login");
            entity.Property(e => e.MiddleName)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("middle_name");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");

            entity.HasOne(d => d.Permission).WithMany(p => p.Users)
                .HasForeignKey(d => d.PermissionId)
                .HasConstraintName("user_permission_id_fkey");
        });

        modelBuilder.Entity<UserLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_log_pkey");

            entity.ToTable("user_log");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Message)
                .HasMaxLength(500)
                .HasColumnName("message");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_log_user");
        });

        modelBuilder.Entity<Wagon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Wagon_pkey");

            entity.ToTable("Wagon");

            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.RailwayId).HasColumnName("railway_id");
            entity.Property(e => e.RegNumber).HasColumnName("reg_number");
            entity.Property(e => e.ServiceDirectorateId).HasColumnName("service_directorate_id");
            entity.Property(e => e.WagonTypeId).HasColumnName("wagon_type_id");

            entity.HasOne(d => d.Railway).WithMany(p => p.Wagons)
                .HasForeignKey(d => d.RailwayId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_wagon_railway");

            entity.HasOne(d => d.ServiceDirectorate).WithMany(p => p.Wagons)
                .HasForeignKey(d => d.ServiceDirectorateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_wagon_service_directorate");

            entity.HasOne(d => d.WagonType).WithMany(p => p.Wagons)
                .HasForeignKey(d => d.WagonTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_wagon_wagon_type");
        });

        modelBuilder.Entity<WagonType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WagonType_pkey");

            entity.ToTable("WagonType");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Worker_pkey");

            entity.ToTable("Worker", tb => tb.HasComment("Работник (рабочий)"));

            entity.HasIndex(e => e.EmployeeId, "Worker_employee_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChiefId).HasColumnName("chief_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

            entity.HasOne(d => d.Chief).WithMany(p => p.Workers)
                .HasForeignKey(d => d.ChiefId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_worker_foreman");

            entity.HasOne(d => d.Employee).WithOne(p => p.Worker)
                .HasForeignKey<Worker>(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_worker_employee");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
    }
}