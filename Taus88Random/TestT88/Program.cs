using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taus88Random.Taus88Random;
using Taus88Random.Taus88Random.Classes;

namespace TestT88
{
    class Program
    {
        static void Main(string[] args)
        {
            T88Random t88 = new T88Random();
            RandomContext ctx = t88.InitContext(23423);

            for(int i = 0; i < 10000; ++i)
            {
                Console.WriteLine(ctx.CurrentNumber);
                t88.StepCtx(ref ctx);
            }

            Console.ReadLine();
        }
    }
}
