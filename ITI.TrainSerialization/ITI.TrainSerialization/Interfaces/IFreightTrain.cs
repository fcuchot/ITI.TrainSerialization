using ITI.TrainSerialization.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.PrimarySchool.Interfaces
{
    public interface IFreightTrain : ITrain
    {
        int MaximalCharge { get; }

        string TypeOfGoods { get; }

        bool IsPriority { get; }
    }
}
