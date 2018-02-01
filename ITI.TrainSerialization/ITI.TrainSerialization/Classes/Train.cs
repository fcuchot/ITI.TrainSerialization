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

        public Train(string name, ICompany company, ILine assignment)
        {
            Name = name;
            Company = company;
            Assignment = _assignment;
        }

        public string Name { get => _name; set => _name = value; }
        public ICompany Company { get => _company; set => _company = value; }
        public ILine Assignment { get => _assignment; set => _assignment = value; }

        public void AssignTo(ILine l)
        {
            throw new NotImplementedException();
        }
    }
}
