using System.Windows.Media;
using WpfTestGraph.Enums;

namespace WpfTestGraph.Abstractions;

public abstract class ElementBase
{
    public string Name { get; set; } = string.Empty;
    public State State { get; set; } = State.Unmodified;
    protected readonly SolidColorBrush _highLightColor = Brushes.Red;
    protected readonly SolidColorBrush _normalColor = Brushes.Black;
    
    public abstract void Highlight();
    public abstract void RemoveHighlight();
    public abstract void UpdateUiElements();
}