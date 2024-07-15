using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZHIVULA.DataBase;

namespace ZHIVULA.Model
{
    class Block_2_Model : INotifyPropertyChanged
    {
        public Block_2_Model()
        {

        }
        public int Count()
        {
            using (var context = new MyDbContext())
            {
                if (context.Cell_2 != null)
                {
                    return context.Cell_2.Count();
                }
                else return 0;
            }
        }
        public List<string> GetBuildingFull()
        {
            using (var context = new MyDbContext())
            {
                if (context.Cell_2.Count() > 0)
                {
                    return context.Cell_2.Select(x => x.Building).ToList();
                }
                else return new List<string>();
            }
        }
        public List<string> GetBuilding()
        {
            using (var context = new MyDbContext())
            {
                if (context.Cell_2.Count() > 0)
                {
                    var list = context.Cell_2.Select(x => x.Building);
                    return list.Distinct().ToList();
                }
                else return new List<string>();
            }
        }
        public List<string> GetKKS()
        {
            using (var context = new MyDbContext())
            {
                if (context.Cell_2.Count() > 0)
                {
                    return context.Cell_2.Select(x => x.KKS).ToList();
                }
                else return new List<string>();
            }
        }
        public List<string> Format(List<string> list)
        {
            return list.Where(x => x[x.Length - 3] == 'B' || x[x.Length - 3] == 'В' && char.IsDigit(x[x.Length - 5]) == true).ToList();
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
