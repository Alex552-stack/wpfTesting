using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfAppTest.Abstractions;

namespace WpfAppTest.Classes;

public class Node : IFlowElement
{
    public string Value { get; set; }
    public List<IFlowElement> Inputs { get; } = new();
    public List<IFlowElement> Outputs { get; } = new();

    public TextBlock Display { get; private set; } // UI Element
    public Ellipse VisualElement { get; private set; } // Shape representation

    public Node(string value, double x, double y, Canvas canvas)
    {
        Value = value;

        // UI: Visual representation
        VisualElement = new Ellipse
        {
            Width = 50,
            Height = 50,
            Fill = Brushes.LightBlue,
            Stroke = Brushes.Black
        };
        Canvas.SetLeft(VisualElement, x - 15);
        Canvas.SetTop(VisualElement, y - 15);
        canvas.Children.Add(VisualElement);
        
        // UI: Display text
        Display = new TextBlock
        {
            Text = Value,
            FontSize = 16,
            Background = Brushes.White,
        };
        Canvas.SetLeft(Display, x);
        Canvas.SetTop(Display, y);
        canvas.Children.Add(Display);
    }

    public void TransferDataTo(IFlowElement target)
    {
        if (Outputs.Contains(target)) // Ensure connection exists
        {
            target.Value = this.Value;
            target.RefreshText();
        }
    }


    public void RefreshText()
    {
        Display.Text = Value;
    }
}