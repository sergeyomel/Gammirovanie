using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab2.PRNG.ParameterInterface;

namespace lab2.PRNG.СalculationStartingParameter
{
    public class GeneratorParameterB : ParameterB
    {
        private long GreatestCommonDivisor(long a, long b)
        {
            while(a != 0 && b != 0)
            {
                if(a > b)
                {
                    a %= b;
                }
                else
                {
                    b %= a;
                }
            }
            return a + b;
        }

        public long GenerateParameter(long m)
        {
            var randomValue = System.DateTime.Now.Millisecond + 15;
            while(randomValue % 2 != 1 && GreatestCommonDivisor(randomValue, m) != 1)
            {
                randomValue += 1;
            }
            return randomValue;
        }
    }
}
