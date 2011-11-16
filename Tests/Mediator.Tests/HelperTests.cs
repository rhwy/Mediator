using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Mediator;

namespace Mediator.Tests
{
    [TestFixture]
    public class HelperTests
    {
        [Test]
        public void should_get_valid_date_from_unix_ticks()
        {
            DateTime date = DateTime.Now.ToUniversalTime();
            long ticks = date.ToUnixTicks();

            DateTime fromTicks = new DateTime(1970, 1, 1).AddMilliseconds(ticks);
            Assert.That(fromTicks.ToString(),Is.EqualTo(date.ToString()));
        }
    }
}
