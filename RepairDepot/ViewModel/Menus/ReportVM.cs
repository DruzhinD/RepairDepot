using DatabaseAdapter.Models;
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
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using ClosedXML.Excel;

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
        public RelayCommand CreateExcel => createExcel ??= new RelayCommand(obj =>
        {
            Thread thread = new Thread(CreateExcelMethod);
            thread.Start();
        });
        RelayCommand createExcel;

        /// <summary>
        /// Создать word
        /// </summary>
        public RelayCommand CreateWord => createWord ??= new RelayCommand(obj =>
        {
            Thread thread = new Thread(CreateWordMethod);
            thread.Start();
        });
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

        /// <summary>
        /// Преобразует текущую коллекцию Data в объект DataTable
        /// </summary>
        DataTable CollectionToDataTable()
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
            return table;
        }

        private void CreateExcelMethod()
        {
            var table = CollectionToDataTable();

            lock (Data)
            {

                using (XLWorkbook wbook = new XLWorkbook())
                {
                    //создание листа
                    IXLWorksheet ws = wbook.Worksheets.Add("Отчет");

                    string workBookName = $"Отчет за {DateTime.Now.ToString("dd.MM.yy H`m`s")}.xlsx";
                    string savePath = Path.Combine(Config.GetInstanse().SavePath, workBookName);

                    int rowIndexer = 0; //индекс строки, указывает на текущую пустую строку в Excel

                    //заголовок таблицы
                    string tableHeader = string.Format("Выполненные работы за период {0} - {1}",
                        arg0: Start.ToString("d"),
                        arg1: Stop.ToString("d"));
                    
                    var range = ws.Range($"A{1 + rowIndexer++}").Merge();
                    range.Value = tableHeader; //объединяем ячейки
                    range.Style.Font.SetBold(true).Font.FontSize = 16; //форматирование

                    string alphabet = "ABCDEFGHIJKLMNOPQRSTUV"; //алфавит
                    string tableStartIndex = $"{alphabet[0]}{1 + rowIndexer}";

                    var borderStyle = XLBorderStyleValues.Medium;
                    //заголовки столбцов таблицы
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        var cellIndex = $"{alphabet[i]}{1 + rowIndexer}";
                        ws.Cell(cellIndex).Value = table.Columns[i].ColumnName; //значение

                        //граница
                        ws.Cell(cellIndex).Style.Border.LeftBorder = borderStyle;
                        ws.Cell(cellIndex).Style.Border.TopBorder = borderStyle;
                        ws.Cell(cellIndex).Style.Border.RightBorder = borderStyle;
                        ws.Cell(cellIndex).Style.Border.BottomBorder = borderStyle;
                    }
                    rowIndexer++;

                    //заполняем таблицу данными
                    foreach (DataRow row in table.Rows)
                    {
                        for (int j = 0; j < table.Columns.Count; j++)
                        {
                            string cellIndex = $"{alphabet[j]}{1 + rowIndexer}"; //текущий индекс ячейки
                            var rowValue = row[j]; //значение из таблицы
                                                   //форматы данных
                            if (rowValue.GetType() == typeof(DateOnly))
                            {
                                ws.Cell(cellIndex).Style.DateFormat.Format = "dd/MM/yy";
                            }
                            else if (float.TryParse(rowValue.ToString(), out float a))
                            {
                                ws.Cell(cellIndex).Style.NumberFormat.Format = "0";
                            }
                            ws.Cell(cellIndex).Value = rowValue.ToString();

                            ws.Cell(cellIndex).Style.Border.LeftBorder = borderStyle;
                            ws.Cell(cellIndex).Style.Border.TopBorder = borderStyle;
                            ws.Cell(cellIndex).Style.Border.RightBorder = borderStyle;
                            ws.Cell(cellIndex).Style.Border.BottomBorder = borderStyle;
                        }
                        rowIndexer++;
                    }
                    string tableStopIndex = $"{alphabet[table.Columns.Count - 1]}{1 + rowIndexer - 1}";

                    //авторазмер столбцов
                    ws.Columns().AdjustToContents();

                    //сохранение
                    wbook.SaveAs(savePath);
                    //открываем документ
                    Process.Start(new ProcessStartInfo(savePath)
                    { UseShellExecute = true });
                }

            }

        }



        private void CreateWordMethod()
        {
            //имя документа
            string docName = $"Отчет за {DateTime.Now.ToString("dd.MM.yy H`m`s")}.docx";
            string savePath = Path.Combine(Config.GetInstanse().SavePath, docName);

            using (var wordDoc = WordprocessingDocument.Create(savePath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            {
                //основная часть документа
                MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = new Body();

                //заголовок таблицы
                string tableHeader = string.Format("Выполненные работы за период {0} - {1}",
                    arg0: Start.ToString("d"),
                    arg1: Stop.ToString("d"));

                string font = "Arial"; //название шрифта

                //параметры текста
                var textProps = new RunProperties()
                {
                    FontSize = new FontSize() { Val = "36" },
                    RunFonts = new RunFonts() { Ascii = font }
                };

                //параграф с текстом
                var text = new Text(tableHeader);
                Paragraph paragraph = new Paragraph(new Run(text) { RunProperties = textProps });
                //добавляем параграф в тело документа
                body.Append(paragraph);

                //данные для заполнения
                var dataTable = CollectionToDataTable();

                //таблица word
                Table table = new Table();

                //граница таблицы
                TableProperties tableProperties = new TableProperties(
                    new TableBorders(new TopBorder()
                    { Val = new EnumValue<BorderValues>(BorderValues.Sawtooth), Size = 6 },
                        new BottomBorder()
                        { Val = new EnumValue<BorderValues>(BorderValues.Sawtooth), Size = 6 },
                        new LeftBorder()
                        { Val = new EnumValue<BorderValues>(BorderValues.Sawtooth), Size = 6 },
                        new RightBorder()
                        { Val = new EnumValue<BorderValues>(BorderValues.Sawtooth), Size = 6 },
                        new InsideHorizontalBorder()
                        { Val = new EnumValue<BorderValues>(BorderValues.Sawtooth), Size = 6 },
                        new InsideVerticalBorder()
                        { Val = new EnumValue<BorderValues>(BorderValues.Sawtooth), Size = 6 })
                );
                table.AppendChild<TableProperties>(tableProperties);

                lock (Data)
                {



                    //заполняем заголовки таблицы
                    TableRow tableRow = new TableRow();
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        //параметры текста
                        textProps = new RunProperties()
                        {
                            FontSize = new FontSize() { Val = "24" },
                            RunFonts = new RunFonts() { Ascii = font },
                            Bold = new Bold(),
                        };
                        //ячейка
                        TableCell cell = new TableCell();
                        cell.Append(new Paragraph(
                                    new Run(
                                        new Text(column.ColumnName)
                                            )
                                    { RunProperties = textProps }));
                        tableRow.Append(cell);
                    }
                    table.Append(tableRow);

                    //заполняем данные
                    for (int row = 0; row < dataTable.Rows.Count; row++)
                    {
                        tableRow = new TableRow();
                        for (int column = 0; column < dataTable.Columns.Count; column++)
                        {
                            //параметры текста
                            textProps = new RunProperties()
                            {
                                FontSize = new FontSize() { Val = "18" },
                                RunFonts = new RunFonts() { Ascii = font }
                            };

                            //1 единица данных из таблицы (ячейка)
                            TableCell cell = new TableCell(
                                new Paragraph(
                                    new Run(
                                        new Text(dataTable.Rows[row][column].ToString()
                                        ))
                                    { RunProperties = textProps }));
                            tableRow.Append(cell);
                        }
                        table.Append(tableRow);
                    }

                    //добавляем таблицу в тело документа
                    body.Append(table);

                    mainPart.Document.Append(body);
                    mainPart.Document.Save();
                }
            }

            //открываем документ
            Process.Start(new ProcessStartInfo(savePath)
            { UseShellExecute = true });
        }
    }
}
