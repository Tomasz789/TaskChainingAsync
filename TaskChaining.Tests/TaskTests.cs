using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TasksLib;

namespace TaskChaining.Tests
{
    [TestFixture]
    public class TaskTests
    {
        private ITaskChainingInterface taskClass;
        [SetUp]
        public void Init()
        {
            taskClass = new TaskChainingClass();
        }
        [Test]
        public void CreateMethod_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => taskClass.GenerateIntegerArray(-10, 0, 10));
        }

        [Test]
        public void CreateMethod_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => taskClass.GenerateIntegerArray(10, 20, 10));
        }
    }
}
