namespace ITI.TrainSerialization.Interfaces
{
    public interface ICity
    {
        string Name { get; }
        ICompany AddCompany(string name);
        ICompany FindCompany(string name);
        ILine AddLine(string name);
        ILine FindLine(string name);
        IStation AddStation(string name,int x,int y);
        IStation FindStation(string name);

    }
}
