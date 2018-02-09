using ITI.TrainSerialization.Interfaces;
using NUnit.Framework;
using System;
using System.Linq;
using FluentAssertions;


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

            l1.Stations.Count().Should().Be(0);
            s1.Lines.Count().Should().Be(0);
            s2.Lines.Count().Should().Be(0);

            Action a1 = () => l1.AddBefore(null, null);
            a1.ShouldThrow<ArgumentException>();

            Action a2 = () => l1.AddBefore(s1, null);
            a2.ShouldNotThrow();

            l1.Next(s1).Should().BeNull();
            l1.Previous(s1).Should().BeNull();

            s1.Lines.Count().Should().Be(1);

            s1.Lines.Count().Should().Be( 1);
            s2.Lines.Count().Should().Be( 0);
            s1.Lines.Single().Should().BeSameAs(l1);

            Action a3 = () => l1.Next(s2);
            a3.ShouldThrow<ArgumentException>();

            Action a4 = () => l1.Previous(s2);
            a4.ShouldThrow<ArgumentException>();

            l1.Stations.Single().Should().BeSameAs(s1);

            Action a5 = () => l1.AddBefore(s2, s1);
            a5.ShouldNotThrow();

            l1.Stations.Count().Should().Be(2);

            s1.Lines.Count().Should().Be(1);
            s2.Lines.Count().Should().Be(1);

            l1.Next(s2).Should().BeSameAs(s1);
            l1.Previous(s1).Should().BeSameAs(s2);
            l1.Next(s1).Should().BeNull();
            l1.Previous(s2).Should().BeNull();
            s1.Lines.Single().Should().BeSameAs(l1);
            s2.Lines.Single().Should().BeSameAs(l1);

        }

        [Test]
        public void T2_station_can_be_on_2_lines()
        {
            ICity c = CityFactory.CreateCity("Paris");
            ILine l1 = c.AddLine("RER A");
            ILine l2 = c.AddLine("RER B");

            IStation s = c.AddStation("Opera", 0, 0);

            Action a1 = () => l1.AddBefore(s, null);
            a1.ShouldNotThrow();

            Action a2 = () => l2.AddBefore(s, null);
            a2.ShouldNotThrow();

            l1.Stations.Single().Should().BeSameAs(l2.Stations.Single());
            l1.Stations.Single().Should().BeSameAs(s);
            l2.Stations.Single().Should().BeSameAs(s);
            s.Lines.Count().Should().Be( 2);

            s.Lines.Contains(l1).Should().BeTrue();
            s.Lines.Contains(l2).Should().BeTrue();
        }

        [Test]
        public void T3_stations_cant_be_2_times_on_a_line()
        {
            ICity c = CityFactory.CreateCity("Paris");
            ILine l = c.AddLine("RER B");
            IStation s = c.AddStation("Opera", 0, 0);

            l.AddBefore(s);

            Action a1 = () => l.AddBefore(s);
            a1.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void T4_stations_can_be_removed()
        {
            ICity c = CityFactory.CreateCity("Paris");
            ILine l = c.AddLine("RER B");
            IStation s = c.AddStation("Opera", 0, 0);

            Action a1 = () => l.Remove(s);
            a1.ShouldThrow<ArgumentException>();

            l.AddBefore(s);

            Action a2 = () => l.Remove(s);
            a2.ShouldNotThrow();

            l.Stations.Count().Should().Be(0);
            s.Lines.Count().Should().Be(0);
        }
        [Test]
        public void T5_stations_can_be_removed_then_re_added()
        {
            ICity c = CityFactory.CreateCity("Paris");
            ILine l = c.AddLine("RER B");
            IStation s = c.AddStation("Opera", 0, 0);

            l.AddBefore(s);
            s.Lines.Count().Should().Be(1);

            l.Remove(s);
            s.Lines.Count().Should().Be(0);

            Action a1 = () => l.AddBefore(s);
            a1.ShouldNotThrow();

            l.Stations.Count().Should().Be(1);
            s.Lines.Count().Should().Be(1);

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

            l.Stations.Count().Should().Be( 5);
            l.Previous(s4).Should().BeNull();
            l.Next(s4).Should().BeSameAs(s3);
            l.Next(s3).Should().BeSameAs(s2);
            l.Next(s2).Should().BeSameAs(s1);
            l.Next(s1).Should().BeSameAs(s);
            l.Next(s).Should().BeNull();
        }
    }
}
