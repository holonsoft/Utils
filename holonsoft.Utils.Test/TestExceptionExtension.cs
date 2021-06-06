using holonsoft.Utils.Extensions;
using System;
using Xunit;

namespace holonsoft.Utils.Test
{
    public class TestExceptionExtension
    {
	    [Fact]
	    public void TestExceptionFlattening()
	    {
		    const string dummyInnerInner = "Dummy inner inner exception";
		    const string dummyInner = "Dummy inner exception";
		    const string dummyOuter = "Dummy outer exception";


		    var exInnerInner = new AccessViolationException(dummyInnerInner);
		    var exInner = new ArgumentNullException(dummyInner, exInnerInner);
#pragma warning disable 612, 618
		    var exOuter = new ExecutionEngineException(dummyOuter, exInner);
#pragma warning restore 612, 618

		    var msg = exOuter.Flatten();

		    Assert.Equal("Dummy outer exception\r\nDummy inner exception\r\nDummy inner inner exception\r\n", msg);
	    }
	}
}
