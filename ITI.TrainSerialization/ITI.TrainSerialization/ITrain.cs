namespace ITI.TrainEval.Interfaces
{
    public interface ITrain
    {
        string Name { get; }
        ICompany Company { get; }
        ILine Assignment { get; }
        void AssignTo(ILine l);
    }
}
