using ITI.TrainSerialization.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.TrainSerialization.Classes
{
   class Train : ITrain
    {
        string _name;
        ICompany _company;
        ILine _assignment;
        ICity _city;

        internal Train(string name, ICompany company, ILine assignment)
        {
            Name = name;
            Company = company;
            Assignment = _assignment;
        }
        internal Train(string name, ICompany company, ICity city)
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
            throw new NotImplementedException();
        }
    }
}
