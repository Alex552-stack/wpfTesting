using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfTestGraph.Abstractions;
using WpfTestGraph.Enums;

namespace WpfTestGraph.Classes;

public class BasicConnection : AbstractConnections
{
    private readonly Line _line;
    public sealed override AbstractNode Source { get; set; }
    public sealed override AbstractNode Destination { get; set; }

    public BasicConnection(string name ,AbstractNode source, AbstractNode destination, Canvas canvas)
    {
        Source = source;
        Destination = destination;
        Name = name;
        
        _line = new Line
        {
            Stroke = Brushes.Black,
            StrokeThickness = 2,
            X1 = Canvas.GetLeft(Source.VisualElement) + 25,
            Y1 = Canvas.GetTop(Source.VisualElement) + 25,
            X2 = Canvas.GetLeft(Destination.VisualElement) + 25,
            Y2 = Canvas.GetTop(Destination.VisualElement) + 25
        };

        Source.Connections.Add(this);
        canvas.Children.Add(_line);
    }
    public override void Highlight()
    {
        if (_line.Stroke != _highLightColor)
            _line.Stroke = _highLightColor;
    }
    public override void RemoveHighlight()
    {
        if (_line.Stroke != _normalColor)
            _line.Stroke = _normalColor;
    }

    public override void UpdateUiElements()
    {
        if (State == State.Modified)
        {
            Highlight();
        }
        else
        {
            RemoveHighlight();
        }
    }
}