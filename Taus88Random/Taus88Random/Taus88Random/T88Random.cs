/*(*************************************************************************

 DESCRIPTION   :  Taus88 based pseudo random number generator
                  Period about 2**88

 REQUIREMENTS  :  TP5-7, D1-D7/D9-D10, FPC, VP

 EXTERNAL DATA :  ---

 MEMORY USAGE  :  ---

 DISPLAY MODE  :  ---

 REFERENCES    :  [1] P. L'Ecuyer, "Maximally Equidistributed Combined Tausworthe
                      Generators", Mathematics of Computation 65, 213 (1996), 203-213
                  [2] http://www.iro.umontreal.ca/~lecuyer/myftp/papers/tausme.ps
                      Online version of [1] with corrections


 Version  Date      Author      Modification
 -------  --------  -------     ------------------------------------------
 0.10     23.04.05  W.Ehrhardt  Initial BP7 version, prng_next, self-test
 0.11     24.04.05  we          Remaining functions
 0.12     24.04.05  we          {$N+} only for BIT16, long/word/double
 0.13     24.04.05  we          BASM16
 0.14     24.04.05  we          prng_double: first add then divide
 0.15     11.05.05  we          Bugfix and consts M,A
 0.16     29.05.05  we          renamed unit to PRNG, .sn to .nr
 0.17     30.05.05  we          new selftest values, constant ICNT
 0.18     02.08.05  we          Bugfix: inc s3 in prng_init3
 0.19     02.08.05  we          Changed prng_ to taus88_
 0.20     05.11.08  we          taus88_dword function
 0.21     02.12.08  we          BTypes/Ptr2Inc
 0.22     07.01.09  we          Uses BTypes moved to implementation
**************************************************************************)


(*-------------------------------------------------------------------------
 (C) Copyright 2005-2009 Wolfgang Ehrhardt

 This software is provided 'as-is', without any express or implied warranty.
 In no event will the authors be held liable for any damages arising from
 the use of this software.

 Permission is granted to anyone to use this software for any purpose,
 including commercial applications, and to alter it and redistribute it
 freely, subject to the following restrictions:

 1. The origin of this software must not be misrepresented; you must not
    claim that you wrote the original software. If you use this software in
    a product, an acknowledgment in the product documentation would be
    appreciated but is not required.

 2. Altered source versions must be plainly marked as such, and must not be
    misrepresented as being the original software.

 3. This notice may not be removed or altered from any source distribution.
----------------------------------------------------------------------------*)*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taus88Random.Taus88Random.Classes;

namespace Taus88Random.Taus88Random
{
    public class T88Random
    {
        uint TC1 = 4294967294;
        uint TC2 = 4294967288;
        uint TC3 = 4294967280;

        public T88Random()
        {

        }

        public RandomContext InitContext(uint seed)
        {
            uint M = 69069;
            uint A = 1;
            uint seed2 = M * seed + A;
            uint seed3 = M * seed2 + A;

            return Init3(seed, seed2, seed3);
        }

        private RandomContext Init3(uint seed1, uint seed2, uint seed3)
        {
            int initCount = 6;

            RandomContext ctx = new RandomContext();
            ctx.Seed1 = seed1;
            ctx.Seed2 = seed2;
            ctx.Seed3 = seed3;

            uint bA1 = ctx.Seed1 & TC1;
            uint bA2 = ctx.Seed2 & TC2;
            uint bA3 = ctx.Seed3 & TC3;

            if(bA1 == 0)
            {
                ctx.Seed1 += 2;
            }
            if (bA2 == 0)
            {
                ctx.Seed2 += 8;
            }
            if (bA3 == 0)
            {
                ctx.Seed3 += 16;
            }

            for(int i = 0; i <= initCount; i++)
            {
                StepCtx(ref ctx);
            }
            return ctx;
        }

        public void StepCtx(ref RandomContext ctx)
        {
            ctx.Seed1 = ((ctx.Seed1 & TC1) << 12) ^ (((ctx.Seed1 << 13) ^ ctx.Seed1) >> 19);
            ctx.Seed2 = ((ctx.Seed2 & TC2) << 4) ^ (((ctx.Seed2 << 2) ^ ctx.Seed2) >> 25);
            ctx.Seed3 = ((ctx.Seed3 & TC3) << 17) ^ (((ctx.Seed3 << 3) ^ ctx.Seed3) >> 11);

            ctx.CurrentNumber = ctx.Seed1 ^ ctx.Seed2 ^ ctx.Seed3;
        }


    }
}
