using Microsoft.VisualStudio.TestTools.UnitTesting;
using hw08_prototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw08_prototype.Tests
{
    [TestClass()]
    public class StingraySquadTests
    {
        [TestMethod()]
        public void IMyCloneTest()
        {
            StingraySquad stringraySquad = new StingraySquad();
            StingraySquad cloned = stringraySquad.clone();
            Assert.IsNotNull(cloned);
            Assert.AreEqual(stringraySquad, cloned);
        }

        [TestMethod()]
        public void IMyCloneToStringTest()
        {
            StingraySquad stringraySquad = new StingraySquad();
            StingraySquad cloned = stringraySquad.clone();
            Assert.AreEqual(stringraySquad.ToString(), cloned.ToString());
        }

        [TestMethod()]
        public void IMyCloneEqualsTest()
        {
            StingraySquad stringraySquad = new StingraySquad();
            StingraySquad cloned = stringraySquad.clone();
            Assert.IsTrue(stringraySquad.Equals(cloned));
        }

        [TestMethod()]
        public void IMyCloneGetHashCodeTest()
        {
            StingraySquad stringraySquad = new StingraySquad();
            StingraySquad cloned = stringraySquad.clone();
            Assert.AreEqual(stringraySquad.GetHashCode(),cloned.GetHashCode());
        }

        [TestMethod()]
        public void IClonableTest()
        {
            StingraySquad stringraySquad = new StingraySquad();
            StingraySquad cloned = (StingraySquad)stringraySquad.Clone();
            Assert.IsNotNull(cloned);
            Assert.AreEqual(stringraySquad, cloned);
        }

        [TestMethod()]
        public void IClonableToStringTest()
        {
            StingraySquad stringraySquad = new StingraySquad();
            StingraySquad cloned = (StingraySquad)stringraySquad.Clone();
            Assert.AreEqual(stringraySquad.ToString(), cloned.ToString());
        }

        [TestMethod()]
        public void IClonableEqualsTest()
        {
            StingraySquad stringraySquad = new StingraySquad();
            StingraySquad cloned = (StingraySquad)stringraySquad.Clone();
            Assert.IsTrue(stringraySquad.Equals(cloned));
        }

        [TestMethod()]
        public void IClonableGetHashCodeTest()
        {
            StingraySquad stringraySquad = new StingraySquad();
            StingraySquad cloned = (StingraySquad)stringraySquad.Clone();
            Assert.AreEqual(stringraySquad.GetHashCode(), cloned.GetHashCode());
        }

    }
}