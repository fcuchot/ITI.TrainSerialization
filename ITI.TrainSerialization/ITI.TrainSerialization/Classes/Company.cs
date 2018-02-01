using ITI.TrainSerialization.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.TrainSerialization.Classes
{
    [Serializable]
    class Company : ICompany
    {
        string _name;
        List<ITrain> _trainList;
        ICity _city;


        internal Company(string name, ICity city, List<ITrain> trainList)
        {
            Name = name;
            City = city;
            TrainList = trainList;
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

        public ICity City
        {
            get { return _city; }
            set
            {
                if (_city != value)
                {
                    if (value != null) throw new ArgumentException();
                    _city = value;
                }
            }
        }
        public List<ITrain> TrainList
        {
            get { return _trainList; }
            set
            {
                if (_trainList != value)
                {
                    if (value != null) throw new ArgumentException();
                    _trainList = value;
                }
            }
        }

        public ITrain AddTrain(string name)
        {
            throw new NotImplementedException();
        }
        public ITrain FindTrain(string name)
        {
            throw new NotImplementedException();
        }
    }
}
