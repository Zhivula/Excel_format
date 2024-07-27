using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ZHIVULA.Data;
using ZHIVULA.Properties;
using ZHIVULA.View;
using Excel = Microsoft.Office.Interop.Excel;

namespace ZHIVULA.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        readonly MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

        public MainWindowViewModel()
        {

        }
        public ICommand GetData => new DelegateCommand(o =>
        {
            string path = string.Empty;

            var dlg = new Microsoft.Win32.OpenFileDialog
            {
                FileName = string.Empty,
                DefaultExt = ".xlsx",

                Filter = "Файлы Excel|*.xlsx"
            };

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                path = dlg.FileName;
            }

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            var list = new List<string>();

            for (int i = 1; i <= rowCount; i++)
            {
                for (int j = 1; j <= colCount; j++)
                {
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null) list.Add(xlRange.Cells[i, j].Value2.ToString());
                }
            }

            var xlSheets = xlWorkbook.Sheets as Excel.Sheets;
            var xlNewSheet = (Excel.Worksheet)xlSheets.Add(xlSheets[1], Type.Missing, Type.Missing, Type.Missing);
            xlNewSheet.Name = "Бирки от Владоса";
            Excel._Worksheet newSheet = xlWorkbook.Sheets[1];
            Excel.Range newRange = newSheet.UsedRange;
            xlNewSheet.Activate();

            int k = 0;
            int y = 1;
            int l = 1;

            while (k != list.Count())
            {
                if (list[k].Length > 6) newRange.Cells[l, y].Value2 = list[k].Substring(5);//откидываем первых 5 символов

                newRange.Cells[l, y].Characters[1, 2].Font.Size = (int)Settings.Default["Position_1"];
                newRange.Cells[l, y].Characters[3, 2].Font.Size = (int)Settings.Default["Position_2"];
                newRange.Cells[l, y].Characters[5, 4].Font.Size = (int)Settings.Default["Position_3"];
                newRange.Cells[l, y].Characters[9, 1].Font.Size = (int)Settings.Default["Position_4"];
                newRange.Cells[l, y].Characters[10, 2].Font.Size = (int)Settings.Default["Position_5"];
                newRange.Cells[l, y].ColumnWidth = (double)Settings.Default["Width"];
                newRange.Cells[l, y].RowHeight = (double)Settings.Default["Height"];
                newRange.Cells[l, y].HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                newRange.Cells[l, y].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                newRange.Cells[l, y].WrapText = true;
                newRange.Cells[l, y].Font.Name = (string)Settings.Default["FontName"];
                newRange.Cells[l, y].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                k++;
                y++;

                if (k % (int)Settings.Default["CountWidth"] == 0)
                {
                    y = 1;
                    l++;
                }
            }
            xlWorkbook.Close();
            xlApp.Quit();
            WindowSuccessfullyViewModel.Successfully();
        });
        public ICommand Block_1_Command => new DelegateCommand(o =>
        {
            window.ChangedGrid.Children.Clear();
            window.ChangedGrid.Children.Add(new BlockView(new Block_1_ViewModel()));
        });
        public ICommand Block_2_Command => new DelegateCommand(o =>
        {
            window.ChangedGrid.Children.Clear();
            window.ChangedGrid.Children.Add(new BlockView(new Block_2_ViewModel()));
        });
        public ICommand SettingsCommand => new DelegateCommand(o =>
        {
            window.ChangedGrid.Children.Clear();
            window.ChangedGrid.Children.Add(new SettingsView());
        });
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion  
    }
}
