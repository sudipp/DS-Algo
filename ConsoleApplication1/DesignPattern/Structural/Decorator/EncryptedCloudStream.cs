using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Structural.Decorator
{
    public class EncryptedCloudStream : IStream
    {
        private IStream steam;
        public EncryptedCloudStream(IStream steam)
        {
            this.steam = steam;
        }
        public void Write(string data)
        {
            string encrypted = "#@$%^&";
            this.steam.Write(encrypted);
        }
    }
}
