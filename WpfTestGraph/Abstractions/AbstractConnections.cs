using WpfTestGraph.Enums;

namespace WpfTestGraph.Abstractions;

public abstract class AbstractConnections : ElementBase
{
    public abstract AbstractNode Source { get; set; }
    public abstract AbstractNode Destination { get; set; }

    public void Transfer()
    {
        Destination.Value = Source.Value;
        State = State.Modified;
    }
}