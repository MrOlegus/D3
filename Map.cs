using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3
{
    public class Map
    {
        public List<Item> items = new List<Item>();

        public Map(string fileName)
        {
            StreamReader sr = new StreamReader(fileName, System.Text.Encoding.Default);

            string objFile;
            while ((objFile = sr.ReadLine()) != null) // чтение файлов объектов
            {
                if (objFile == "") break;

                items.Add(new Item(objFile));
            }

            sr.Close();
        }
    }
}
