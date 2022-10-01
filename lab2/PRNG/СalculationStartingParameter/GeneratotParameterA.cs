using lab2.PRNG.ParameterInterface;

namespace lab2.PRNG.СalculationStartingParameter
{
    public class GeneratorParameterA : ParameterA
    {
        public long GenerateParameter(long m)
        {
            var randomValue = System.DateTime.Now.Millisecond + 5;
            while (randomValue % 4 != 1)
                randomValue += 1;
            return randomValue;
        }
    }
}
