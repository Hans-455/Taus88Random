using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Taus88Random.Taus88Random;
using Taus88Random.Taus88Random.Classes;

namespace Taus88Random
{
    class Program
    {
        static void Main(string[] args)
        {
            T88Random rnd = new T88Random();
            RandomContext ctx = rnd.InitContext((uint)DateTime.Now.Ticks);

            Random rndD = new Random();
            //Console.Write(ctx.CurrentNumber % 2);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int count = 0;
            bool lastBit = true;
            int longest = 0;
            for(int i = 0; i < 100000000; i++)
            {
                rnd.StepCtx(ref ctx);

                bool currentBit = (ctx.CurrentNumber % 2) == 0;

                if(currentBit == lastBit)
                {
                    count++;
                }else
                {
                    if(count > longest)
                    {
                        longest = count;
                    }
                    //Console.Write("|" + count.ToString());
                    count = 0;
                }

                lastBit = currentBit;
                //Thread.Sleep(10);
                
                //Console.Write(ctx.CurrentNumber % 2);

            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            Console.WriteLine(longest);
            Console.ReadLine();
        }
    }
}
