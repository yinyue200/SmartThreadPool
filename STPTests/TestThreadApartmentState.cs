using System.Threading;
#if NETCOREAPP2_0
using Test = Xunit.FactAttribute;
using TestBase = SmartThreadPoolTests.NunitTestBase;
#else
using NUnit.Framework;
using TestBase=System.Object;
#endif
using Amib.Threading;

namespace SmartThreadPoolTests
{
	/// <summary>
    /// Summary description for TestThreadApartment.
	/// </summary>
	[TestFixture]
    [Category("TestThreadApartment")]
	public class TestThreadApartmentState : TestBase
    {
		[Test]
		public void TestSTA()
		{
            CheckApartmentState(ApartmentState.STA);
		}

		[Test]
		public void TestMTA()
		{
            CheckApartmentState(ApartmentState.MTA);
		}

	    private static void CheckApartmentState(ApartmentState requestApartmentState)
	    {
	        STPStartInfo stpStartInfo = new STPStartInfo();
            stpStartInfo.ApartmentState = requestApartmentState;

	        SmartThreadPool stp = new SmartThreadPool(stpStartInfo);

	        IWorkItemResult<ApartmentState> wir = stp.QueueWorkItem(() => GetCurrentThreadApartmentState());

	        ApartmentState resultApartmentState = wir.GetResult();

	        stp.WaitForIdle();

	        Assert.AreEqual(requestApartmentState, resultApartmentState);
	    }

	    private static ApartmentState GetCurrentThreadApartmentState()
	    {
	        return Thread.CurrentThread.GetApartmentState();
	    }
	}
}