using ITI.TrainSerialization.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.TrainSerialization.Classes
{
    internal class Station : IStation
    {
        string _name;
        ICity _city;
        int _x;
        int _y;
        IEnumerable<ILine> _lines;

        internal Station(string name, ICity city, int x, int y, IEnumerable<ILine> lines)
        {
            Name = name;
            City = city;
            X = x;
            Y = y;
            Lines = lines;
        }

        internal Station(string name, ICity city, int x, int y)
        {
            Name = name;
            City = city;
            X = x;
            Y = y;

        }

        public string Name { get => _name; set => _name = value; }
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public ICity City { get => _city; set => _city = value; }
        public IEnumerable<ILine> Lines { get => _lines; set => _lines = value; }
    }
}
