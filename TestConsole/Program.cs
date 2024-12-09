using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using RepairDepot.Model;
using System;
using System.Reflection;

public class Паспорт
{
    public string Номер { get; set; }
    public string Серия { get; set; }
}

public class Человек
{
    public string Фамилия { get; set; }
    public string Имя { get; set; }
    public string Отчество { get; set; }
    public Паспорт Паспорт { get; set; }
}

class Program
{
    static void Main()
    {
        using var db = new RepairDepotContext(Config.GetInstanse().DbContextOptions);
        var data = db.Foremen
            .Include(x => x.Workers)
            .ThenInclude(x => x.Employee)
            .ToList();
        return;
    }
}
