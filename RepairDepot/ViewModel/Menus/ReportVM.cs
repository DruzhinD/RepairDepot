using DatabaseAdapter.Models;
using IronXL;
using IronXL.Formatting;
using Microsoft.EntityFrameworkCore;
using RepairDepot.Model;
using RepairDepot.ViewModel.Commands;
using RepairDepot.ViewModel.DefinitionVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairDepot.ViewModel
{
    public class ReportVM : BasePageVM
    {
        public override string Name => "Создание отчета";

        #region Свойства
        public ObservableCollection<CompleteReport> Data { get => data; set { data = value; OnPropertyChanged(); } }
        ObservableCollection<CompleteReport> data;

        public DateTime Start { get => start; set { start = value; OnPropertyChanged(); } }
        DateTime start = DateTime.Now;

        public DateTime Stop { get => stop; set { stop = value; OnPropertyChanged(); } }
        DateTime stop = DateTime.Now;
        #endregion

        #region Команды

        public AsyncCommand Refresh => refresh ??= new AsyncCommand(async (obj) => await RefreshMethod());
        AsyncCommand refresh;

        /// <summary>
        /// Создать excel
        /// </summary>
        public RelayCommand CreateExcel => createExcel ??= new RelayCommand(obj => CreateExcelMethod());
        RelayCommand createExcel;

        /// <summary>
        /// Создать word
        /// </summary>
        public RelayCommand CreateWord => createWord ??= new RelayCommand(obj => CreateWordMethod());
        RelayCommand createWord;
        #endregion

        public async override Task Initialize()
        {
            using var db = new RepairDepotContext(Config.GetInstanse().DbContextOptions);

            var reports = await db.CompleteReports
                .Include(x => x.RepairTask)
                .ThenInclude(x => x.RepairOrder)
                .ThenInclude(x => x.RepairRequest)
                .ThenInclude(x => x.Wagon)
                .Include(x => x.RepairTask)
                .ThenInclude(x => x.Foreman)
                .ThenInclude(x => x.Employee)
                .ToListAsync();
            Data = new ObservableCollection<CompleteReport>(reports);
        }

        private async Task RefreshMethod()
        {
            using var db = new RepairDepotContext(Config.GetInstanse().DbContextOptions);

            var reports = await db.CompleteReports
                .Where(x => x.DateStartFact > DateOnly.FromDateTime(Start) && x.DateStopFact < DateOnly.FromDateTime(Stop))
                .Include(x => x.RepairTask)
                .ThenInclude(x => x.RepairOrder)
                .ThenInclude(x => x.RepairRequest)
                .ThenInclude(x => x.Wagon)
                .Include(x => x.RepairTask)
                .ThenInclude(x => x.Foreman)
                .ThenInclude(x => x.Employee)
                .ToListAsync();
            Data = new ObservableCollection<CompleteReport>(reports);
        }



        private void CreateExcelMethod()
        {
            //создание таблицы DataTable для дальнейшего экспорта в Excel
            DataTable table = new DataTable("Отчет");
            table.Columns.Add("Дата начала (план)", typeof(DateOnly));
            table.Columns.Add("Дата начала (факт)", typeof(DateOnly));
            table.Columns.Add("Дата завершения (план)", typeof(DateOnly));
            table.Columns.Add("Дата завершения (факт)", typeof(DateOnly));
            table.Columns.Add("Стоимость", typeof(decimal));
            table.Columns.Add("ФИО бригадира", typeof(string));
            table.Columns.Add("Рег. номер вагона", typeof(long));
            //заполнение данными
            foreach (CompleteReport report in Data)
            {
                object[] importData = [
                    report.RepairTask.RepairOrder.DateStart,
                    report.DateStartFact,
                    report.RepairTask.RepairOrder.DateStop,
                    report.DateStopFact,
                report.RepairTask.RepairOrder.Money,
                report.RepairTask.Foreman.Employee.Name,
                report.RepairTask.RepairOrder.RepairRequest.Wagon.RegNumber];
                table.Rows.Add(importData);
            }

            //создание книги
            var workBook = new WorkBook();
            WorkSheet ws = workBook.DefaultWorkSheet; //лист по умолчанию
            string workBookName = $"Отчет за {DateTime.Now.ToString("dd.MM.yy H`m`s")}.xlsx";
            string savePath = Path.Combine(Config.GetInstanse().SavePath, workBookName);

            int rowIndexer = 0; //индекс строки, указывает на текущую пустую строку в Excel

            //записываем заголовок таблицы
            string tableHeader = string.Format("Выполненные работы за период {0} - {1}",
                arg0: Start.ToString("d"),
                arg1: Stop.ToString("d"));
            ws[$"A{1 + rowIndexer++}"].Value = tableHeader;
            ws.Merge("A1:G1"); //объединяем ячейки

            string alphabet = "ABCDEFGHIJKLMNOPQRSTUV"; //алфавит
            string tableStartIndex = $"{alphabet[0]}{1 + rowIndexer}";
            //заголовки столбцов таблицы
            for (int i = 0; i < table.Columns.Count; i++)
                ws[$"{alphabet[i]}{1 + rowIndexer}"].Value = table.Columns[i].ColumnName;

            //заполняем таблицу данными
            foreach (DataRow row in table.Rows)
            {
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    string cellIndex = $"{alphabet[j]}{1 + rowIndexer}"; //текущий индекс ячейки
                    var rowValue = row[j]; //значение из таблицы
                    if (rowValue.GetType() == typeof(DateOnly))
                    {
                        ws[cellIndex].DateTimeValue = ((DateOnly)rowValue).ToDateTime(TimeOnly.MinValue);
                    }
                    else
                    {
                        ws[cellIndex].Value = rowValue;
                        //ws[cellIndex].
                    }

                }
                rowIndexer++;
            }
            string tableStopIndex = $"{alphabet[table.Columns.Count - 1]}{1 + rowIndexer - 1}";

            //авторазмер столбцов
            for (int i = 0; i < ws.ColumnCount; i++)
                ws.AutoSizeColumn(i);

            //форматируем таблицу
            //IronXL.Range tableRange = ws[tableStartIndex + ":" + tableStopIndex];
            //var tableStyle = IronXL.Styles.TableStyle.TableStyleDark1;
            //ws.AddNamedTable("table1", tableRange, tableStyle: tableStyle);

            //сохранение
            workBook.SaveAs(savePath);
            //открываем документ
            Process.Start(new ProcessStartInfo(savePath)
            { UseShellExecute = true });
            return;
        }



        private void CreateWordMethod()
        {
            throw new NotImplementedException();
        }
    }
}
