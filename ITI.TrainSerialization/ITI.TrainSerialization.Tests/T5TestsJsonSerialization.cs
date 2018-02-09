using System;
using System.Linq;
using NUnit.Framework;
using ITI.TrainSerialization.Interfaces;
using ITI.TrainSerialization;
using FluentAssertions;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization;
using ITI.TrainSerialization.Classes;
namespace ITI.TrainSerialization.Tests
{
    
    [TestFixture]
 
    class T5TestsJsonSerialization
    {

        [Test]
        public void T1_type_and_properties_must_be_serializable()
        {
            var c1 = CreateTestCity();
            var c1Type = c1.GetType();
            c1Type.IsSerializable.Should().BeTrue(c1Type + " must be marked [Serializable]");

        }
        [Test]
        public void T2_city_is_serialized()
        {

            var c1 = CreateTestCity();
            var serializedCity = Serialization.JSONSerialization.Serialize(c1);

            serializedCity.Should().BeOfType<string>();


        }
        
        private static ICity CreateTestCity()
        {
            ICity city = CityFactory.CreateCity("Paris");
            ICompany company = city.AddCompany("ITICORP");
            ITrain train = company.AddTrain("TGV");

            return city;
        }

      
        [Test]
        public void T3_train_propoerties_should_be_serializable()
        {
            var citySerialized = @"{
   'Name': 'Paris',
'StationList' : [],
'CompanyList' : [
'Name', 'ITICORP'
],
'LineList' : []
  
 }";
            var cityDeserialized = Serialization.JSONSerialization.Deserialize<ICity>(citySerialized);

            cityDeserialized.Should().BeOfType<ICity>();
        }  
       
    }
}

