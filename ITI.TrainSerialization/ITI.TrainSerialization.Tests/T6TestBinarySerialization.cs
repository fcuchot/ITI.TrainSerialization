using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using ITI.TrainSerialization.Interfaces;
using ITI.TrainSerialization.Serialization;
using Newtonsoft.Json;
using FluentAssertions;


namespace ITI.TrainSerialization.Tests
{
    [TestFixture]
    class T6TestBinarySerialization
    {
        [Test]
        public void save_and_load_city()
        {
            ICity city = CreateTestCity();

            // Act
            Stream stream = BinarySerialization.Serialize(city);
            stream.Position = 0;
            ICity city2 = BinarySerialization.Deserialize<ICity>(stream);

            // Assert

            city2.Name.Should().BeEquivalentTo(city.Name);
            city2.FindCompany( "C01" ).Should().NotBeNull();
            city2.FindCompany( "C02" ).Should().NotBeNull();
            city2.FindCompany( "C01" ).City.Should().BeSameAs( city2 );
         
        }

        private static ICity CreateTestCity()
        {
            ICity city = CityFactory.CreateCity("First City");
            ICompany c1 = city.AddCompany("C01");
            ITrain t1 = c1.AddTrain("T01");
            ICompany c2 = city.AddCompany("C02");
            ITrain t2 = c2.AddTrain("T02");

            return city;
        }
    }
}

