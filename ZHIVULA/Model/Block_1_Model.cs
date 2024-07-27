using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZHIVULA.Data;

namespace ZHIVULA.Model
{
    class Block_1_Model : INotifyPropertyChanged
    {
        public Block_1_Model()
        {

        }
        public int Count()
        {
            return Block_1.GetInstance().Count;
        }
        public List<string> GetBuildingFull()
        {
            return Block_1.GetInstance().Building;
        }
        public List<string> GetBuilding()
        {
            var list = Block_1.GetInstance().Building;
            return list.Distinct().ToList();
        }
        public List<string> GetKKS()
        {
            return Block_1.GetInstance().KKS;
        }
        public List<string> Format(List<string> list)
        {
            return list.Where(x => (x[x.Length - 3] == 'B' || x[x.Length - 3] == 'В') && (char.IsDigit(x[x.Length - 5]) == true)).ToList();
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
