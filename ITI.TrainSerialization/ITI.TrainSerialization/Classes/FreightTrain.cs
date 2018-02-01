using ITI.PrimarySchool.Interfaces;
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

        public FreightTrain(int maximalCharge, string typeOfGoods, bool isPriority, string name, ICompany company, ILine assignement) : base(name, company, assignement)
        {
            MaximalCharge = maximalCharge;
            TypeOfGoods = typeOfGoods;
            IsPriority = isPriority;
            this.Name = name;
            this.Company = company;
            this.Assignment = assignement;
        }

        public int MaximalCharge { get => _maximalCharge; set => _maximalCharge = value; }
        public string TypeOfGoods { get => _typeOfGoods; set => _typeOfGoods = value; }
        public bool IsPriority { get => _isPriority; set => _isPriority = value; }
    }
}
