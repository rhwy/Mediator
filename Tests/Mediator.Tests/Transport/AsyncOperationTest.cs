using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using System.Web;
using System.Web.Routing;
using Mediator.Tests.SampleData;

namespace Mediator.Tests.Transport
{
    [TestFixture]
    public class AsyncOperationTest
    {
        public void should_get_result()
        {
            var context = MockHelper.GetHttpContext();
            //AsynchOperation<string> op = new AsynchOperation<string>(null,null,null);
        }
    }
}
