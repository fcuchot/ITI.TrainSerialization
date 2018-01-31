using ITI.TrainSerialization.Utils;
using System.Collections.Generic;

namespace ITI.TrainSerialization.Interfaces
{
    public interface ILine
    {
        string Name { get; }
        ICity City { get; }
        IEnumerable<IStation> Stations { get; }
        IEnumerable<ITrain> Trains { get; }
        IStation Previous(IStation s);
        IStation Next(IStation s);
        void AddBefore(IStation toAdd, IStation before = null);
        void Remove(IStation s);
    }
}
