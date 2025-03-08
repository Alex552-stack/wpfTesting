using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAppTest.Classes;

namespace WpfAppTest;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private List<Node> _nodes = [];
    private List<FlowConnection> _arrows = [];

    public MainWindow()
    {
        InitializeComponent();

        // Create nodes
        var step1 = new Node("Step 1", 50, 100, MyCanvas);
        var step2 = new Node("Step 2", 200, 100, MyCanvas);
        var step3 = new Node("Step 3", 350, 100, MyCanvas);

        _nodes.AddRange([step1, step2, step3]);

        // Create directed connections
        _arrows.Add(new FlowConnection(step1, step2, MyCanvas));
        _arrows.Add(new FlowConnection(step2, step3, MyCanvas));
    }

    /// <summary>
    /// I do not think this is the way to go forward
    /// Basic example of choosing what and where goes.
    /// We could go this way, where the UI actually does the data transfer between the different locations.
    /// This should/would make it pretty easy to just hardCode an arrow to light up for each possible transfer.
    /// Being able to subscribe to some sort of event or state change(React style) would make this trivial, but i do not think that we have this option
    /// Modeling the whole thing in memory as a full Graph(here i have nodes and arrows, but the arrows are only visual) would also just allow us to add some state to each element to see if it changed or not.
    ///     Then we would need to check the state of each element to see if we should reDraw it or not but i do not think this will be a bottleneck(from what i know actually drawing is expensive, not 20-30, max 100 ifs that we will check)
    /// Another interesting possibility is having some sort of way of finding out from the logic part what elements changed. Maybe having some sort of static list <!--something--> that after each f10/f11 key press adds to the list what updated.
    ///     The ui would just read that list, iterate over it, and updated all the items that changed. What i see as a "change" is also going from unmodified -> modified / modified -> unmodified / modified -> modified
    /// Not going for some sort of datastructure that has the links between nodes well defined(and a class) would mean that each node needs to remember to who it links. The only way i see of managing this from the ui is by hardcoding the somehow like in this example
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Transfer(object sender, RoutedEventArgs e)
    {
        RemoveAllHighLight();

        if (sender is not Button button) return;
        
        switch (button.Content)
        {
            case "TransferFrom1To2":
                _nodes[0].TransferDataTo(_nodes[1]);
                _arrows[0].Highlight();
                break;
            case "TransferFrom2To3":
                _nodes[1].TransferDataTo(_nodes[2]);
                _arrows[1].Highlight();
                break;
        }
    }

    private void RemoveAllHighLight()
    {
        _arrows.ForEach(arrow => arrow.RemoveHighlight());
    }
}
