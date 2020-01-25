using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taus88Random.Taus88Random.Classes
{
    public class RandomContext
    {
        public uint Seed1
        {
            get; set;
        }
        public uint Seed2 { get; set; }
        public uint Seed3 { get; set; }
        public uint CurrentNumber { get; set; }
    }
}
