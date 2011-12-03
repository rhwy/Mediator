using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Mediator.Transport;
using Mediator.Tests.SampleData;

namespace Mediator.Tests.Transport
{
    [TestFixture]
    public class EventBusRouterTests
    {
        [Test]
        public void should_extract_handler_tokens()
        {
            EventBusIdentifier actual = EventBusRouter.ExtractIdentifier(MockHelper.GetRouterRequestContext("/mediator","string","test"));
            EventBusIdentifier expected = new EventBusIdentifier("test","string");

            Assert.That(actual,Is.EqualTo(expected));
        }
    }
}
