using System.Windows.Shapes;

namespace WpfTestGraph.Abstractions;

public abstract class AbstractNode : ElementBase
{
    public abstract int Value { get; set; }
    public List<AbstractConnections> Connections { get; set; } = [];
    public Tuple<double,double> Coords { get; set; }
    public Shape VisualElement { get; set; }
    public abstract void Traverse(List<string> steps);
}