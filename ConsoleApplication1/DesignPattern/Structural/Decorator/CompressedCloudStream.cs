using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Structural.Decorator
{
    public class CompressedCloudStream : IStream
    {
        private IStream steam;
        public CompressedCloudStream(IStream steam)
        {
            this.steam = steam;
        }
        public void Write(string data)
        {
            string compressed = data.Substring(0, 3);
            this.steam.Write(compressed);
        }
    }
}
