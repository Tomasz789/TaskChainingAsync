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
            int min = 0, max = 1;

            Console.WriteLine("Generating an array of integers: ");


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
