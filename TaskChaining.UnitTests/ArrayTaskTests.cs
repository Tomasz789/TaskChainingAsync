using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
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
        public void CreateMethod_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => taskClass.GenerateIntegerArray(-10, 0, 10));
        }

        [Test]
        public void CreateMethod_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => taskClass.GenerateIntegerArray(10, 20, 10));
        }

        [Test]
        public void CreateArray_Test()
        {
            var array = taskClass.GenerateIntegerArray(5, 0, 10);
            Assert.IsNotNull(array);
            Assert.AreEqual(5, array.Length);
        }

        [TestCase(new [] {0, 1, 2, 3, 4}, 10, ExpectedResult = new [] {0, 10, 20, 30, 40})]
        [TestCase(new [] {-10, -9, -5, 0, 0, 5, 1, 2}, 10, ExpectedResult = new [] {-100, -90, -50, 0, 0, 50, 10, 20})]
        [TestCase(new [] {0, -10, 20, 30, -50}, 2, ExpectedResult = new [] {0, -20, 40, 60, -100})]
        [TestCase(new[] { int.MinValue, 100000, 2, 3, 1, -15, -1555, 10, int.MaxValue }, 1, ExpectedResult = new[] { int.MinValue, 100000, 2, 3, 1, -15, -1555, 10, int.MaxValue })]
        public int [] GetMultiplyOfArray_Test(int[] array, int mult)
        {
            var actual = taskClass.ReturnMultipliedArray(array, mult);
            return actual;
        }

        [TestCase(new [] {2, 3, 1}, ExpectedResult = new [] {1, 2, 3})]
        [TestCase(new[] { -2, 3, -1, 5 }, ExpectedResult = new[] { -2, -1, 3, 5 })]
        [TestCase(new[] { -1889, 3, 1, 88, 2009, 3030, -10000 }, ExpectedResult = new[] {-10000, -1889, 1, 3, 88, 2009 , 3030})]
        [TestCase(new[] {int.MinValue, 100000,  2, 3, 1, -15, -1555, 10, int.MaxValue }, ExpectedResult = new[] { int.MinValue, -1555, -15, 1, 2, 3, 10, 100000, int.MaxValue})]
        public int[] SortArray_Test(int[] array)
        {
            var actual = taskClass.SortArrayAscending(array);

            return actual;
        }

        [TestCase(new [] {2, 3, 1}, ExpectedResult = 2)]
        [TestCase(new [] {1, 10, 12, -5, 22, 105, -1449}, ExpectedResult = -186)]
        [TestCase(new [] { int.MinValue, -1, 1, 20, int.MaxValue }, ExpectedResult =3)]
        [TestCase(new [] { int.MinValue, -2100, 1, 0, -10000, 500, 18993, int.MaxValue }, ExpectedResult = 924)]
        public double AvgValue_Test(int[] array)
        {
            var actual = taskClass.GetAverageValue(array);

            return actual;
        }

        [Test]
        public void MethodsThrowsArgumentNullExceptionForNullArrays_Test()
        {
            Assert.Throws<ArgumentNullException>(() => taskClass.GetAverageValue(null));
            Assert.Throws<ArgumentNullException>(() => taskClass.SortArrayAscending(null));
            Assert.Throws<ArgumentNullException>(() => taskClass.ReturnMultipliedArray(null, 0));
        }

        [Test]
        public void MethodsThrowsArgumentExceptionForEmptyArrays_Test()
        {
            var tab = new int[0];

            Assert.Throws<ArgumentException>(() => taskClass.GetAverageValue(tab));
            Assert.Throws<ArgumentException>(() => taskClass.SortArrayAscending(tab));
            Assert.Throws<ArgumentException>(() => taskClass.ReturnMultipliedArray(tab, 0));
        }

        [Test]
        public void MethodsThrowsStackOverFlowException_Test()
        {
            var tab = new int[3] {int.MinValue, 0, int.MaxValue};

            Assert.Throws<StackOverflowException>(() => taskClass.ReturnMultipliedArray(tab, 3));
        }
    }
}
