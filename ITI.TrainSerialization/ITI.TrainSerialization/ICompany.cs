﻿namespace ITI.TrainEval.Interfaces
{
    public interface ICompany 
    {
        string Name { get; }
        ICity City { get; }
        ITrain AddTrain(string name);
        ITrain FindTrain(string name);
    }
}
