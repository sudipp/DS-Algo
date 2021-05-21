using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Structural.Decorator
{
    public class Client
    {
        public static void main(string[] args)
        {
            var compressedStream = new CompressedCloudStream(new CloudStream());
            var encryptedStream = new EncryptedCloudStream(compressedStream);
            StoreCreaditCard(encryptedStream);
        }
        private static void StoreCreaditCard(IStream stream)
        {
            stream.Write("1234-45-1989");
        }
    }
}
