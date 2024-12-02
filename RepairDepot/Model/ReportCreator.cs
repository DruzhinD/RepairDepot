using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using RepairDepot.Model.TableManaging;

namespace RepairDepot.Model
{
    public class ReportCreator
    {
        public async Task<string> CreateReport()
        {
            List<CompleteReport> data;
            using (RepairDepotContext db = new RepairDepotContext(Config.GetInstanse().DbContextOptions))
            {
                data = await db.CompleteReports.ToListAsync();
                string path = await CreateWorkBook(data, db);
                return path;
            }

        }

        async Task<string> CreateWorkBook(List<CompleteReport> data, RepairDepotContext db)
        {
            using (XLWorkbook wbook = new XLWorkbook())
            {
                string dateformat = "dd/MM//yy";
                IXLWorksheet ws = wbook.Worksheets.Add("Отчет");

                //главный заголовок
                string h1 = "Отчет о выполнении работ.";
                IXLRange range = ws.Range("A1:H1");
                range.Merge().Value = h1;
                range.Style.Font.SetBold(true).Font.FontSize = 16;

                //заголовки
                string[] headers =
                    {
                    "ID",
                    "Дата начала (факт)",
                    "Дата завершения (факт)",
                    "Дата начала (план)",
                    "Дата завершения (план)",
                    "Стоимость работ",
                    "Регистрационный номер вагона",
                    "ФИО бригадира"
                };
                char[] chars = "ABCDEFGH".ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                {
                    ws.Cell($"{chars[i]}{2}").Value = headers[i];
                }
                //основные данные
                for (int i = 0; i < data.Count; i++)
                {

                    int j = i + 3;
                    ws.Cell($"A{j}").Value = data[i].Id;
                    ws.Cell($"B{j}").Value = data[i].DateStartFact.ToString(dateformat);
                    ws.Cell($"C{j}").Value = data[i].DateStopFact.ToString(dateformat);
                    ws.Cell($"D{j}").Value = data[i].RepairTask.RepairOrder.DateStart.ToString(dateformat);
                    ws.Cell($"E{j}").Value = data[i].RepairTask.RepairOrder.DateStop.ToString(dateformat);
                    ws.Cell($"F{j}").Value = data[i].RepairTask.RepairOrder.Money;

                    var wagon = data[i].RepairTask.RepairOrder.RepairRequest.Wagon;
                    ws.Cell($"G{j}").Style.NumberFormat.Format = "0";
                    ws.Cell($"G{j}").Value = wagon.RegNumber;

                    int foremanId = data[i].RepairTask.ForemanId;
                    Foreman foreman = await db.Foremen.FirstOrDefaultAsync(x => x.Id == foremanId);
                    ws.Cell($"H{j}").Value = foreman.Employee.Name;
                }
                ws.Columns().AdjustToContents(); //автоширина
                string filePath = "Report.xlsx";
                wbook.SaveAs(Path.Combine(Config.GetInstanse().SavePath, filePath));
                return filePath;
            }
        }
    }
}
