using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.MicrosoftFolder
{
    public class Resource
    {
        public string Name;
        public Resource(string name)
        {
            this.Name = name;
        }
    }
    public class ResourceManager
    {
        ConcurrentDictionary<string, Resource> resources = null;
        ConcurrentDictionary<Resource, HashSet<int>> ReadQ = null;
        ConcurrentDictionary<Resource, HashSet<int>> WriteQ = null;

        public Resource AddResource(string resourceName)
        {
            if (!resources.ContainsKey(resourceName))
                resources.GetOrAdd(resourceName, new Resource(resourceName));

            ReadQ.GetOrAdd(resources[resourceName], new HashSet<int>());
            WriteQ.GetOrAdd(resources[resourceName], new HashSet<int>());

            return resources[resourceName];
        }

        public ResourceManager()
        {
            this.resources = new ConcurrentDictionary<string, Resource>();
            this.ReadQ = new ConcurrentDictionary<Resource, HashSet<int>>();
            this.WriteQ = new ConcurrentDictionary<Resource, HashSet<int>>();
        }

        public bool Lock(string resourceName, int threadNumber, bool isReadLock)
        {
            if (!resources.ContainsKey(resourceName))
                throw new Exception("Resource Not Found");

            /*
             // operating on single shared resource
            /*
            -- Support shared reads
                a) R1L/grant R2L/grant R3L/grant R1U R2U R3U
            -- Block simultenous read/write
                a) R1L/grant W1L/block
                b) R1L/grant R1U W1L/grant
            -- Block simultenous writes
                a) W1L/grant W2L/block
                b) W1L/grant W1U W2L/grant 
            */

            Resource res = resources[resourceName];
            if(isReadLock) //for Read locking
            {
                if (WriteQ[res].Any())
                    return false;

                ReadQ[res].Add(threadNumber);
            }
            else //for write locking
            {
                if (ReadQ[res].Any() || WriteQ[res].Any())
                    return false;

                WriteQ[res].Add(threadNumber);
            }
            return true;
        }
        public void Unlock(string resourceName, int threadNumber, bool isReadLock)
        {
            if (!resources.ContainsKey(resourceName))
                throw new Exception("Resource Not Found");

            Resource res = resources[resourceName];
            if(isReadLock)
                ReadQ[res].Remove(threadNumber);
            else
                WriteQ[res].Remove(threadNumber);
        }


    }
}
