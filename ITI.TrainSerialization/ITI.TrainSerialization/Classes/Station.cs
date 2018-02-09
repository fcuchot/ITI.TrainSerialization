using ITI.TrainSerialization.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.TrainSerialization.Classes
{
    [Serializable]
    internal class Station : IStation
    {
        string _name;
        ICity _city;
        int _x;
        int _y;
       readonly List<ILine> _lines;

        internal Station(string name, ICity city, int x, int y)
        {
            Name = name;
            City = city;
            X = x;
            Y = y;
            _lines = new List<ILine>();
        }

        public string Name { get => _name; private set => _name = value; }
        public int X { get => _x; private set => _x = value; }
        public int Y { get => _y; private set => _y = value; }
        public ICity City { get => _city; private set => _city = value; }
        public IEnumerable<ILine> Lines => _lines;
        internal void AddLine( Line l )
        {
            _lines.Add( l );
        }
        internal void RemoveLine( Line l )
        {
            _lines.Remove( l );
        }
    }
}
