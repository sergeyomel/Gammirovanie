using Histogramm;
using System.Collections.Specialized;
using System.Configuration;

using lab2.PRNG;
using lab2.PRNG.ParameterInterface;
using lab2.PRNG.СalculationStartingParameter;
using lab2;

namespace GPSH
{
    public class Programm
    {
        public static void Main()
        {
            /*var generator = new Generator();
            generator.GenerateParameters();

            var listRandomValue = new List<long>();
            var rnd = new Random();
            for (int i = 0; i < 10000000; i++)
            {
                listRandomValue.Add(generator.Next());
            }

            var hisrogramm = new Histogramm.Histogramm();
            hisrogramm.InitializeHistogram(listRandomValue);

            using (StreamWriter outputFile = new StreamWriter(@"C:\Users\myacc\Desktop\histogramm.txt"))
            {
                outputFile.Write(hisrogramm.BuildHistorgramm());
            }*/

            /*var generator = new Generator();
            generator.GenerateParameters();
            generator.GetSubsequence(10);

            var tuple = generator.GetParameters();
            generator.SetParameters(tuple.a, tuple.b, tuple.c);

            generator.GetSubsequence(15);

            tuple = generator.GetParameters();
            generator.SetParameters(tuple.a, tuple.b, tuple.c);

            generator.GetSubsequence(10);

            generator.GetSubsequence(5);*/

            /*var generator = new Generator();
            generator.GenerateParameters();

            var str = "пид";
            var encodeStr = "";
            var decodeStr = "";


            foreach(var symbol in str)
            {
                var num = generator.Next();
                encodeStr += (char)(num ^ (int)symbol);
            }
            Console.WriteLine(encodeStr);

            var tuple = generator.GetParameters();
            generator.SetParameters(tuple.a, tuple.b, tuple.c);

            foreach (var symbol in encodeStr)
            {
                var num = generator.Next();
                decodeStr += (char)(num ^ (int)symbol);
            }
            Console.WriteLine(decodeStr);*/


            Console.WriteLine(StaticData.PathKeyFale);
            var menu = new ConsoleMenu();
            menu.CallMenu();

        }
    }
}
