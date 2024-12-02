using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.IO;
using System.Text.Json;

#nullable disable
namespace RepairDepot.Model;

/// <summary>
/// является классом конфигурации
/// </summary>
public class Config
{
    #region Singleton
    private Config()
    {
        this.Initialize();
    }

    public static Config GetInstanse()
    {
        if (instanse == null)
            return new Config();
        return Config.instanse;
    }
    #endregion

    DbContextOptions<RepairDepotContext> dbContextOptions;
    public DbContextOptions<RepairDepotContext> DbContextOptions
        => dbContextOptions ??= new DbContextOptionsBuilder<RepairDepotContext>().UseNpgsql(this["connectionString"]).Options;

    /// <summary>
    /// путь к логотипу программы, отображаемый в форме приветствия
    /// </summary>
    public string LogoPath
    {
        get
        {
            var s = Path.Combine(Directory.GetCurrentDirectory(), this["logo_path"]);
            return s;
        }
    }

    /// <summary>
    /// индексатор конфигуратора
    /// </summary>
    /// <param name="key">название параметра</param>
    /// <returns>значение параметра</returns>
    public string this[string key]
    {
        get => data[key];
    }
    private static Config instanse;
    /// <summary>
    /// хранит информацию из файла конфигурации
    /// </summary>
    public Dictionary<string, string>? data;
    private string configPath = Path.Combine(
        Directory.GetCurrentDirectory(), @"..\..\..\", "data\\config.json");

    //инициализация класса конфигурации
    private void Initialize()
    {
        this.data = Config.Deserialize(this.configPath);
    }

    //чтение данных из файла конфигурации
    private static Dictionary<string, string> Deserialize(string configPath)
    {
        Dictionary<string, string>? data;
        using (Stream file = new FileStream(configPath, FileMode.Open))
        {
            data = JsonSerializer.Deserialize<Dictionary<string, string>>(file);
        }
        //string json = JsonSerializer.Deserialize()
        return data;
    }

}
