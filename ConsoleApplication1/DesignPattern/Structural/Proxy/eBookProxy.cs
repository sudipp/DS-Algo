using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Structural.Proxy
{
    public class eBookProxy : IeBook
    {
        private string fileName;
        private RealEBook realebook;
        public eBookProxy(string fileName)
        {
            this.fileName = fileName;
        }
        public string GetFileName()
        {
            return fileName;
        }
        public void Show()
        {
            if (realebook == null)
                realebook = new RealEBook(fileName);
            realebook.Show();
        }
    }
}
