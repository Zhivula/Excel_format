using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZHIVULA.Data
{
    class Block_1
    {
        private static Block_1 instance;
        public List<string> KKS;
        //public List<string> Date;
        public List<string> Shleif;
        public List<string> Building;
        public List<string> Room;
        public int Count;

        public Block_1()
        {
            KKS = new List<string>();
            //Date = new List<string>();
            Shleif = new List<string>();
            Building = new List<string>();
            Room = new List<string>();
            Count = 0;

            UpDate();
        }
        public static Block_1 GetInstance()
        {
            if (instance == null) instance = new Block_1();
            return instance;
        }
        public void UpDate()
        {
            KKS = new List<string>();
            Shleif = new List<string>();
            Building = new List<string>();
            Room = new List<string>();

            Count = File.ReadAllLines(@"Block_1.txt").Where(x => x != "").Count();//количество считываемых строк с файла данных о линиях
            string[] lines = File.ReadAllLines(@"Block_1.txt").Take(Count).ToArray();

            for (int i = 1; i < Count; i++)
            {
                string[] row = lines[i].Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

                KKS.Add(row[0]);
                //Date[i] = row[1];
                Shleif.Add(row[1]);
                Building.Add(row[2]);
                Room.Add(row[3]);
            }
        }
    }
}
