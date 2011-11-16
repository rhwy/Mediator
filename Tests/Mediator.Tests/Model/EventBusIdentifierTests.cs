using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Mediator.Tests.Model
{
    [TestFixture]
    public class EventBusIdentifierTests
    {
        [Test]
        public void Should_get_valid_Key()
        {
            EventBusIdentifier id = new EventBusIdentifier("test",typeof(string));
            Assert.That(id.Key,Is.EqualTo("String-test"));
        }

        [Test]
        public void All_Constructors_should_provide_the_same_id_if_params_default()
        {
         EventBusIdentifier idAll = new EventBusIdentifier("default",typeof(object));
         EventBusIdentifier idWithName = new EventBusIdentifier("default");
         EventBusIdentifier idWithType = new EventBusIdentifier(typeof(object));


         Assert.That(idAll,Is.EqualTo(idWithName).And.EqualTo(idWithType));

        }
        [Test]
        public void Should_use_equalty_based_on_key()
        {
            EventBusIdentifier id1 = new EventBusIdentifier("test", typeof(string));
            EventBusIdentifier id2 = new EventBusIdentifier("test", typeof(string));

            Assert.That(id1, Is.EqualTo(id2));
        }

        [Test]
        public void verify_equalty_operator()
        {
            EventBusIdentifier id1 = new EventBusIdentifier("test", typeof(string));
            EventBusIdentifier id2 = new EventBusIdentifier("test", typeof(string));

            Assert.IsTrue(id1.Equals(id2));
        }
    }
}
