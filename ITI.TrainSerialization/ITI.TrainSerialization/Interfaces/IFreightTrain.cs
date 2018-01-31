using ITI.TrainSerialization.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.PrimarySchool.Interfaces
{
    class IFreightTrain : ITrain
    {
        int MaximalCharge { get; }

        string TypeOfGoods { get; }

        bool IsPriority { get; }

        public string Name { get; }

        public ICompany Company { get; }

        public ILine Assignment { get; }

        public void AssignTo(ILine l)
        {
            var b = 2;
        }
    }
}
