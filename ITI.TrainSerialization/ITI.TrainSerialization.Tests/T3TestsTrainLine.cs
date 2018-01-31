using ITI.TrainSerialization.Interfaces;
using NUnit.Framework;
using System;
using System.Linq;

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

            Assert.That(t1.Assignment, Is.Null);
            Assert.That(t2.Assignment, Is.Null);

            ILine l = s.AddLine("RER A");

            t1.AssignTo(l);
            Assert.That(t1.Assignment, Is.SameAs(l));
            Assert.That(t2.Assignment, Is.Null);
            Assert.AreEqual(l.Trains.Count(), 1);
            Assert.That(l.Trains.Single(), Is.SameAs(t1));
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

            Assert.That(t1.Assignment, Is.SameAs(l1));

            t1.AssignTo(l2);
            Assert.That(t1.Assignment, Is.SameAs(l2));
            Assert.AreEqual(l1.Trains.Count(), 0);
            Assert.That(l2.Trains.Single(), Is.SameAs(t1));
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

            Assert.DoesNotThrow(() => t1.AssignTo(l1));
            Assert.Throws<ArgumentException>(() => t1.AssignTo(l2));
        }

        [Test]
        public void T4_assigning_a_train_to_a_null_line_removes_its_assignment()
        {
            ICity s1 = CityFactory.CreateCity("Paris");
            ICompany c1 = s1.AddCompany("SNCF");
            ILine l1 = s1.AddLine("RER A");
            ITrain t1 = c1.AddTrain("RER1");

            Assert.DoesNotThrow(() => t1.AssignTo(null));

            t1.AssignTo(l1);
            Assert.That(t1.Assignment, Is.SameAs(l1));

            t1.AssignTo(null);
            Assert.That(t1.Assignment, Is.Null);
            Assert.AreEqual(l1.Trains.Count(), 0);
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
            Assert.AreEqual(l1.Trains.Count(), 2);
            Assert.That(l1.Trains.Contains(t1));
            Assert.That(l1.Trains.Contains(t2));

            t1.AssignTo(null);
            Assert.That(l1.Trains.Single(), Is.SameAs(t2));

        }
    }
}