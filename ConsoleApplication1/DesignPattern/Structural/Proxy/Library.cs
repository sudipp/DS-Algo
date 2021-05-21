using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Structural.Proxy
{
    public class Library
    {
        //Dictionary<string, IeBook> map = new Dictionary<string, IeBook>();
        //public void Add(IeBook ebook)
        //{
        //    map.Add(ebook.GetFileName(), ebook);
        //}
        //public void OpenEBook(string fileName)
        //{
        //    map[fileName].Show();
        //}

        Dictionary<string, RealEBook> map = new Dictionary<string, RealEBook>();
        public void Add(RealEBook ebook)
        {
            map.Add(ebook.GetFileName(), ebook);
        }
        public void OpenEBook(string fileName)
        {
            map[fileName].Show();
        }
    }
}
