using ITI.TrainSerialization.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.TrainSerialization.Classes
{
    [Serializable]
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

        internal City(string name)
        {
            Name = name;
            StationList = new List<IStation>();
            CompanyList = new List<ICompany>();
            LineList = new List<ILine>();
        }

        public string Name
        {
            get { return _name; }
            private set
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
            if (name == null || name == String.Empty)
                throw new ArgumentException();

            var CompanyWithSameNameAlreadyExist = FindCompany(name);

            if (CompanyWithSameNameAlreadyExist != null)
                throw new ArgumentException("a company with the same name already exist for this city");

            ICompany newCompany = new Company(name, this);
            _companyList.Add(newCompany);

            return newCompany;
        }

        public ICompany FindCompany(string name)
        {
            if (_companyList.Any(item => item.Name == name))
                return _companyList.First(item => item.Name == name);

            return null;
        }
        public ILine AddLine(string name)
        {
            if (name == null || name == String.Empty)
                throw new ArgumentException();

            var LineWithSameNameAlreadyExist = FindLine(name);

            if (LineWithSameNameAlreadyExist != null)
                throw new ArgumentException("a line with the same name already exist for this city");

            ILine newLine = new Line(name, this);
            _lineList.Add(newLine);

            return newLine;
        }

        public ILine FindLine(string name)
        {
            if (_lineList.Any(item => item.Name == name))
                return _lineList.First(item => item.Name == name);

            return null;
        }
        public IStation AddStation(string name, int x, int y)
        {
            if (name == null || name == String.Empty)
                throw new ArgumentException();

            var StationWithSameNameAlreadyExist = FindStation(name);

            if (StationWithSameNameAlreadyExist != null)
                throw new ArgumentException("a line with the same name already exist for this city");

            IStation newStation = new Station(name, this, x, y);
            _stationList.Add(newStation);

            return newStation;
        }

        public IStation FindStation(string name)
        {
            if (_stationList.Any(item => item.Name == name))
                return _stationList.First(item => item.Name == name);

            return null;
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
