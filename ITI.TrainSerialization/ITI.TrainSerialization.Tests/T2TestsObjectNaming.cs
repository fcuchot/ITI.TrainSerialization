using System;
using System.Linq;
using NUnit.Framework;
using ITI.TrainSerialization.Interfaces;
using ITI.TrainSerialization;
using FluentAssertions;
namespace ITI.TrainSerialization.Tests
{
    [TestFixture]
    class T2TestsObjectNaming
    {
        [Test]
        public void T1_companies_can_be_found_by_name()
        {
            ICity s = CityFactory.CreateCity("Paris");
            ICompany c1 = s.AddCompany("SNCF");

            s.FindCompany("SNCF").Should().BeSameAs(c1);
            s.FindCompany("RATP").Should().BeNull();
          
            ICompany c2 = s.AddCompany("RATP");
            s.FindCompany("SNCF").Should().BeSameAs(c1);
            s.FindCompany("RATP").Should().BeSameAs(c2);
            s.FindCompany("Transports de Lyon").Should().BeNull();
           

            ICompany c3 = s.AddCompany("Transports de Lyon");
            ICompany c4 = s.AddCompany("Transports de Marseille");
            ICompany c5 = s.AddCompany("Transports de Lille");

            s.FindCompany("SNCF").Should().BeSameAs(c1);
            s.FindCompany("RATP").Should().BeSameAs(c2);
            s.FindCompany("Transports de Lyon").Should().BeSameAs(c3);
            s.FindCompany("Transports de Marseille").Should().BeSameAs(c4);
            s.FindCompany("Transports de Lille").Should().BeSameAs(c5);
           

            var randomNames = Enumerable.Range(0, 20).Select(i => String.Format("n°{0} - {1}", i, Guid.NewGuid().ToString())).ToArray();
            var teachers = randomNames.Select(n => s.AddCompany(n)).ToArray();

            teachers.Should().BeEquivalentTo(randomNames.Select(n => s.FindCompany(n)));
            
        }

        [Test]
        public void T2_lines_can_be_found_by_name()
        {
            ICity c = CityFactory.CreateCity("Paris");

            ILine l1 = c.AddLine("1");
            c.FindLine("1").Should().BeSameAs(l1);
            c.FindLine("2").Should().BeNull();
          

            ILine l2 = c.AddLine("2");
            c.FindLine("1").Should().BeSameAs(l1);
            c.FindLine("2").Should().BeSameAs(l2);
            c.FindLine("3").Should().BeNull();
           

            ILine l3 = c.AddLine("3");
            ILine l4 = c.AddLine("4");
            ILine l5 = c.AddLine("5");

            c.FindLine("1").Should().BeSameAs(l1);
            c.FindLine("2").Should().BeSameAs(l2);
            c.FindLine("3").Should().BeSameAs(l3);
            c.FindLine("4").Should().BeSameAs(l4);
            c.FindLine("5").Should().BeSameAs(l5);
          

            var randomNames = Enumerable.Range(0, 20)
                                            .Select(i => String.Format("n°{0} - {1}", i, Guid.NewGuid().ToString()))
                                            .ToArray();
            var lines = randomNames.Select(n => c.AddLine(n)).ToArray();
            lines.Should().BeEquivalentTo(randomNames.Select(n => c.FindLine(n)));
           
        }

        [Test]
        public void T3_stations_can_be_found_by_name()
        {
            ICity s = CityFactory.CreateCity("Paris");

            IStation c1 = s.AddStation("Opera", 0, 0);
            s.FindStation("Opera").Should().BeSameAs(c1);
            s.FindStation("Chatelet").Should().BeNull();


            IStation c2 = s.AddStation("Chatelet", 1, 1);
            s.FindStation("Opera").Should().BeSameAs(c1);
            s.FindStation("Chatelet").Should().BeSameAs(c2);
            s.FindStation("Ivry").Should().BeNull();

            IStation c3 = s.AddStation("Ivry", 2, 2);
            IStation c4 = s.AddStation("Villejuif", 3, 3);
            IStation c5 = s.AddStation("Bourse", 4, 4);

            s.FindStation("Opera").Should().BeSameAs(c1);
            s.FindStation("Chatelet").Should().BeSameAs(c2);
            s.FindStation("Ivry").Should().BeSameAs(c3);
            s.FindStation("Villejuif").Should().BeSameAs(c4);
            s.FindStation("Bourse").Should().BeSameAs(c5);
        }
        [Test]
        public void T4_trains_can_be_found_by_name()
        {
            ICity s = CityFactory.CreateCity("Paris");

            ICompany c1 = s.AddCompany("Transports de Lyon");
            ICompany c2 = s.AddCompany("Transports de Marseille");

            ITrain t1 = c1.AddTrain("RER1");
            c1.FindTrain("RER1").Should().BeSameAs(t1);
            c2.FindTrain("RER1").Should().BeNull();
           

            ITrain t2 = c2.AddTrain("RER1");
            c2.FindTrain("RER1").Should().BeSameAs(t2);
            c1.FindTrain("RER1").Should().NotBeSameAs(t2);
            

            ITrain t3 = c1.AddTrain("RER2");
            ITrain t4 = c1.AddTrain("RER3");
            ITrain t5 = c1.AddTrain("RER4");

            c1.FindTrain("RER2").Should().BeSameAs(t3);
            c1.FindTrain("RER3").Should().BeSameAs(t4);
            c1.FindTrain("RER4").Should().BeSameAs(t5);

            c2.FindTrain("RER2").Should().BeNull();
            c2.FindTrain("RER3").Should().BeNull();
            c2.FindTrain("RER4").Should().BeNull();
          
        }
    }
}
