using ITI.TrainSerialization.Interfaces;
using NUnit.Framework;
using System;
using System.Linq;
using FluentAssertions;

namespace ITI.TrainSerialization.Tests
{
    [TestFixture]
    class T3TestsTrainLine
    {
        [Test]
        public void T1_trains_can_be_assigned_to_a_line()
        {
            ICity s = CityFactory.CreateCity("Paris");
            ICompany c = s.AddCompany("SNCF");

            ITrain t1 = c.AddTrain("RER1");
            ITrain t2 = c.AddTrain("RER2");

            t1.Assignment.Should().BeNull();
            t2.Assignment.Should().BeNull();
          
            ILine l = s.AddLine("RER A");

            t1.AssignTo(l);
            t1.Assignment.Should().BeSameAs(l);
            t2.Should().BeNull();
            l.Trains.Count().Should().Be(1);
            l.Trains.Single().Should().BeSameAs(t1);
        }

        [Test]
        public void T2_when_a_train_is_assigned_to_a_line_he_losts_its_previous_line()
        {
            ICity s = CityFactory.CreateCity("Paris");
            ICompany c = s.AddCompany("SNCF");
            ILine l1 = s.AddLine("RER A");
            ILine l2 = s.AddLine("RER B");
            ITrain t1 = c.AddTrain("RER1");

            t1.AssignTo(l1);

            t1.Assignment.Should().BeSameAs(l1);
            
            t1.AssignTo(l2);
            t1.Assignment.Should().BeSameAs(l2);
            l1.Trains.Count().Should().Be(0);
            l2.Trains.Single().Should().BeSameAs(t1);
        }

        [Test]
        public void T3_trains_and_lines_must_belong_to_the_same_city()
        {
            ICity s1 = CityFactory.CreateCity("Paris");
            ICompany c1 = s1.AddCompany("SNCF");
            ILine l1 = s1.AddLine("RER A");
            ITrain t1 = c1.AddTrain("RER1");

            ICity s2 = CityFactory.CreateCity("Lyon");
            ICompany c2 = s2.AddCompany("SNCF");
            ILine l2 = s2.AddLine("RER A");
            ITrain t2 = c2.AddTrain("RER1");

            Action a1 = () => t1.AssignTo(l1);
            Action a2 = () => t1.AssignTo(l2);
            a1.ShouldNotThrow();
            a2.ShouldThrow<ArgumentException>();

        }

        [Test]
        public void T4_assigning_a_train_to_a_null_line_removes_its_assignment()
        {
            ICity s1 = CityFactory.CreateCity("Paris");
            ICompany c1 = s1.AddCompany("SNCF");
            ILine l1 = s1.AddLine("RER A");
            ITrain t1 = c1.AddTrain("RER1");

            Action a1 = () => t1.AssignTo(null);
            a1.ShouldNotThrow();

            t1.AssignTo(l1);
            t1.Assignment.Should().BeSameAs(l1);

            t1.AssignTo(null);
            t1.Assignment.Should().BeNull();
            l1.Trains.Count().Should().Be(0);
        }
        [Test]
        public void T5_line_can_have_mutiple_trains()
        {
            ICity s1 = CityFactory.CreateCity("Paris");
            ICompany c1 = s1.AddCompany("SNCF");
            ILine l1 = s1.AddLine("RER A");
            ITrain t1 = c1.AddTrain("RER1");
            ITrain t2 = c1.AddTrain("RER2");

            t1.AssignTo(l1);
            t2.AssignTo(l1);
            l1.Trains.Count().Should().Be(2);
            l1.Trains.Contains(t1).Should().BeTrue();
            l1.Trains.Contains(t2).Should().BeTrue();

            t1.AssignTo(null);
            l1.Trains.Single().Should().BeSameAs(t2);

        }
    }
}