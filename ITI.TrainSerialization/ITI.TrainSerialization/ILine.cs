using ITI.TrainEval.Utils;
using System.Collections.Generic;

namespace ITI.TrainEval.Interfaces
{
    public interface ILine
    {
        string Name { get; }
        ICity City { get; }
        IEnumerable<IStation> Stations { get; }
        IStation Previous(IStation s);
        IStation Next(IStation s);
        void Add(StationPositioning positioning, IStation s);
    }
}
