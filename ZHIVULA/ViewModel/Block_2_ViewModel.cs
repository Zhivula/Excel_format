using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ZHIVULA.Data;
using ZHIVULA.Model;
using ZHIVULA.Properties;
using Excel = Microsoft.Office.Interop.Excel;

namespace ZHIVULA.ViewModel
{
    class Block_2_ViewModel : INotifyPropertyChanged, IBlockViewModel
    {
        private ObservableCollection<Room> rooms;
        public ObservableCollection<Room> Rooms
        {
            get => rooms;
            set
            {
                rooms = value;
                OnPropertyChanged(nameof(Rooms));
            }
        }
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
        private string selectedItem_Building;
        public string SelectedItem_Building
        {
            get => selectedItem_Building;
            set
            {
                selectedItem_Building = value;
                OnPropertyChanged(nameof(SelectedItem_Building));
                UpdateRooms();
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

        public DelegateCommand GetData { get; set; }

        public Block_2_ViewModel()
        {
            model = new Block_2_Model();
            UpdateStackPanel = Visibility.Hidden;
            Building = new List<string>();
            BuildingFull = new List<string>();
            Number_B = new List<string>();
            ListB = new List<string>();
            Rooms = new ObservableCollection<Room>();
            if (model.Count() > 0)
            {
                BuildingFull = model.GetBuildingFull();
                Building = model.GetBuilding();
                Number_B = model.GetKKS();
                selectedItem_Building = Building.FirstOrDefault();
                Icon = PackIconKind.CheckCircle;
                IconForeground = new SolidColorBrush((Color)new ColorConverter().ConvertFrom("#00E676"));
            }
            else
            {
                Icon = PackIconKind.CloseCircle;
                IconForeground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            }
            GetData = new DelegateCommand(o => { GetDataMethod(); });
        }
        public ICommand GetB => new DelegateCommand(o =>
        {
            var list = new List<string>();

            var rooms = Block_2.GetInstance().Room;
            var rooms_cheched = new List<string>();

            foreach (var item in Rooms)
            {
                if (item.Checked) rooms_cheched.Add(item.Text);
            }

            if (selectedItem_Building != null)
            {
                for (var i = 0; i < rooms.Count(); i++)
                {
                    if (rooms_cheched.Contains(rooms[i]))
                    {
                        list.Add(Number_B[i]);
                    }
                }
            }

            ListB = model.Format(list);
            SelectedItem_ListB = ListB.FirstOrDefault();
            WindowSuccessfullyViewModel.Successfully();
        });
        public void GetDataMethod()
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
        }
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

                GetDataExcel_2(PathFile);
                WindowSuccessfullyViewModel.Successfully();
            }
        });

        /// <summary>
        /// Этот метод перезаписывает(обновляет) данные из Excel(ВОР СКУПЗ)
        /// </summary>
        /// <param name="path"></param>
        private void GetDataExcel_2(string path)
        {
            new Thread(() => {
                Excel.Application xlApp = new Excel.Application();

                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);

                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];

                Excel.Range UsedRange = xlWorksheet.UsedRange;

                int rowCount = UsedRange.Rows.Count;
                int colCount = UsedRange.Columns.Count;

                UpdateStackPanel = Visibility.Visible;
                UpdateCell_All = rowCount;
                Console.WriteLine(rowCount);
                // полная перезапись файла 
                using (StreamWriter writer = new StreamWriter(@"Block_2.txt", false))
                {
                    for (int i = 1; i <= rowCount; i++)
                        {
                            if (UsedRange.Cells[i, 4] != null && UsedRange.Cells[i, 4].Value2 != null)
                            {
                                if (UsedRange.Cells[i, 20] == null || UsedRange.Cells[i, 20].Value2 == null)
                                {
                                    UsedRange.Cells[i, 20].Value2 = "-";
                                }
                                if (UsedRange.Cells[i, 21] == null || UsedRange.Cells[i, 21].Value2 == null)
                                {
                                    UsedRange.Cells[i, 21].Value2 = "-";
                                }
                                if (UsedRange.Cells[i, 22] == null || UsedRange.Cells[i, 22].Value2 == null)
                                {
                                    UsedRange.Cells[i, 22].Value2 = "-";
                                }
                                //4-KKS
                                //10-Date
                                //20-Shleif
                                //21-Building
                                //22-Room
                                writer.WriteLineAsync(
                                    UsedRange.Cells[i, 4].Value2.ToString() + "&"
                                    + UsedRange.Cells[i, 20].Value2.ToString() + "&"
                                    + UsedRange.Cells[i, 21].Value2.ToString() + "&"
                                    + UsedRange.Cells[i, 22].Value2.ToString()
                                    );
                            }
                            UpdateCell_inProcess = i;
                        }
                }
                xlWorkbook.Close();
                xlApp.Quit();
                UpdateStackPanel = Visibility.Hidden;
            

            Block_2.GetInstance().UpDate();//уведомляем, что изменились данные в файле

            BuildingFull = model.GetBuildingFull();
            Number_B = model.GetKKS();
            Building = model.GetBuilding();
            }).Start();
            WindowSuccessfullyViewModel.Successfully();
        }
        /// <summary>
        /// Вписывает форматированные строки - это уже готовые бирки
        /// </summary>
        /// <param name="path"></param>
        /// <param name="dataList"></param>
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
        }
        public void UpdateRooms()
        {
            Rooms.Clear();
            var building = Block_2.GetInstance().Building;
            var rooms = Block_2.GetInstance().Room;
            var bag = new List<string>();
            for(var i = 0; i < building.Count; i++)
            {
                if (building[i] == SelectedItem_Building)
                {
                    bag.Add(rooms[i]);
                }
            }
            bag = bag.Distinct().OrderBy(x=>x).ToList();
            foreach (var item in bag)
            {
                Rooms.Add(new Room()
                {
                    Text = item,
                    Checked = false
                });
            }
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
