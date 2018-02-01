using ITI.TrainSerialization.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.TrainSerialization.Classes
{
    class City : ICity
    {
        string _name;
        List<IStation> _stationList;
        List<ICompany> _companyList;
        List<ILine> _lineList;

        internal City(string name, List<IStation> stationList, List<ICompany> companyList, List<ILine> lineList)
        {
            Name = name;
            StationList = stationList;
            CompanyList = companyList;
            LineList = lineList;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    if (String.IsNullOrEmpty(value)) throw new ArgumentException();
                    _name = value;
                }
            }
        }

        public List<IStation> StationList
        {
            get { return _stationList; }
            set
            {
                if (_stationList != value)
                {
                    _stationList = value ?? throw new ArgumentException();
                }
            }
        }

        public List<ICompany> CompanyList
        {
            get { return _companyList; }
            set
            {
                if (_companyList != value)
                {
                    _companyList = value ?? throw new ArgumentException();
                }
            }
        }

        public List<ILine> LineList
        {
            get { return _lineList; }
            set
            {
                if (_lineList != value)
                {
                    _lineList = value ?? throw new ArgumentException();
                }
            }
        }

        public ICompany AddCompany(string name)
        {
            throw new NotImplementedException();
        }
        public ICompany FindCompany(string name)
        {
            throw new NotImplementedException();
        }
        public ILine AddLine(string name)
        {
            throw new NotImplementedException();
        }

        public ILine FindLine(string name)
        {
            throw new NotImplementedException();
        }
        public IStation AddStation(string name, int x, int y)
        {
            throw new NotImplementedException();
        }

        public IStation FindStation(string name)
        {
            throw new NotImplementedException();
        }
        public IStation FindNearestStation(int x, int y)
        {
            throw new NotImplementedException();
        }
        public bool CanGo(IStation from, IStation to)
        {
            throw new NotImplementedException();
        }
    }
}
