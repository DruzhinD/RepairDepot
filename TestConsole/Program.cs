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
        // Создаем экземпляр класса Человек с заполненными данными
        Человек человек = new Человек
        {
            Фамилия = "Иванов",
            Имя = "Иван",
            Отчество = "Иванович",
            Паспорт = new Паспорт { Номер = "123456", Серия = "AB" }
        };

        // Получаем тип класса Человек
        Type человекType = typeof(Человек);

        // Получаем свойство Паспорт
        PropertyInfo паспортProperty = человекType.GetProperty("Паспорт");

        // Получаем значение свойства Паспорт
        Паспорт паспорт = (Паспорт)паспортProperty.GetValue(человек);

        // Получаем тип класса Паспорт
        Type паспортType = typeof(Паспорт);

        // Получаем вложенные свойства класса Паспорт
        PropertyInfo[] вложенныеСвойства = паспортType.GetProperties();

        // Выводим значения вложенных свойств
        foreach (var свойство in вложенныеСвойства)
        {
            var значение = свойство.GetValue(паспорт);
            Console.WriteLine($"{свойство.Name}: {значение}");
        }
    }
}
