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

        internal Company(string name, ICity city)
        {
            Name = name;
            City = city;
            TrainList = new List<ITrain>();
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

        public ICity City
        {
            get { return _city; }
            private set
            {
                if (_city != value)
                {
                    _city = value ?? throw new ArgumentException();
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
                    _trainList = value ?? throw new ArgumentException();
                }
            }
        }

        public ITrain AddTrain(string name)
        {
            if (name == null || name == String.Empty)
                throw new ArgumentException();

            var TrainWithSameNameAlreadyExist = FindTrain(name);

            if (TrainWithSameNameAlreadyExist != null)
                throw new ArgumentException("a train with the same name already exist for this company");

            ITrain newTrain = new Train(name, this, this.City);
            _trainList.Add(newTrain);

            return newTrain;
        }
        public ITrain FindTrain(string name)
        {
            if (_trainList.Any(item => item.Name == name))
                return _trainList.First(item => item.Name == name);

            return null;
        }
    }
}
