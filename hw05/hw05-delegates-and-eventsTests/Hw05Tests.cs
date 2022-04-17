using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace hw05_delegates_and_events.Tests
{
    internal class TestClass
    {
        public float f { get; set; }
        public TestClass(float f)
        {
            this.f = f;
        }
    }

    [TestClass()]
    public class Hw05Tests
    {
        [TestMethod()]
        public void GetMaxTest()
        {
            List<TestClass> list = new List<TestClass> {
                new TestClass(1.1f),
                new TestClass(2.1f),
                new TestClass(-4.1f)
            };

            var max = list.GetMax<TestClass>(e => e.f);
            Assert.AreEqual(max.f, 2.1f);
        }

        [TestMethod]
        public void GetMaxEmptyListTest()
        {
            List<TestClass> list = new List<TestClass> { };
            var max = list.GetMax<TestClass>(e => e.f);
            Assert.IsNull(max);
        }
    }
}