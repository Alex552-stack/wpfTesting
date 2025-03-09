using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfTestGraph.Abstractions;
using WpfTestGraph.Enums;

namespace WpfTestGraph.Classes;

public class BasicNode : AbstractNode
{
    private int _value;
    public override int Value
    {
        get => _value;
        set
        {
            _value = value;
            State = State.Modified;
        }
    }

    public override void Traverse(List<string> steps)
    {
        //check sa nu fie asta destinatia
        if (steps.Count == 1)
        {
            //asta este destinatia. throw
            throw new Exception("Destinatia a primit comanda de transfer");
        }

        if (steps.Count == 0)
        {
            throw new Exception($"Nod-ul {Name} a primit comanda de transfer cu 0 pasi ramasi");
        }

        if (steps.First() != this.Name)
        {
            throw new Exception($"Nod-ul {Name} a primit comanda element-ului {steps.First()}");
        }
        
        steps.Remove(this.Name);
        
        if (steps.Count != 0)
        {
            var conn = Connections.Find(c => c.Name == steps.First());
            if (conn == null)
            {
                throw new Exception($"Nod-ul {this.Name} nu are o legatura cu {steps.First()}");
            }
            
            conn.Transfer();
        }
    }

    public TextBlock Display { get; private set; } // UI Element
    
    public BasicNode(string name,int value, double x, double y, Canvas canvas)
    {
        _value = value;
        Coords = new Tuple<double, double>(x, y);
        Name = name;

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
            Text = _value.ToString(),
            FontSize = 16,
            Background = Brushes.White,
        };
        Canvas.SetLeft(Display, x);
        Canvas.SetTop(Display, y);
        canvas.Children.Add(Display);
    }
    public override void Highlight()
    {
        if(VisualElement.Stroke != _highLightColor)
            VisualElement.Stroke = _highLightColor;
    }
    public override void RemoveHighlight()
    {
        if(VisualElement.Stroke != _normalColor)
            VisualElement.Stroke = _normalColor;
    }
    public override void UpdateUiElements()
    {
        if (State == State.Modified)
        {
            Display.Text = Value.ToString();
            Highlight();
        }
        else
        {
            RemoveHighlight();
        }
    }
}