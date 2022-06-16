using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TasksLib
{
    public interface ITaskChainingInterface
    {
        Task<int[]> GenerateIntegerArray(int number, int min, int max);
        Task<int[]> ReturnMultipliedArray(int[] tab, int mult);
        Task<int[]> SortArrayAscending(int[] array);
        Task<int> GetAverageValue(int [] tab);
    }
    public class TaskChainingClass : ITaskChainingInterface
    {
        /// <summary>
        /// Creates a new array with values in range from min to max.
        /// </summary>
        /// <param name="number">Number of array items.</param>
        /// <param name="min">Min range value.</param>
        /// <param name="max">Max range value.</param>
        /// <returns>An array with a number value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">When number of array items is less than 0.</exception>
        /// <exception cref="ArgumentException">If value of max is greater than min.</exception>
        public async Task<int[]> GenerateIntegerArray(int number, int min, int max)
        {
            if (number < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            if (max < min)
            {
                throw new ArgumentException(nameof(max));
            }

            var random = new Random();
            var array = new int[number];

            for (int i = 0; i < number; i++)
            {
                array[i] = random.Next(min, max);
            }

            return await Task.FromResult(array);
        }

        /// <summary>
        /// Calculates an average value based on array values.
        /// </summary>
        /// <param name="tab">Array with values to calcluate avg value.</param>
        /// <returns>Average value.</returns>
        public async Task<int> GetAverageValue(int [] tab)
        {
            CheckArray(tab);
            int sum = 0;

            for (var i = 0; i < tab.Length; i++)
            {
                sum += tab[i];
            }

            int res =  sum / tab.Length;

            return await Task.FromResult(res);
        }

        /// <summary>
        /// Multiples all array values by
        /// </summary>
        /// <param name="tab">Array to multiply.</param>
        /// <param name="mult">Multiplication value. </param>
        /// <returns> A new multiplied array.</returns>
        public async Task<int[]> ReturnMultipliedArray(int [] tab, int mult)
        {
            CheckArray(tab);

            for (int i = 0; i < tab.Length; i++)
            {
                if (mult > 1 && (tab[i] == int.MinValue || tab[i] == int.MaxValue))
                {
                    throw new StackOverflowException();
                }
            }
            var array = new int[tab.Length];
            
            for (int i = 0; i < tab.Length; i++)
            {
                array[i] = mult * tab[i];
            }

            return await Task.FromResult(array);
        }

        /// <summary>
        /// Sorts array ascending.
        /// </summary>
        /// <param name="array">Array with values.</param>
        /// <returns>Sorted array.</returns>
        public async Task<int[]> SortArrayAscending(int[] array)
        {
            CheckArray(array);
            Array.Sort(array);
            return await Task.FromResult(array);
        }

        /// <summary>
        /// Check if array is not null or empty.
        /// </summary>
        /// <param name="array">Array to check.</param>
        /// <exception cref="ArgumentNullException">If array is null.</exception>
        /// <exception cref="ArgumentException">If array has no elements.</exception>
        private static void CheckArray(int[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (array.Length == 0)
            {
                throw new ArgumentException(nameof(array));
            }
        }
    }
}
