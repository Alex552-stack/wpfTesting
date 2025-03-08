using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfAppTest.Abstractions;

namespace WpfAppTest.Classes;

public class FlowConnection
{
    private readonly Line _line;
    private readonly IFlowElement _from;
    private readonly IFlowElement _to;

    public FlowConnection(IFlowElement from, IFlowElement to, Canvas canvas)
    {
        _from = from;
        _to = to;
        _from.Outputs.Add(to);
        _to.Inputs.Add(from);

        _line = new Line
        {
            Stroke = Brushes.Black,
            StrokeThickness = 2,
            X1 = Canvas.GetLeft(((Node)_from).VisualElement) + 25,
            Y1 = Canvas.GetTop(((Node)_from).VisualElement) + 25,
            X2 = Canvas.GetLeft(((Node)_to).VisualElement) + 25,
            Y2 = Canvas.GetTop(((Node)_to).VisualElement) + 25
        };

        canvas.Children.Add(_line);
    }

    public void Highlight()
    {
        _line.Stroke = Brushes.Red;
    }

    public void RemoveHighlight()
    {
        _line.Stroke = Brushes.Black;
    }
}
