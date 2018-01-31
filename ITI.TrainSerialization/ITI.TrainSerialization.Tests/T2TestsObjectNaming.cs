using System;
using System.Linq;
using NUnit.Framework;
using ITI.TrainSerialization.Interfaces;
using ITI.TrainSerialization;

namespace ITI.TrainEval.Tests
{
    [TestFixture]
    class T2TestsObjectNaming
    {
        [Test]
        public void T1_companies_can_be_found_by_name()
        {
            ICity s = CityFactory.CreateCity("Paris");
            ICompany c1 = s.AddCompany("SNCF");
            Assert.That(s.FindCompany("SNCF"), Is.SameAs(c1));
            Assert.That(s.FindCompany("RATP"), Is.Null);

            ICompany c2 = s.AddCompany("RATP");
            Assert.That(s.FindCompany("SNCF"), Is.SameAs(c1));
            Assert.That(s.FindCompany("RATP"), Is.SameAs(c2));
            Assert.That(s.FindCompany("Transports de Lyon"), Is.Null);

            ICompany c3 = s.AddCompany("Transports de Lyon");
            ICompany c4 = s.AddCompany("Transports de Marseille");
            ICompany c5 = s.AddCompany("Transports de Lille");

            Assert.That(s.FindCompany("SNCF"), Is.SameAs(c1));
            Assert.That(s.FindCompany("RATP"), Is.SameAs(c2));
            Assert.That(s.FindCompany("Transports de Lyon"), Is.SameAs(c3));
            Assert.That(s.FindCompany("Transports de Marseille"), Is.SameAs(c4));
            Assert.That(s.FindCompany("Transports de Lille"), Is.SameAs(c5));

            var randomNames = Enumerable.Range(0, 20).Select(i => String.Format("n°{0} - {1}", i, Guid.NewGuid().ToString())).ToArray();
            var teachers = randomNames.Select(n => s.AddCompany(n)).ToArray();
            CollectionAssert.AreEqual(teachers, randomNames.Select(n => s.FindCompany(n)));
        }

        [Test]
        public void T2_lines_can_be_found_by_name()
        {
            ICity c = CityFactory.CreateCity("Paris");

            ILine l1 = c.AddLine("1");
            Assert.That(c.FindLine("1"), Is.SameAs(l1));
            Assert.That(c.FindLine("1"), Is.Null);

            ILine l2 = c.AddLine("2");
            Assert.That(c.FindLine("1"), Is.SameAs(l1));
            Assert.That(c.FindLine("2"), Is.SameAs(l2));
            Assert.That(c.FindLine("3"), Is.Null);

            ILine l3 = c.AddLine("3");
            ILine l4 = c.AddLine("4");
            ILine l5 = c.AddLine("5");

            Assert.That(c.FindLine("1"), Is.SameAs(l1));
            Assert.That(c.FindLine("2"), Is.SameAs(l2));
            Assert.That(c.FindLine("3"), Is.SameAs(l3));
            Assert.That(c.FindLine("4"), Is.SameAs(l4));
            Assert.That(c.FindLine("5"), Is.SameAs(l5));

            var randomNames = Enumerable.Range(0, 20)
                                            .Select(i => String.Format("n°{0} - {1}", i, Guid.NewGuid().ToString()))
                                            .ToArray();
            var lines = randomNames.Select(n => c.AddLine(n)).ToArray();
            CollectionAssert.AreEqual(lines, randomNames.Select(n => c.FindLine(n)));
        }

        [Test]
        public void T3_stations_can_be_found_by_name()
        {
            ICity s = CityFactory.CreateCity("Paris");

            IStation c1 = s.AddStation("Opera", 0, 0);
            Assert.That(s.FindStation("Opera"), Is.SameAs(c1));
            Assert.That(s.FindStation("Chatelet"), Is.Null);

            IStation c2 = s.AddStation("Chatelet", 1, 1);
            Assert.That(s.FindStation("Opera"), Is.SameAs(c1));
            Assert.That(s.FindStation("Chatelet"), Is.SameAs(c2));
            Assert.That(s.FindStation("Ivry"), Is.Null);

            IStation c3 = s.AddStation("Ivry", 2, 2);
            IStation c4 = s.AddStation("Villejuif", 3, 3);
            IStation c5 = s.AddStation("Bourse", 4, 4);

            Assert.That(s.FindStation("Opera"), Is.SameAs(c1));
            Assert.That(s.FindStation("Chatelet"), Is.SameAs(c2));
            Assert.That(s.FindStation("Ivry"), Is.SameAs(c3));
            Assert.That(s.FindStation("Villejuif"), Is.SameAs(c4));
            Assert.That(s.FindCompany("Bourse"), Is.SameAs(c5));
        }
        [Test]
        public void T4_trains_can_be_found_by_name()
        {
            ICity s = CityFactory.CreateCity("Paris");

            ICompany c1 = s.AddCompany("Transports de Lyon");
            ICompany c2 = s.AddCompany("Transports de Marseille");

            ITrain t1 = c1.AddTrain("RER1");
            Assert.That(c1.FindTrain("RER1"), Is.SameAs(t1));
            Assert.That(c2.FindTrain("RER1"), Is.Null);

            ITrain t2 = c2.AddTrain("RER1");
            Assert.That(c2.FindTrain("RER1"), Is.SameAs(t2));
            Assert.That(c1.FindTrain("RER1"), Is.Not.SameAs(t2));

            ITrain t3 = c1.AddTrain("RER2");
            ITrain t4 = c1.AddTrain("RER3");
            ITrain t5 = c1.AddTrain("RER4");

            Assert.That(c1.FindTrain("RER2"), Is.SameAs(t3));
            Assert.That(c1.FindTrain("RER3"), Is.SameAs(t4));
            Assert.That(c1.FindTrain("RER4"), Is.SameAs(t5));

            Assert.That(c2.FindTrain("RER2"), Is.Null);
            Assert.That(c2.FindTrain("RER3"), Is.Null);
            Assert.That(c2.FindTrain("RER4"), Is.Null);
        }
    }
}