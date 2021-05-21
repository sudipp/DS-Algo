using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Structural.Proxy
{
    public class RealEBook : IeBook
    {
        private string fileName;
        public RealEBook(string fileName)
        {
            this.fileName = fileName;
            Load();
        }
        public void Load()
        {
            Console.WriteLine("Loading eBook from database " + fileName);
        }
        public string GetFileName()
        {
            return fileName;
        }
        public void Show()
        {
            Console.WriteLine("Showing eBook " + fileName);
        }
    }
}
