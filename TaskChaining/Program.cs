using System;
using System.Threading.Tasks;
using TasksLib;
#pragma warning disable S1118 // Utility classes should not have public constructors

namespace TaskChaining
{
    public class Program
    {
        static void Main(string[] args)
        {
            var taskClass = new TaskChainingClass();
            Task<int[]> generateArrayTask = Task.Run(() =>
                {
                    Task.Delay(1000);
                    return taskClass.GenerateIntegerArray(1, 0, 10);
                }
            );
            Task<int[]> multArrayTask = generateArrayTask.ContinueWith(task => taskClass.ReturnMultipliedArray(generateArrayTask.Result, 10));
            Task<int[]> sortTask = multArrayTask.ContinueWith(task => taskClass.SortArrayAscending(multArrayTask.Result));
            Task<int> findAvgTask = sortTask.ContinueWith(task => taskClass.GetAverageValue(sortTask.Result));

            Console.WriteLine("Array contains following values: ");
            foreach (var item in generateArrayTask.Result)
            {
                Console.Write(item + "\t");
            }
            Console.WriteLine("Average value of the array {0}", findAvgTask.Result);
        }
    }
}
