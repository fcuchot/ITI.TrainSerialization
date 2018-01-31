using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITI.TrainSerialization.Interfaces;
namespace ITI.PrimarySchool.Interfaces
{
    class IPassengerTrain : ITrain
    {
        public string Name { get; }

        public ICompany Company { get; }

        public ILine Assignment { get; }

        int NbFirstClassSeats { get; }

        int NbEconomicClassSeats { get; }
        int Capacity { get; }
        ITrain Train { get; }

        public void AssignTo(ILine l)
        {
            var a = 1;
        }
    }
}
