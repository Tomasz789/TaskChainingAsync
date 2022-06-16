using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using TasksLib;
#pragma warning disable S1118 // Utility classes should not have public constructors

namespace TaskChaining
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var rnd = new Random();
            var taskClass = new TaskChainingClass();
            int min, max;

            do
            {
                min = rnd.Next();
                max = rnd.Next();
            }
            while (min >= max);

            Console.WriteLine("Generating an array of integers: ");
            var generatedArray = taskClass.GenerateIntegerArray(10, min, max).Result;
            Console.WriteLine("Generated values: ");
            PrintCurrentArrayValues(generatedArray);
            Console.WriteLine("Average value: {0}", taskClass.GetAverageValue(generatedArray).Result);
            int mult = rnd.Next();
            Console.WriteLine("Array multiplied by: {0}", mult);
            Console.WriteLine();
            var multArray = taskClass.ReturnMultipliedArray(generatedArray, mult).Result;
            PrintCurrentArrayValues(multArray);
            Console.WriteLine("Average value: {0}", taskClass.GetAverageValue(multArray).Result);
            Console.WriteLine();
            Console.WriteLine("Array after sorting:");
            var sortedArray = taskClass.SortArrayAscending(multArray).Result;
            PrintCurrentArrayValues(sortedArray);
            Console.WriteLine("Average value: {0}", taskClass.GetAverageValue(sortedArray).Result);

            await Task.CompletedTask;

        }

        private static void PrintCurrentArrayValues(int [] array)
        {
            if (!array.Any())
            {
                Console.WriteLine("Array is empty - there's nothing to print!");
            }
            foreach (var item in array)
            {
                Console.Write(item + "\t");
            }

            Console.WriteLine();
        }
    }
}
