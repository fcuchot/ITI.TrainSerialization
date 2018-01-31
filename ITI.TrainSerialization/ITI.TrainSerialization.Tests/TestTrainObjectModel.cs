using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using NUnit.Framework;
using ITI.TrainSerialization.Interfaces;

namespace ITI.TrainSerialization.Tests
{
    [TestFixture]
    public class TestTrainObjectModel
    {
        [Test]
        public void T1_creating_named_cities()
        {
            Assert.Throws<ArgumentException>(() => CityFactory.CreateCity(null));
            Assert.Throws<ArgumentException>(() => CityFactory.CreateCity(String.Empty));
            {
                ICity s = CityFactory.CreateCity("Paris");
                Assert.That(s.Name, Is.EqualTo("Paris"));
            }
            {
                var randomName = Guid.NewGuid().ToString();
                ICity s = CityFactory.CreateCity(randomName);
                Assert.That(s.Name, Is.EqualTo(randomName));
            }
            ICity s1 = CityFactory.CreateCity("getType");

            Assert.That(s1.GetType().GetProperty("Name").GetSetMethod(), Is.Null, "City.Name must NOT be writeable.");
            Assert.That(s1.GetType().GetConstructors(BindingFlags.Instance | BindingFlags.Public), Is.Empty, "Company must not expose any public constructors.");
        }

        [Test]
        public void T2_companies_are_created_by_cities_and_have_a_unique_name()
        {
            ICity s = CityFactory.CreateCity("Paris");

            Assert.Throws<ArgumentException>(() => s.AddCompany(null));
            Assert.Throws<ArgumentException>(() => s.AddCompany(String.Empty));

            ICompany c1 = s.AddCompany("SNCF");
            Assert.That(c1.City, Is.SameAs(s));
            Assert.That(c1.Name, Is.EqualTo("SNCF"));
            Assert.Throws<ArgumentException>(() => s.AddCompany("SNCF"));

            ICompany c2 = s.AddCompany("RATP");
            Assert.That(c2.City, Is.SameAs(s));
            Assert.That(c2.Name, Is.EqualTo("RATP"));
            Assert.Throws<ArgumentException>(() => s.AddCompany("RATP"));

            Assert.That(c2.GetType().GetConstructors(BindingFlags.Instance | BindingFlags.Public), Is.Empty, "Company must not expose any public constructors.");
            Assert.That(c2.GetType().GetProperty("Name").GetSetMethod(), Is.Null, "Company.Name must NOT be writeable.");
            Assert.That(c2.GetType().GetProperty("City").GetSetMethod(), Is.Null, "Company.City must NOT be writeable.");
        }

        [Test]
        public void T3_lines_are_created_by_cities_and_have_a_unique_name()
        {
            ICity s = CityFactory.CreateCity("Paris");

            Assert.Throws<ArgumentException>(() => s.AddLine(null));
            Assert.Throws<ArgumentException>(() => s.AddLine(String.Empty));

            ILine c1 = s.AddLine("RER A");
            Assert.That(c1.City, Is.SameAs(s));
            Assert.That(c1.Name, Is.EqualTo("RER A"));
            Assert.Throws<ArgumentException>(() => s.AddLine("RER A"));

            ILine c2 = s.AddLine("RER B");
            Assert.That(c2.City, Is.SameAs(s));
            Assert.That(c2.Name, Is.EqualTo("RER B"));
            Assert.Throws<ArgumentException>(() => s.AddLine("RER B"));

            Assert.That(c1.GetType().GetProperty("Name").GetSetMethod(), Is.Null, "Line.Name must NOT be writeable.");
            Assert.That(c1.GetType().GetProperty("City").GetSetMethod(), Is.Null, "Line.Name must NOT be writeable.");
            Assert.That(c1.GetType().GetConstructors(BindingFlags.Instance | BindingFlags.Public), Is.Empty, "Line must not expose any public constructors.");
        }

        [Test]
        public void T4_trains_are_created_by_companies_and_have_a_unique_name()
        {
            ICity s = CityFactory.CreateCity("Paris");
            ICompany c = s.AddCompany("SNCF");
            Assert.Throws<ArgumentException>(() => c.AddTrain(null));

            ITrain p1 = c.AddTrain("train1");
            Assert.That(p1.Company, Is.SameAs(c));
            Assert.That(p1.Name, Is.EqualTo("train1"));
            Assert.That(p1.Assignment, Is.Null);
            Assert.Throws<ArgumentException>(() => c.AddTrain("train1"));

            ITrain p2 = c.AddTrain("train2");
            Assert.That(p2.Company, Is.SameAs(c));
            Assert.That(p2.Name, Is.EqualTo("train2"));
            Assert.That(p2.Name, Is.EqualTo("train1"));
            Assert.Throws<ArgumentException>(() => c.AddTrain("train2"));

            Assert.That(p1.GetType().GetProperty("Name").GetSetMethod(), Is.Null, "Train.Name must NOT be writeable.");
            Assert.That(p1.GetType().GetProperty("Assignment").GetSetMethod(), Is.Null, "Train.Assignment must NOT be writeable.");
            Assert.That(p1.GetType().GetProperty("City").GetSetMethod(), Is.Null, "Train.City must NOT be writeable.");
            Assert.That(p1.GetType().GetConstructors(BindingFlags.Instance | BindingFlags.Public), Is.Empty, "Train must not expose any public constructors.");
        }
        [Test]
        public void T5_stations_are_created_by_cities_and_have_a_unique_name()
        {
            ICity s = CityFactory.CreateCity("Paris");

            IStation p1 = s.AddStation("Opera", 0, 0);
            Assert.That(p1.City, Is.SameAs(s));
            Assert.That(p1.Name, Is.EqualTo("Opera"));
            Assert.That(p1.X, Is.EqualTo(0));
            Assert.That(p1.Y, Is.EqualTo(0));
            Assert.Throws<ArgumentException>(() => s.AddStation("Opera", 0, 0));
            Assert.Throws<ArgumentException>(() => s.AddStation("Opera", 10, 0));
            Assert.Throws<ArgumentException>(() => s.AddStation("Opera", 0, 10));

            IStation p2 = s.AddStation("Chatelet", 1, 1);
            Assert.That(p2.City, Is.SameAs(s));
            Assert.That(p2.Name, Is.EqualTo("Chatelet"));
            Assert.That(p2.X, Is.EqualTo(1));
            Assert.That(p2.Y, Is.EqualTo(1));
            Assert.Throws<ArgumentException>(() => s.AddStation("Chatelet", 0, 0));
            Assert.Throws<ArgumentException>(() => s.AddStation("Chatelet", 10, 0));
            Assert.Throws<ArgumentException>(() => s.AddStation("Chatelet", 0, 10));

            Assert.Throws<ArgumentException>(() => s.AddStation("Same Place Station", 0, 0));

            Assert.That(p1.GetType().GetProperty("Name").GetSetMethod(), Is.Null, "Lane.Name must NOT be writeable.");
            Assert.That(p1.GetType().GetProperty("X").GetSetMethod(), Is.Null, "Lane.X must NOT be writeable.");
            Assert.That(p1.GetType().GetProperty("Y").GetSetMethod(), Is.Null, "Lane.Y must NOT be writeable.");
            Assert.That(p1.GetType().GetConstructors(BindingFlags.Instance | BindingFlags.Public), Is.Empty, "Train must not expose any public constructors.");
        }
        [Test]
        public void T6_line_with_no_stations_doesnt_throe()
        {
            ICity s = CityFactory.CreateCity("Paris");

            ILine p1 = s.AddLine("K");
            Assert.DoesNotThrow(() => p1.Stations.Count());
        }
    
}
}
