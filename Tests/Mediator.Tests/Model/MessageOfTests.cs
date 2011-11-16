using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Mediator.Tests.SampleData;

namespace Mediator.Tests.Model
{
    [TestFixture]
    public class MessageOfTests
    {
        [Test]
        public void should_get_valid_unix_timestamp_on_creation()
        {

            MessageOf<DateTime> message = new MessageOf<DateTime>(DateTime.UtcNow);
            DateTime date = MediatorCommonHelpers.ToUtcDateTime(message.TimeStamp);
            Assert.That(date.ToString(), Is.EqualTo(message.MessageItem.ToString()));
        }

        [Test]
        public void ensure_message_is_serializable()
        {
            DateTime sourceMessage = DateTime.UtcNow;
            MessageOf<DateTime> expected = new MessageOf<DateTime>(sourceMessage);
            MessageOf<DateTime> actual = TestHelper.GetNewSerializedCopyOfObject<MessageOf<DateTime>>(expected);
            Assert.That(actual,Is.EqualTo(expected));

        }

        [Test]
        public void ensure_message_with_source_is_serializable()
        {
            ComplexModel model = new ComplexModel(duration:new TimeSpan(10));
            MessageOf<ComplexModel> expected = new MessageOf<ComplexModel>(model);
            MessageOf<ComplexModel> actual = TestHelper.GetNewSerializedCopyOfObject<MessageOf<ComplexModel>>(expected);
            Assert.That(actual, Is.EqualTo(expected));
        }

    }
}
