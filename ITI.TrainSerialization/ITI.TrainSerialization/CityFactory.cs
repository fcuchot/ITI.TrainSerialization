using ITI.TrainSerialization.Classes;
using ITI.TrainSerialization.Interfaces;
using System;

namespace ITI.TrainSerialization
{
    public static class CityFactory
    {
        public static ICity CreateCity(string name)
        {
            if(name == null || name == String.Empty)
            throw new ArgumentException();

            ICity newCity = new City(name);

            return newCity;
        }
    }
}