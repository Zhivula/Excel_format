using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ZHIVULA.Data;
using ZHIVULA.DataBase;
using ZHIVULA.Model;
using ZHIVULA.Properties;
using Excel = Microsoft.Office.Interop.Excel;

namespace ZHIVULA.ViewModel
{
    class Block_2_ViewModel : INotifyPropertyChanged, IBlockViewModel
    {
        private List<string> building;
        public List<string> Building
        {
            get => building;
            set
            {
                building = value;
                OnPropertyChanged(nameof(Building));
            }
        }
        private List<string> buildingFull;
        public List<string> BuildingFull
        {
            get => buildingFull;
            set
            {
                buildingFull = value;
                OnPropertyChanged(nameof(BuildingFull));
            }
        }
        private List<string> number_B;
        public List<string> Number_B
        {
            get => number_B;
            set
            {
                number_B = value;
                OnPropertyChanged(nameof(Number_B));
            }
        }
        private List<string> listB;
        public List<string> ListB
        {
            get => listB;
            set
            {
                listB = value;
                OnPropertyChanged(nameof(ListB));
            }
        }
        private string selectedItem;
        public string SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        private string selectedItem_ListB;
        public string SelectedItem_ListB
        {
            get => selectedItem_ListB;
            set
            {
                selectedItem_ListB = value;
                OnPropertyChanged(nameof(SelectedItem_ListB));
            }
        }
        private string pathFile;
        public string PathFile
        {
            get => pathFile;
            set
            {
                pathFile = value;
                OnPropertyChanged(nameof(PathFile));
            }
        }
        private Visibility updateStackPanel;
        public Visibility UpdateStackPanel
        {
            get => updateStackPanel;
            set
            {
                updateStackPanel = value;
                OnPropertyChanged(nameof(UpdateStackPanel));
            }
        }
        private int updateCell_All;
        public int UpdateCell_All
        {
            get => updateCell_All;
            set
            {
                updateCell_All = value;
                OnPropertyChanged(nameof(UpdateCell_All));
            }
        }
        private int updateCell_inProcess;
        public int UpdateCell_inProcess
        {
            get => updateCell_inProcess;
            set
            {
                updateCell_inProcess = value;
                OnPropertyChanged(nameof(UpdateCell_inProcess));
            }
        }
        private SolidColorBrush iconForeground;
        public SolidColorBrush IconForeground
        {
            get => iconForeground;
            set
            {
                iconForeground = value;
                OnPropertyChanged(nameof(IconForeground));
            }
        }
        private PackIconKind icon;
        public PackIconKind Icon
        {
            get => icon;
            set
            {
                icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }
        private Block_2_Model model;

        public Block_2_ViewModel()
        {
            model = new Block_2_Model();
            UpdateStackPanel = Visibility.Hidden;
            Building = new List<string>();
            BuildingFull = new List<string>();
            Number_B = new List<string>();
            ListB = new List<string>();
            if (model.Count() > 0)
            {
                BuildingFull = model.GetBuildingFull();
                Building = model.GetBuilding();
                Number_B = model.GetKKS();
                SelectedItem = Building.FirstOrDefault();
                Icon = PackIconKind.CheckCircle;
                IconForeground = new SolidColorBrush((Color)new ColorConverter().ConvertFrom("#00E676"));
            }
            else
            {
                Icon = PackIconKind.CloseCircle;
                IconForeground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            }
        }
        public ICommand GetB => new DelegateCommand(o =>
        {
            var list = new List<string>();
            if (SelectedItem != null)
            {
                for (var i = 0; i < BuildingFull.Count(); i++)
                {
                    if (BuildingFull[i] == SelectedItem)
                    {
                        list.Add(Number_B[i]);
                    }
                }
            }

            ListB = model.Format(list);
            SelectedItem_ListB = ListB.FirstOrDefault();
            WindowSuccessfullyViewModel.Successfully();
        });
        public ICommand GetData => new DelegateCommand(o =>
        {
            var dlg = new Microsoft.Win32.OpenFileDialog
            {
                FileName = string.Empty,
                DefaultExt = ".xlsx",

                Filter = "Файлы Excel|*.xlsx"
            };

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                PathFile = dlg.FileName;
                GetDataExcel(PathFile);
            }
        });
        public ICommand WhiteBirkiInFile => new DelegateCommand(o =>
        {
            var dlg = new Microsoft.Win32.OpenFileDialog
            {
                FileName = string.Empty,
                DefaultExt = ".xlsx",

                Filter = "Файлы Excel|*.xlsx"
            };

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                PathFile = dlg.FileName;
                GetDataExcel(PathFile, listB);
            }
        });
        public ICommand GetData_2 => new DelegateCommand(o =>
        {
            var dlg = new Microsoft.Win32.OpenFileDialog
            {
                FileName = string.Empty,
                DefaultExt = ".xlsx",

                Filter = "Файлы Excel|*.xlsx"
            };

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                PathFile = dlg.FileName;
                using (var context = new MyDbContext())
                {
                    context.Cell_2.RemoveRange(context.Cell_2.ToList());
                    context.SaveChanges();
                }
                GetDataExcel_2(PathFile);
                WindowSuccessfullyViewModel.Successfully();
            }
        });
        private void GetDataExcel_2(string path)
        {
            new Thread(() => {
                Excel.Application xlApp = new Excel.Application();

                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);

                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];

                Excel.Range UsedRange = xlWorksheet.UsedRange;

                int rowCount = UsedRange.Rows.Count;
                int colCount = UsedRange.Columns.Count;

                using (var context = new MyDbContext())
                {
                    if (context.Cell_2.Count() == 0)
                    {
                        UpdateStackPanel = Visibility.Visible;
                        UpdateCell_All = rowCount;

                        for (int i = 1; i <= rowCount; i++)
                        {
                            if (UsedRange.Cells[i, 4] != null && UsedRange.Cells[i, 4].Value2 != null &&
                                UsedRange.Cells[i, 10] != null && UsedRange.Cells[i, 10].Value2 != null &&
                                UsedRange.Cells[i, 20] != null && UsedRange.Cells[i, 20].Value2 != null &&
                                UsedRange.Cells[i, 21] != null && UsedRange.Cells[i, 21].Value2 != null &&
                                UsedRange.Cells[i, 22] != null && UsedRange.Cells[i, 22].Value2 != null)
                            {
                                context.Cell_2.Add(new Cell_2()
                                {
                                    KKS = UsedRange.Cells[i, 4].Value2.ToString(),
                                    Date = UsedRange.Cells[i, 10].Value2.ToString(),
                                    Shleif = UsedRange.Cells[i, 20].Value2.ToString(),
                                    Building = UsedRange.Cells[i, 21].Value2.ToString(),
                                    Room = UsedRange.Cells[i, 22].Value2.ToString()
                                });
                            }
                            UpdateCell_inProcess = i;
                        }
                        context.SaveChanges();
                    }
                }
                xlWorkbook.Close();
                xlApp.Quit();
                UpdateStackPanel = Visibility.Hidden;
            }).Start();


            BuildingFull = model.GetBuildingFull();
            Number_B = model.GetKKS();
            Building = model.GetBuilding();
            WindowSuccessfullyViewModel.Successfully();
        }
        private void GetDataExcel(string path, List<string> dataList = null)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            var list = new List<string>();

            if (dataList == null)
            {
                for (int i = 1; i <= rowCount; i++)
                {
                    for (int j = 1; j <= colCount; j++)
                    {
                        if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null) list.Add(xlRange.Cells[i, j].Value2.ToString());
                    }
                }
            }
            else if (ListB != null) list = ListB;

            int t = 0;

            var xlSheets = xlWorkbook.Sheets as Excel.Sheets;
            var xlNewSheet = (Excel.Worksheet)xlSheets.Add(xlSheets[1], Type.Missing, Type.Missing, Type.Missing);
            xlNewSheet.Name = "Бирки от Владоса";
            Excel._Worksheet newSheet = xlWorkbook.Sheets[1];
            Excel.Range newRange = newSheet.UsedRange;
            xlNewSheet.Activate();

            for (int i = 1; i <= list.Count() / 3; i++)
            {
                for (int j = 1; j <= 3; j++)
                {
                    var b = list[t];
                    if (b.Length > 6)
                    {
                        string newB = b.Substring(5);
                        newRange.Cells[i, j].Value2 = newB;
                    }
                    newRange.Cells[i, j].Characters[1, 2].Font.Size = 48;
                    newRange.Cells[i, j].Characters[3, 2].Font.Size = 26;
                    newRange.Cells[i, j].Characters[5, 4].Font.Size = 48;
                    newRange.Cells[i, j].Characters[9, 1].Font.Size = 36;
                    newRange.Cells[i, j].Characters[10, 2].Font.Size = 90;
                    newRange.Cells[i, j].ColumnWidth = (double)Settings.Default["Width"];
                    newRange.Cells[i, j].RowHeight = (double)Settings.Default["Height"];
                    newRange.Cells[i, j].HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    newRange.Cells[i, j].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    newRange.Cells[i, j].WrapText = true;
                    newRange.Cells[i, j].Font.Name = "Times New Roman";
                    newRange.Cells[i, j].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    t++;
                }
            }
            xlWorkbook.Close();
            xlApp.Quit();
            WindowSuccessfullyViewModel.Successfully();
        }
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion  
    }
}
