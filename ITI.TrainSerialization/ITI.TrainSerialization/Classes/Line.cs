using ITI.TrainSerialization.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.TrainSerialization.Classes
{
    [Serializable]
    class Line : ILine
    {
        string _name;
        ICity _city;
        LinkedList<IStation> _stations;
        List<ITrain> _trains;

        internal Line(string name, ICity city)
        {
            Name = name;
            City = city;
            _stations = new LinkedList<IStation>();
            _trains = new List<ITrain>();
        }

        public string Name { get => _name; private set => _name = value; }
        public ICity City { get => _city; private set => _city = value; }
        public IEnumerable<IStation> Stations { get => _stations; }
        public IEnumerable<ITrain> Trains { get => _trains; }

        public IStation Previous(IStation s)
        {
            if( s == null ) throw new ArgumentException();
            if( !_stations.Contains( (Station)s ) ) throw new ArgumentException();
            return _stations.Find( (Station)s ).Previous?.Value;
        }
       public IStation Next(IStation s)
        {
            if( s == null ) throw new ArgumentException();
            if( !_stations.Contains( (Station)s ) ) throw new ArgumentException();
            return _stations.Find( (Station)s ).Next?.Value;
        }
        public void AddBefore(IStation toAdd, IStation before = null)
        {
            if( toAdd == null ) throw new ArgumentException();
            if( _stations.Contains( (Station)toAdd ) ) throw new ArgumentException();

            if( before == null ) _stations.AddFirst((Station)toAdd );
            else            
                _stations.AddBefore( _stations.Find( before ),( Station)toAdd);

            ((Station)toAdd).AddLine( this );
        }
        public void Remove( IStation s )
        {
            if( s == null ) throw new ArgumentException();
            if( !_stations.Contains( (Station)s ) ) throw new ArgumentException();
            _stations.Remove( (Station)s );
            ((Station)s).RemoveLine( this );
        }
    }
}
