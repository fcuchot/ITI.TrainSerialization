using System;
using System.Linq;
using NUnit.Framework;
using ITI.TrainSerialization.Interfaces;
using ITI.TrainSerialization;
using FluentAssertions;
using System.Reflection;

namespace ITI.TrainSerialization.Tests
{
    [TestFixture]
    class T5TestsJsonSerialization
    {
        [Test]
       public void T1_type_and_properties_must_be_serializable(Type type)
        {
            // base case
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
         
    }
}