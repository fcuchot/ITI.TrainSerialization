using ITI.TrainSerialization.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.TrainSerialization.Classes
{
    [Serializable]
   class Train : ITrain
    {
        string _name;
        [NonSerialized]
        ICompany _company;
        ILine _assignment;
        [NonSerialized]
        ICity _city;

        public Train(string name, ICompany company, ILine assignment)
        {
            Name = name;
            Company = company;
            Assignment = _assignment;
        }
        public Train(string name, ICompany company, ICity city)
        {
            Name = name;
            Company = company;
            Assignment = null;
            City = city;
        }

        public string Name { get => _name; private set => _name = value; }

        public ICompany Company { get => _company; private set => _company = value; }

        public ILine Assignment { get => _assignment; private set => _assignment = value; }

        public ICity City { get; private set; }

        public void AssignTo(ILine l)
        {
            if( l != null && l.City != _company.City ) throw new ArgumentException( "The line is not in the same city than the train." );

            if( _assignment != null ) ((List<ITrain>)_assignment.Trains).Remove( this );
            _assignment = l;
            if( l != null ) ((List<ITrain>)l.Trains).Add( this );
        }
    }
}
