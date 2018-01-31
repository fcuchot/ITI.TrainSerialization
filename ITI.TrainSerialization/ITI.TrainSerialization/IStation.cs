using System.Collections.Generic;

namespace ITI.TrainSerialization.Interfaces
{
    public interface IStation
    {
        string Name { get; }
        ICity City { get; }
        int X { get; }
        int Y { get; }
        IEnumerable<ILine> Lines { get; }
    }
}
