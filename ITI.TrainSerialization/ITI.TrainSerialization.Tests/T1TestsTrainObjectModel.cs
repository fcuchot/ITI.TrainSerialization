using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using NUnit.Framework;
using ITI.TrainSerialization.Interfaces;
using FluentAssertions;
namespace ITI.TrainSerialization.Tests
{
    [TestFixture]
     class T1TestsTrainObjectModel
    {
        [Test]
        public void T1_creating_named_cities()
        {
            Action a = () => CityFactory.CreateCity(null);
            a.ShouldThrow<ArgumentException>();
            Action ab = () => CityFactory.CreateCity(String.Empty);
            ab.ShouldThrow<ArgumentException>();         
            {
                ICity s = CityFactory.CreateCity("Paris");        
                s.Name.Should().BeEquivalentTo("Paris");
            }
            {
                var randomName = Guid.NewGuid().ToString();
                ICity s = CityFactory.CreateCity(randomName);
              
                s.Name.Should().BeEquivalentTo(randomName);
            }
            ICity s1 = CityFactory.CreateCity("getType");

            s1.GetType().GetProperty("Name").GetSetMethod().Should().BeNull("City.Name must NOT be writeable.");
            s1.GetType().GetConstructors(BindingFlags.Instance | BindingFlags.Public).Should().BeEmpty("Company must not expose any public constructors.");
 
        }

        [Test]
        public void T2_companies_are_created_by_cities_and_have_a_unique_name()
        {
            ICity s = CityFactory.CreateCity("Paris");

            Action a = () => s.AddCompany(null);
            a.ShouldThrow<ArgumentException>();
            Action ab = () => s.AddCompany(String.Empty);
            ab.ShouldThrow<ArgumentException>();
            

            ICompany c1 = s.AddCompany("SNCF");
            c1.City.Should().BeSameAs(s);
            c1.Name.Should().BeEquivalentTo("SNCF");
            Action a1 = () => s.AddCompany("SNCF");         
            a1.ShouldThrow<ArgumentException>();

            ICompany c2 = s.AddCompany("RATP");
            c2.City.Should().BeSameAs(s);
            c2.Name.Should().BeEquivalentTo("RATP");
            Action a2 = () => s.AddCompany("RATP");
            a2.ShouldThrow<ArgumentException>();

            c2.GetType().GetConstructors(BindingFlags.Instance | BindingFlags.Public).Should().BeEmpty("Company must not expose any public constructors.");
            c2.GetType().GetProperty("Name").GetSetMethod().Should().BeNull("Company.Name must NOT be writeable.");
            c2.GetType().GetProperty("City").GetSetMethod().Should().BeNull("Company.City must NOT be writeable.");
           
        }

        [Test]
        public void T3_lines_are_created_by_cities_and_have_a_unique_name()
        {
            ICity s = CityFactory.CreateCity("Paris");

            Action a = () => s.AddLine(null);
            a.ShouldThrow<ArgumentException>();
            Action ab = () => s.AddLine(String.Empty);
            ab.ShouldThrow<ArgumentException>();
           

            ILine c1 = s.AddLine("RER A");
            c1.City.Should().BeSameAs(s);
            c1.Name.Should().BeEquivalentTo("RER A");
            Action a1 = () => s.AddLine("RER A");
            a1.ShouldThrow<ArgumentException>();


            ILine c2 = s.AddLine("RER B");
            c2.City.Should().BeSameAs(s);
            c2.Name.Should().BeEquivalentTo("RER B");
            Action a2 = () => s.AddLine("RER B");
            a2.ShouldThrow<ArgumentException>();

            c1.GetType().GetProperty("Name").GetSetMethod().Should().BeNull("Line.Name must NOT be writeable.");
            c1.GetType().GetProperty("City").GetSetMethod().Should().BeNull("Line.City must NOT be writeable.");
            c1.GetType().GetConstructors(BindingFlags.Instance | BindingFlags.Public).Should().BeEmpty("Line must not expose any public constructors.");
         
        }

        [Test]
        public void T4_trains_are_created_by_companies_and_have_a_unique_name()
        {
            ICity s = CityFactory.CreateCity("Paris");
            ICompany c = s.AddCompany("SNCF");

            Action a = () => c.AddTrain(null);
            a.ShouldThrow<ArgumentException>();
            

            ITrain p1 = c.AddTrain("train1");
            p1.Company.Should().BeSameAs(c);
            p1.Name.Should().BeEquivalentTo("train1");
            p1.Assignment.Should().BeNull();
            Action a1 = () => c.AddTrain("train1");
            a1.ShouldThrow<ArgumentException>();

           
            ITrain p2 = c.AddTrain("train2");
            p2.Company.Should().BeSameAs(c);
            p2.Name.Should().BeEquivalentTo("train2");
            p2.Assignment.Should().BeNull();
            Action a2 = () => c.AddTrain("train2");
            a2.ShouldThrow<ArgumentException>();

            p1.GetType().GetProperty("Name").GetSetMethod().Should().BeNull("Train.Name must NOT be writeable.");
            p1.GetType().GetProperty("Assignment").GetSetMethod().Should().BeNull("Train.Assignment must NOT be writeable.");
            p1.GetType().GetProperty("City").GetSetMethod().Should().BeNull("Train.City must NOT be writeable.");
            p1.GetType().GetConstructors(BindingFlags.Instance | BindingFlags.Public).Should().BeEmpty("Train must not expose any public constructors.");
            
        }
        [Test]
        public void T5_stations_are_created_by_cities_and_have_a_unique_name()
        {
            ICity s = CityFactory.CreateCity("Paris");
            IStation p1 = s.AddStation("Opera", 0, 0);

            p1.City.Should().BeSameAs(s);
            p1.Name.Should().BeEquivalentTo("Opera");
            p1.X.Should().Be(0);
            p1.Y.Should().Be(0);
            Action a = () => s.AddStation("Opera", 0, 0);
            a.ShouldThrow<ArgumentException>();
            Action a1 = () => s.AddStation("Opera", 10, 0);
            a1.ShouldThrow<ArgumentException>();
            Action a2 = () => s.AddStation("Opera", 0, 10);
            a2.ShouldThrow<ArgumentException>();
          

            IStation p2 = s.AddStation("Chatelet", 1, 1);
            p2.City.Should().BeSameAs(s);
            p2.Name.Should().BeEquivalentTo("Chatelet");
            p2.X.Should().Be(0);
            p2.Y.Should().Be(0);
            Action a4 = () => s.AddStation("Chatelet", 0, 0);
            a4.ShouldThrow<ArgumentException>();
            Action a5 = () => s.AddStation("Chatelet", 10, 0);
            a5.ShouldThrow<ArgumentException>();
            Action a6 = () => s.AddStation("Chatelet", 0, 10);
            a6.ShouldThrow<ArgumentException>();

            Action a7 = () => s.AddStation("Same Place Station", 0, 0);
            a7.ShouldThrow<ArgumentException>();

            p1.GetType().GetProperty("Name").GetSetMethod().Should().BeNull("Lane.X must NOT be writeable.");
            p1.GetType().GetProperty("X").GetSetMethod().Should().BeNull("Lane.X must NOT be writeable.");
            p1.GetType().GetProperty("Y").GetSetMethod().Should().BeNull("Lane.Y must NOT be writeable.");
            p1.GetType().GetConstructors(BindingFlags.Instance | BindingFlags.Public).Should().BeEmpty("Train must not expose any public constructors.");

        }
        [Test]
        public void T6_line_with_no_stations_doesnt_throe()
        {
            ICity s = CityFactory.CreateCity("Paris");

            ILine p1 = s.AddLine("K");
            Action a = () => p1.Stations.Count();
            a.ShouldNotThrow();
          
        }
    
}
}
