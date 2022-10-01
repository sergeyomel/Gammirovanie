using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using lab2.PRNG.ParameterInterface;
using lab2.PRNG.СalculationStartingParameter;

namespace lab2.PRNG
{
    public class Generator
    {
        private string path = System.Environment.CurrentDirectory + @"\PRNG\Subsequence.txt";

        private readonly int m = (int)Math.Pow(2, 24);

        private long a;
        private long b;
        private long c;
        private long c0;

        public Generator()
        {
            GenerateParameters();
        }

        public void GenerateParameters()
        {
            ParameterA ga = new GeneratorParameterA();
            ParameterB gb = new GeneratorParameterB();
            ParameterC gc = new GeneratorParameterC();

            a = ga.GenerateParameter(m);
            b = gb.GenerateParameter(m);
            c0 = gc.GenerateParameter();

            c = c0;
        }

        public void WriteGeneratorParameters()
        {
            var lParams = new List<string> { a.ToString(), b.ToString(), c0.ToString() };

            var parameters = string.Join("\n", lParams);
            File.WriteAllText(StaticData.PathKeyFale, parameters, System.Text.Encoding.UTF8);
        }

        public (long a, long b, long c) GetParameters() => (a, b, c0);
        public void SetParameters(long a, long b, long c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public long Next()
        {
            c = (a * c + b) % m;
            return c;
        }

        public string GetSubsequence(int countNum)
        {
            var sb = new System.Text.StringBuilder();
            for (int subCount = 0; subCount < countNum; subCount++)
            {
                sb.Append(Next().ToString() + " ");
            }
            sb.Append("\n");

            return sb.ToString();
        }
    }
}
