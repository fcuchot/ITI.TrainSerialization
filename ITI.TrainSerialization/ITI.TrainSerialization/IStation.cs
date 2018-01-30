namespace ITI.TrainEval.Interfaces
{
    public interface IStation
    {
        string Name { get; }
        ICity City { get; }
        int X { get; }
        int Y { get; }

    }
}
