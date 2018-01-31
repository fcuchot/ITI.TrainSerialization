using ITI.TrainSerialization.Interfaces;
using NUnit.Framework;
using System;
using System.Linq;

namespace ITI.TrainSerialization.Tests
{
    [TestFixture]
    class T4TestsLineStation
    {
        [Test]
        public void T1_stations_can_be_assigned_to_a_line()
        {
            ICity c = CityFactory.CreateCity("Paris");
            ILine l1 = c.AddLine("RER A");

            IStation s1 = c.AddStation("Opera", 0, 0);
            IStation s2 = c.AddStation("Chatelet", 1, 1);

            Assert.AreEqual(l1.Stations.Count(), 0);
            Assert.AreEqual(s1.Lines.Count(), 0);
            Assert.AreEqual(s2.Lines.Count(), 0);

            Assert.Throws<ArgumentException>(() => l1.AddBefore(null, null));

            Assert.DoesNotThrow(() => l1.AddBefore(s1, null));
            Assert.That(l1.Next(s1), Is.Null);
            Assert.That(l1.Previous(s1), Is.Null);

            Assert.AreEqual(s1.Lines.Count(), 1);
            Assert.AreEqual(s2.Lines.Count(), 0);
            Assert.That(s1.Lines.Single(), Is.SameAs(l1));

            Assert.Throws<ArgumentException>(() => l1.Next(s2));
            Assert.Throws<ArgumentException>(() => l1.Previous(s2));

            Assert.That(l1.Stations.Single(), Is.SameAs(s1));
            Assert.DoesNotThrow(() => l1.AddBefore(s2, s1));
            Assert.Equals(l1.Stations.Count(), 2);

            Assert.AreEqual(s1.Lines.Count(), 1);
            Assert.AreEqual(s2.Lines.Count(), 1);

            Assert.That(l1.Next(s2), Is.SameAs(s1));
            Assert.That(l1.Previous(s1), Is.SameAs(s2));
            Assert.That(l1.Next(s1), Is.Null);
            Assert.That(l1.Previous(s2), Is.Null);
            Assert.That(s1.Lines.Single(), Is.SameAs(l1));
            Assert.That(s2.Lines.Single(), Is.SameAs(l1));

        }

        [Test]
        public void T2_station_can_be_on_2_lines()
        {
            ICity c = CityFactory.CreateCity("Paris");
            ILine l1 = c.AddLine("RER A");
            ILine l2 = c.AddLine("RER B");

            IStation s = c.AddStation("Opera", 0, 0);

            Assert.DoesNotThrow(() => l1.AddBefore(s, null));
            Assert.DoesNotThrow(() => l2.AddBefore(s, null));

            Assert.That(l1.Stations.Single(), Is.SameAs(l2.Stations.Single()));
            Assert.That(l1.Stations.Single(), Is.SameAs(s));
            Assert.That(l2.Stations.Single(), Is.SameAs(s));
            Assert.AreEqual(s.Lines.Count(), 2);

            Assert.IsTrue(s.Lines.Contains(l1));
            Assert.IsTrue(s.Lines.Contains(l2));
        }

        [Test]
        public void T3_stations_cant_be_2_times_on_a_line()
        {
            ICity c = CityFactory.CreateCity("Paris");
            ILine l = c.AddLine("RER B");
            IStation s = c.AddStation("Opera", 0, 0);

            l.AddBefore(s);
            Assert.Throws<ArgumentException>(() => l.AddBefore(s));
        }

        [Test]
        public void T4_stations_can_be_removed()
        {
            ICity c = CityFactory.CreateCity("Paris");
            ILine l = c.AddLine("RER B");
            IStation s = c.AddStation("Opera", 0, 0);
            Assert.Throws<ArgumentException>(() => l.Remove(s));
            l.AddBefore(s);
            Assert.DoesNotThrow(() => l.Remove(s));
            Assert.AreEqual(l.Stations.Count(), 0);
            Assert.AreEqual(s.Lines.Count(), 0);
        }
        [Test]
        public void T5_stations_can_be_removed_then_re_added()
        {
            ICity c = CityFactory.CreateCity("Paris");
            ILine l = c.AddLine("RER B");
            IStation s = c.AddStation("Opera", 0, 0);

            l.AddBefore(s);
            Assert.AreEqual(s.Lines.Count(), 1);

            l.Remove(s);
            Assert.AreEqual(s.Lines.Count(), 0);

            Assert.DoesNotThrow(() => l.AddBefore(s));
            Assert.AreEqual(l.Stations.Count(), 01);
            Assert.AreEqual(s.Lines.Count(), 1);

        }
        [Test]
        public void T6_lines_should_start_and_end_with_null()
        {
            ICity c = CityFactory.CreateCity("Paris");
            ILine l = c.AddLine("RER B");
            IStation s = c.AddStation("0", 0, 0);
            IStation s1 = c.AddStation("1", 1, 0);
            IStation s2 = c.AddStation("2", 2, 0);
            IStation s3 = c.AddStation("3", 3, 0);
            IStation s4 = c.AddStation("4", 4, 0);
            l.AddBefore(s);
            l.AddBefore(s1);
            l.AddBefore(s2);
            l.AddBefore(s3);
            l.AddBefore(s4);

            Assert.AreEqual(l.Stations.Count(), 5);
            Assert.That(l.Previous(s), Is.Null);
            Assert.That(l.Next(s), Is.SameAs(s1));
            Assert.That(l.Next(s1), Is.SameAs(s2));
            Assert.That(l.Next(s2), Is.SameAs(s3));
            Assert.That(l.Next(s3), Is.SameAs(s4));
            Assert.That(l.Next(s4), Is.Null);
        }
    }
}