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
    /// Summary description for TestThreadIsBackground.
	/// </summary>
	[TestFixture]
    [Category("TestThreadIsBackground")]
	public class TestThreadIsBackground : TestBase
    {
		[Test]
        public void TestIsBackground()
		{
            CheckIsBackground(true);
		}

		[Test]
        public void TestNotIsBackground()
		{
            CheckIsBackground(false);
		}

        private static void CheckIsBackground(bool isBackground)
	    {
	        STPStartInfo stpStartInfo = new STPStartInfo();
	        stpStartInfo.AreThreadsBackground = isBackground;

	        SmartThreadPool stp = new SmartThreadPool(stpStartInfo);

            IWorkItemResult<bool> wir = stp.QueueWorkItem(() => GetCurrentThreadIsBackground());

	        bool resultIsBackground = wir.GetResult();

	        stp.WaitForIdle();

            Assert.AreEqual(isBackground, resultIsBackground);
	    }

	    private static bool GetCurrentThreadIsBackground()
	    {
	        return Thread.CurrentThread.IsBackground;
	    }
	}
}