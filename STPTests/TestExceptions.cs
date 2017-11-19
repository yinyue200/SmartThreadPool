using Amib.Threading;
#if NETCOREAPP2_0
using Test = Xunit.FactAttribute;
using TestBase = SmartThreadPoolTests.NunitTestBase;
using System;
#else
using NUnit.Framework;
using TestBase=System.Object;
#endif


namespace SmartThreadPoolTests
{
    /// <summary>
    /// Summary description for TestExceptions.
    /// </summary>
    [TestFixture]
	[Category("TestExceptions")]
	public class TestExceptions : TestBase
    {
		private class DivArgs
		{
			public int x;
			public int y;
		}

		[Test]
		public void ExceptionThrowing() 
		{ 
			SmartThreadPool _smartThreadPool = new SmartThreadPool();

			DivArgs divArgs = new DivArgs();
			divArgs.x = 10;
			divArgs.y = 0;

			IWorkItemResult wir = 
				_smartThreadPool.QueueWorkItem(new WorkItemCallback(this.DoDiv), divArgs);

			try
			{
				wir.GetResult();
			}
			catch(WorkItemResultException wire)
			{
				Assert.IsTrue(wire.InnerException is System.DivideByZeroException);
				return;
			}
			catch(System.Exception e)
			{
                e.GetHashCode();
				Assert.Fail();
			}
			Assert.Fail();
		} 

		[Test]
		public void ExceptionReturning() 
		{ 
			bool success = true;

			SmartThreadPool _smartThreadPool = new SmartThreadPool();

			DivArgs divArgs = new DivArgs();
			divArgs.x = 10;
			divArgs.y = 0;

			IWorkItemResult wir = 
				_smartThreadPool.QueueWorkItem(new WorkItemCallback(this.DoDiv), divArgs);

            System.Exception e = null;
			try
			{
				wir.GetResult(out e);
			}
			catch (System.Exception ex)
			{
                ex.GetHashCode();
				success = false;
			}

			Assert.IsTrue(success);
			Assert.IsTrue(e is System.DivideByZeroException);
		}

        private object DoDiv(object state)
        {
            DivArgs divArgs = (DivArgs)state;
            return (divArgs.x / divArgs.y);
        }

        [Test]
		public void ExceptionType()
		{ 
			SmartThreadPool smartThreadPool = new SmartThreadPool();

	        var workItemResult = smartThreadPool.QueueWorkItem(new Func<int>(ExceptionMethod));

            smartThreadPool.WaitForIdle();

            Assert.IsInstanceOf<System.NotImplementedException>(workItemResult.Exception);

            smartThreadPool.Shutdown();
        }

	    public int ExceptionMethod()
        {
            throw new System.NotImplementedException();
        }
	}
}
