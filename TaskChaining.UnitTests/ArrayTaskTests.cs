using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TasksLib;

namespace TaskChaining.UnitTests
{
    [TestFixture]
    public class ArrayTaskTests
    {
        private ITaskChainingInterface taskClass;
        [SetUp]
        public void Init()
        {
            taskClass = new TaskChainingClass();
        }
        [Test]
        public async Task CreateMethod_ThrowsArgumentOutOfRangeException()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await taskClass.GenerateIntegerArray(-10, 0, 10));
            await Task.CompletedTask;
        }

        [Test]
        public async Task CreateMethod_ThrowsArgumentException()
        {
            Assert.ThrowsAsync<ArgumentException>(async () => await taskClass.GenerateIntegerArray(10, 20, 10));
            await Task.CompletedTask;
        }

        [Test]
        public async Task CreateArray_Test()
        {
            var array = await taskClass.GenerateIntegerArray(5, 0, 10);
            Assert.IsNotNull(array);
            Assert.AreEqual(5, array.Length);
        }

        [TestCase(new [] {0, 1, 2, 3, 4}, 10, ExpectedResult = new [] {0, 10, 20, 30, 40})]
        [TestCase(new [] {-10, -9, -5, 0, 0, 5, 1, 2}, 10, ExpectedResult = new [] {-100, -90, -50, 0, 0, 50, 10, 20})]
        [TestCase(new [] {0, -10, 20, 30, -50}, 2, ExpectedResult = new [] {0, -20, 40, 60, -100})]
        [TestCase(new[] { int.MinValue, 100000, 2, 3, 1, -15, -1555, 10, int.MaxValue }, 1, ExpectedResult = new[] { int.MinValue, 100000, 2, 3, 1, -15, -1555, 10, int.MaxValue })]
        public async Task<int []> GetMultiplyOfArray_Test(int[] array, int mult)
        {
            var actual = await taskClass.ReturnMultipliedArray(array, mult);
            return actual;
        }

        [TestCase(new [] {2, 3, 1}, ExpectedResult = new [] {1, 2, 3})]
        [TestCase(new[] { -2, 3, -1, 5 }, ExpectedResult = new[] { -2, -1, 3, 5 })]
        [TestCase(new[] { -1889, 3, 1, 88, 2009, 3030, -10000 }, ExpectedResult = new[] {-10000, -1889, 1, 3, 88, 2009 , 3030})]
        [TestCase(new[] {int.MinValue, 100000,  2, 3, 1, -15, -1555, 10, int.MaxValue }, ExpectedResult = new[] { int.MinValue, -1555, -15, 1, 2, 3, 10, 100000, int.MaxValue})]
        public async Task<int []> SortArray_Test(int[] array)
        {
            var actual = await taskClass.SortArrayAscending(array);

            return actual;
        }

        [TestCase(new [] {2, 3, 1}, ExpectedResult = 2)]
        [TestCase(new [] {1, 10, 12, -5, 22, 105, -1449}, ExpectedResult = -186)]
        [TestCase(new [] { int.MinValue, -1, 1, 20, int.MaxValue }, ExpectedResult =3)]
        [TestCase(new [] { int.MinValue, -2100, 1, 0, -10000, 500, 18993, int.MaxValue }, ExpectedResult = 924)]
        public async Task<double> AvgValue_Test(int[] array)
        {
            var actual = await taskClass.GetAverageValue(array);

            return actual;
        }

        [Test]
        public async Task MethodsThrowsArgumentNullExceptionForNullArrays_Test()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await taskClass.GetAverageValue(null));
            Assert.ThrowsAsync<ArgumentNullException>(async () => await taskClass.SortArrayAscending(null));
            Assert.ThrowsAsync<ArgumentNullException>(async () => await taskClass.ReturnMultipliedArray(null, 0));
            await Task.CompletedTask;
        }

        [Test]
        public async Task MethodsThrowsArgumentExceptionForEmptyArrays_Test()
        {
            var tab = new int[0];

            Assert.ThrowsAsync<ArgumentException>(async () => await taskClass.GetAverageValue(tab));
            Assert.ThrowsAsync<ArgumentException>(async () => await taskClass.SortArrayAscending(tab));
            Assert.ThrowsAsync<ArgumentException>(async () => await taskClass.ReturnMultipliedArray(tab, 0));
            await Task.CompletedTask;
        }

        [Test]
        public async Task MethodsThrowsStackOverFlowException_Test()
        {
            var tab = new int[3] {int.MinValue, 0, int.MaxValue};

            Assert.ThrowsAsync<StackOverflowException>(async () => await taskClass.ReturnMultipliedArray(tab, 3));
            await Task.CompletedTask;
        }
    }
}
