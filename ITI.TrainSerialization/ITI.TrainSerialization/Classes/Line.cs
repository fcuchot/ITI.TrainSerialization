using ITI.TrainSerialization.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.TrainSerialization.Classes
{
    class Line : ILine
    {
        string _name;
        ICity _city;
        IEnumerable<IStation> _stations;
        IEnumerable<ITrain> _trains;

        internal Line(string name, ICity city, IEnumerable<IStation> stations, IEnumerable<ITrain> trains)
        {
            Name = name;
            City = city;
            Stations = stations;
            Trains = trains;
        }

        internal Line(string name, ICity city)
        {
            Name = name;
            City = city;
        }

        public string Name { get => _name; private set => _name = value; }
        public ICity City { get => _city; private set => _city = value; }
        public IEnumerable<IStation> Stations { get => _stations; set => _stations = value; }
        public IEnumerable<ITrain> Trains { get => _trains; set => _trains = value; }

        public IStation Previous(IStation s)
        {
            throw new NotImplementedException();
        }
       public IStation Next(IStation s)
        {
            throw new NotImplementedException();
        }
        public void AddBefore(IStation toAdd, IStation before = null)
        {
            throw new NotImplementedException();
        }
        public void Remove(IStation s)
        {
            throw new NotImplementedException();
        }
    }
}
