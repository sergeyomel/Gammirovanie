using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab2.PRNG.ParameterInterface;

namespace lab2.PRNG.СalculationStartingParameter
{
    public class GeneratorParameterC: ParameterC
    {
        public long GenerateParameter()
        {
            return System.DateTime.Now.Millisecond % 10000000 + 10;
        }
    }
}
