using System;
using Amib.Threading;
using PriorityQueueTests;

namespace SmartThreadPoolTests
{
    internal class Assert
    {
        internal static void Throws<T>(Func<object> p)where T:Exception
        {
            Xunit.Assert.Throws<T>(p);
        }
        internal static void Throws<T>(Action p) where T : Exception
        {
            Xunit.Assert.Throws<T>(p);
        }

        internal static void IsTrue(bool isCanceled)
        {
            Xunit.Assert.True(isCanceled);
        }

        internal static void IsFalse(bool completed)
        {
            Xunit.Assert.False(completed);
        }


        internal static void AreEqual<T>(T v1, T v2)
        {
            Xunit.Assert.Equal(v1, v2);
        }

        internal static void AreEqual<T>(T v1, T counter1, string v2,params object[] obj)
        {
            Xunit.Assert.True(System.Collections.Generic.EqualityComparer<T>.Default.Equals(v1, counter1), string.Format(v2,obj));
        }

        internal static void Fail()
        {
            throw new Exception("Fail") { };
        }

        internal static void IsNull(object state)
        {
            Xunit.Assert.Null(state);
        }

        internal static void IsNotNull(object pi2, string v,params object[] wip)
        {
            Xunit.Assert.True(pi2 != null, string.Format(v, wip));
        }
        internal static void IsNotNull(object pi2)
        {
            Xunit.Assert.NotNull(pi2);
        }

        internal static void AreSame(object pi, object pi2, string v,params object[] wip)
        {
            Xunit.Assert.True(ReferenceEquals(pi, pi2), string.Format(v, wip));
        }

        internal static void True(bool v)
        {
            Xunit.Assert.True(v);
        }

        internal static void IsTrue(bool v1, string v2)
        {
            Xunit.Assert.True(v1, v2);
        }

        internal static void IsInstanceOf<T>(object exception)
        {
            Xunit.Assert.IsAssignableFrom<T>(exception);
        }


        internal static void AreSame(object priorityItem1, object priorityItem2)
        {
            Xunit.Assert.Same(priorityItem1, priorityItem2);
        }
    }
}