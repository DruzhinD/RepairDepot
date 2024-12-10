using DatabaseAdapter.Models;
using IronXL;
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
            DataTable table = new DataTable();
            table.TableName = "Отчет";
            var column1 = new DataColumn("Дата начала (план)", typeof(string));
            table.Columns.Add(column1);
            var column2 = new DataColumn("Дата начала (факт)", typeof(string));
            table.Columns.Add(column2);
            var column3 = new DataColumn("Дата завершения (план)", typeof(string));
            table.Columns.Add(column3);
            var column4 = new DataColumn("Дата завершения (факт)", typeof(string));
            table.Columns.Add(column4);
            var column5 = new DataColumn("Стоимость", typeof(decimal));
            table.Columns.Add(column5);
            var column6 = new DataColumn("ФИО бригадира", typeof(string));
            table.Columns.Add(column6);
            var column7 = new DataColumn("Рег. номер вагона", typeof(long));
            table.Columns.Add(column7);
            foreach (CompleteReport report in Data)
            {
                object[] objects = [
                    report.RepairTask.RepairOrder.DateStart.ToString("d"),
                    report.DateStartFact.ToString("d"),
                    report.RepairTask.RepairOrder.DateStop.ToString("d"),
                    report.DateStopFact.ToString("d"),
                report.RepairTask.RepairOrder.Money,
                report.RepairTask.Foreman.Employee.Name,
                report.RepairTask.RepairOrder.RepairRequest.Wagon.RegNumber];
                table.Rows.Add(objects);
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(table);
            var options = new IronXL.Options.CreatingOptions();
            //WorkBook workBook = WorkBook.Load(ds);
            var workBook = new WorkBook();
            string workBookName = $"{DateTime.Now.ToString("d")} Report.xlsx";
            string path = Path.Combine(Config.GetInstanse().SavePath, workBookName);

            string header = $"Выполненные работы за период {Start} - {Stop}";
            string[] headers = { "Дата начала (план)" , "Дата начала (факт)", "Дата завершения (план)" , "Дата завершения (факт)" , "Стоимость", "ФИО бригадира", "Рег. номер вагона" };
            WorkSheet ws = workBook.CreateWorkSheet("отчет");
            //ws.AddNamedTable(,,new IronXL.Styles.TableStyle() { })
            //ws.AddNamedTable
            for (int i = 0; i < headers.Length; i++)
                ws.SetCellValue(1, i, headers[i]);
            ws.SetCellValue(0, 0, header);
            ws.Merge("A1:G1");
            for (int i = 0; i < table.Rows.Count; i++)
            //foreach (var column in table.Columns)
            {
                var row = table.Rows[i];
                for (int j = 0; j < table.Columns.Count; j++)
                    //foreach (var row in table.Rows)
                {
                    //var cell = ws.GetCellAt(i+1, j);
                    //cell = Cell.;
                    ws.SetCellValue(i+2, j, row[j].ToString());
                    //int[] indexes = {0,1,2,3 };
                    //if (indexes.Contains(j))
                    //    cell.FormatString = "dd/MM/yy";
                    //ws.
                    ws.AutoSizeColumn(j); //переместить
                }
            }

            workBook.SaveAs(path);
            //открываем документ
            Process.Start(new ProcessStartInfo(path)
            { UseShellExecute = true });
            return;
        }



        private void CreateWordMethod()
        {
            throw new NotImplementedException();
        }
    }
}
