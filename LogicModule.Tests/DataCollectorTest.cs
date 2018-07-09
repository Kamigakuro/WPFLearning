using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LogicModule.Tests
{
    [TestFixture]
    public class DataCollectorTest
    {
        private DataCollector collector;
        private Random _rand;
        [SetUp]
        public void SetUp()
        {
            collector = new DataCollector();
        }

        [Test]
        public void BattleType_IsWork()
        {
            string text = "\"battleType\": 1,";

            int res = collector.BattleType(text);

            Assert.That(res, Is.InRange(1, 1000));

        }
        [Test]
        public void DateTime_Test()
        {
            string text = "\"dateTime\": \"30.06.2018 12:23:16\",";

            string res = collector.DateTime(text);

            Assert.That(res, Is.EqualTo("30.06.2018 12:23:16"));

        }

    }
}
