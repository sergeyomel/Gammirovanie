using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using System.Collections.Specialized;

namespace Histogramm
{
    public  class Histogramm
    {
        private int countColumn = 100;
        private int maxHeightColumn = 300;
        private bool IsCropHistogramm = false;
        private int maxRangeFrequencyValue = 0;

        private List<long> _numbers;

        public void ChangeCroppingType() => IsCropHistogramm = !IsCropHistogramm;

        /// <summary>
        /// Инициализация списка данных для построения гистограммы.
        /// </summary>
        /// <param name="numbers"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void InitializeHistogram(List<long> numbers)
        {
            if(numbers == null)
            {
                throw new ArgumentNullException("Numbers is null");
            }

            _numbers = new List<long>(numbers);
            _numbers.Sort();
        }

        #region Начальные значения
        /// <summary>
        /// Поиск Минимального и Максимального значения данных для гистограммы.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private (long min, long max) SearchExtremeValues()
        {
            if (_numbers == null)
            {
                throw new ArgumentNullException("Numbers is null");
            }

            var min = _numbers.Min();
            var max = _numbers.Max();
            return (min, max);
        }

        /// <summary>
        /// Вычисление разницы Максимального и Минимального значений диапазона входных значений для гистограммы.
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        private long SearchDifferenceValue((long min, long max) range) => range.max - range.min;
        #endregion

        #region Относительная частота значений
        /// <summary>
        /// Нахождения относительной частоты элементов гистограммы.
        /// </summary>
        /// <returns></returns>
        private Dictionary<long, int > CalculatingRelativeFrequencyValue()
        {
            var tmpFrequency = _numbers.GroupBy(s => s)
                                    .OrderBy(v => v.Key)
                                    .ToDictionary(g => g.Key, g => g.Count());

            var frequency = new Dictionary<long, int>();
            foreach(var tuple in tmpFrequency)
            {
                frequency.Add(tuple.Key, (int)tuple.Value);
            }
            return frequency;
        }

        /// <summary>
        /// Поиск Минимального и Максимального значений относительной частоты элементов словаря.
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns></returns>
        private (int min, int max) SearchExtremeFrequency(Dictionary<long, int> frequency)
        {
            var minFreq = frequency.Values.Min();
            var maxFreq = frequency.Values.Max();
            return (minFreq, maxFreq);
        }

        /// <summary>
        /// Вычисление количества относительных частот в диапазоне.
        /// </summary>
        /// <param name="relativeFrequence"></param>
        /// <returns></returns>
        private List<(double limitValue, int numberOccurrences)> CalculatingLineSegmentFrequence(Dictionary<long, int> relativeFrequence)
        {
            var extremeValues = SearchExtremeValues();
            var valueIncrease = Math.Round(Convert.ToDouble(SearchDifferenceValue(extremeValues)) / countColumn, 3);
            var frequenceValue = CalculatingRelativeFrequencyValue();
            var extremeFrequency = SearchExtremeFrequency(frequenceValue);

            var bandingList = new List<(double limitValue, int numberOccurrences)>();

            var lowerRangeLimit = (double)extremeValues.min;

            for (int lsi = 0; lsi < countColumn; lsi++)
            {
                var upperRangeLimit = lowerRangeLimit + valueIncrease;
                int countElement = relativeFrequence.Where(x => x.Key >= lowerRangeLimit && x.Key < upperRangeLimit)
                                                    .Sum(x => x.Value);
                bandingList.Add((Math.Round(extremeValues.min + valueIncrease * lsi, 0), countElement));

                if(countElement > maxRangeFrequencyValue)
                    maxRangeFrequencyValue = countElement;

                lowerRangeLimit = upperRangeLimit;
            }

            return bandingList;
        }
        #endregion

        /// <summary>
        /// Рассчет высоты колонки посредством отношения относительной частоты диапазона к максимальной относительной частоте.
        /// </summary>
        /// <param name="freqValue"></param>
        /// <returns></returns>
        private int CalculateHeightColumn(int freqValue) =>(int)(maxHeightColumn  * freqValue / maxRangeFrequencyValue);

        public string BuildHistorgramm()
        {
            var sb = new StringBuilder();

            foreach(var band in CalculatingLineSegmentFrequence(CalculatingRelativeFrequencyValue()))
            {
                sb.Append(band.limitValue.ToString() + " |");
                sb.AppendLine(string.Concat(Enumerable.Repeat("#", CalculateHeightColumn(band.numberOccurrences))));
            }

            return sb.ToString();
        }

    }
}
