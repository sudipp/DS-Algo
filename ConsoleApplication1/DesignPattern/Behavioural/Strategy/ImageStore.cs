using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Behavioural.Strategy
{
    public class ImageStore
    {
        //we apply compression (JPEG, PNG)
        //We apply filter (B&W, High Contrat)
        //We apply Resizing (small, medium, large)
        public void Store(string fileName, ICompressor compressor, IFilter filter)
        {
            compressor.Compress(fileName);
            filter.Apply(fileName);
        }
    }
}
