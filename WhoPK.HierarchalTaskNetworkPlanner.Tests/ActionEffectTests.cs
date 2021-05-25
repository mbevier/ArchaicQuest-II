using System;
using WhoPK.HierarchalTaskNetworkPlanner;
using WhoPK.HierarchalTaskNetworkPlanner.Effects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WhoPK.HierarchalTaskNetworkPlanner.Tests
{
    [TestClass]
    public class ActionEffectTests
    {
        [TestMethod]
        public void SetsName_ExpectedBehavior()
        {
            var e = new ActionEffect<MyContext>("Name", EffectType.PlanOnly, null);

            Assert.AreEqual("Name", e.Name);
        }

        [TestMethod]
        public void SetsType_ExpectedBehavior()
        {
            var e = new ActionEffect<MyContext>("Name", EffectType.PlanOnly, null);

            Assert.AreEqual(EffectType.PlanOnly, e.Type);
        }

        [TestMethod]
        public void ApplyDoesNothingWithoutFunctionPtr_ExpectedBehavior()
        {
            var ctx = new MyContext();
            var e = new ActionEffect<MyContext>("Name", EffectType.PlanOnly, null);

            e.Apply(ctx);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = false)]
        public void ApplyThrowsIfBadContext_ExpectedBehavior()
        {
            var e = new ActionEffect<MyContext>("Name", EffectType.PlanOnly, null);

            e.Apply(null);
        }

        [TestMethod]
        public void ApplyCallsInternalFunctionPtr_ExpectedBehavior()
        {
            var ctx = new MyContext();
            var e = new ActionEffect<MyContext>("Name", EffectType.PlanOnly, (c, et) => c.Done = true);

            e.Apply(ctx);

            Assert.AreEqual(true, ctx.Done);
        }
    }
}
