
using ITI.TrainSerialization.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.TrainSerialization.Classes
{
    class FreightTrain : Train, IFreightTrain
    {
        int _maximalCharge;
        string _typeOfGoods;
        bool _isPriority;

        public FreightTrain(int maximalCharge, string typeOfGoods, bool isPriority, string name, ICompany company, ILine assignement) 
            : base(name, company, assignement)
        {
            MaximalCharge = maximalCharge;
            TypeOfGoods = typeOfGoods;
            IsPriority = isPriority;
        }

        public int MaximalCharge { get; set; }
        public string TypeOfGoods { get; set; }
        public bool IsPriority { get; set; }
    }
}
