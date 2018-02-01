using ITI.PrimarySchool.Interfaces;
using ITI.TrainSerialization.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.TrainSerialization.Classes
{
    class PassengerTrain : Train, IPassengerTrain
    {
        int _nbFirstClassSeats;
        int _nbEconomicClassSeats;
        int _capacity;

        public PassengerTrain(int nbFirstClassSeats, int nbEconomicClassSeats, int capacity, string name, Company company, Line assignement) : base(name, company, assignement)
        {
            NbFirstClassSeats = nbFirstClassSeats;
            NbEconomicClassSeats = nbEconomicClassSeats;
            Capacity = capacity;
            this.Name = name;
            this.Company = company;
            this.Assignment = assignement;
        }

        public int NbFirstClassSeats { get => _nbFirstClassSeats; set => _nbFirstClassSeats = value; }
        public int NbEconomicClassSeats { get => _nbEconomicClassSeats; set => _nbEconomicClassSeats = value; }
        public int Capacity { get => _capacity; set => _capacity = value; }
        public ITrain Train => throw new NotImplementedException();
    }
}
