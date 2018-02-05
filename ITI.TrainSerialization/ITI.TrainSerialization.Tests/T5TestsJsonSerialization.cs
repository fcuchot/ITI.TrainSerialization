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
       public void T1_type_and_properties_must_be_serializable(Type type)
        {

            if (type.IsValueType || type == typeof(string)) return;
            type.IsSerializable.Should().BeTrue(type + " must be marked [Serializable]");
                     
            foreach (var propertyInfo in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (propertyInfo.PropertyType.IsGenericType)
                {
                    foreach (var genericArgument in propertyInfo.PropertyType.GetGenericArguments())
                    {
                        if (genericArgument == type) continue;
                        T1_type_and_properties_must_be_serializable(genericArgument);
                    }
                }
                else if (propertyInfo.GetType() != type)
                    T1_type_and_properties_must_be_serializable(propertyInfo.PropertyType);
            }
        }
        
        public void T2_train_station_must_be_serializable()
        {
           
            //City s = new City()
            //IStation station1 = s.AddStation("Opera", 0, 0);

            
            
        }
        public static void ShouldDeepEqual<T>(T expected, T actual)
        {
            Assert.IsInstanceOf(expected.GetType(), actual);
            var serializedExpected = Serialize(expected);
            var serializedActual = Serialize(actual);
            serializedExpected.Should().Equals(serializedActual);
            
        }
        [Test]
        public void T3_train_propoerties_should_be_serializable()
        {

        
        }  
        public static string Serialize<T>(T obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            try
            {
                var stream1 = new MemoryStream();
              //  var ser = JsonConvert.SerializeObject(obj);
                ser.WriteObject(stream1, obj);
                stream1.Position = 0;
                var streamReader = new StreamReader(stream1);
                return streamReader.ReadToEnd();
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }
    }
}