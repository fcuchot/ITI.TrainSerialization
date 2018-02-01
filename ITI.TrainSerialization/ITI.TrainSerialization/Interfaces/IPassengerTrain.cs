using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITI.TrainSerialization.Interfaces;
namespace ITI.TrainSerialization.Interfaces
{
    public interface IPassengerTrain : ITrain
    {
        int NbFirstClassSeats { get; }

        int NbEconomicClassSeats { get; }
        int Capacity { get; }
        ITrain Train { get; }
    }
}
