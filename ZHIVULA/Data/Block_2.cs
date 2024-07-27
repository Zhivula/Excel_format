using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZHIVULA.Data
{
    public class Block_2
    {
        private static Block_2 instance;
        public List<string> KKS;
        //public List<string> Date;
        public List<string> Shleif;
        public List<string> Building;
        public List<string> Room;
        public int Count;

        public Block_2()
        {
            KKS = new List<string>();
            //Date = new List<string>();
            Shleif = new List<string>();
            Building = new List<string>();
            Room = new List<string>();
            Count = 0;

            UpDate();
        }
        public static Block_2 GetInstance()
        {
            if (instance == null) instance = new Block_2();
            return instance;
        }
        public void UpDate()
        {
            KKS = new List<string>();
            Shleif = new List<string>();
            Building = new List<string>();
            Room = new List<string>();

            Count = File.ReadAllLines(@"Block_2.txt").Where(x => x != "").Count();//количество считываемых строк с файла данных о линиях
            string[] lines = File.ReadAllLines(@"Block_2.txt").Take(Count).ToArray();

            for (int i = 1; i < Count; i++)
            {
                string[] row = lines[i].Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

                KKS.Add(row[0]);
                //Date[i] = row[1];
                Shleif.Add(row[1]);
                Building.Add(row[2]);
                Room.Add(row[3]);
                Console.WriteLine(i);
            }
            Console.WriteLine(Count);
        }
    }
}
