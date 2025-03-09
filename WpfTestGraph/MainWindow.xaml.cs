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
using WpfTestGraph.Abstractions;
using WpfTestGraph.Classes;
using WpfTestGraph.Enums;

namespace WpfTestGraph;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private AbstractNode[] nodes;
    private AbstractConnections[] conns;
    public MainWindow()
    {
        InitializeComponent();
        InitializeGraph();
    }

    private void ResetElements()
    {
        foreach (var node in nodes)
        {
            node.State = State.Unmodified;
        }
        
        foreach (var conn in conns)
        {
            conn.State = State.Unmodified;
        }
    }
    
    private void UpdateElements()
    {
        foreach (var node in nodes)
        {
            node.UpdateUiElements();
        }
        
        foreach (var conn in conns)
        {
            conn.UpdateUiElements();
        }
    }
    
    private void InitializeGraph()
    {
        // Create two nodes
        BasicNode node1 = new BasicNode( "PC", 10, 100, 100, GraphCanvas);
        BasicNode node2 = new BasicNode("BUS", 20, 300, 200, GraphCanvas);
        BasicNode node3 = new BasicNode("BUS2", 20, 500, 200, GraphCanvas);

        nodes =
        [
            node1,
            node2,
            node3
        ];

        // Create a connection between the two nodes
        BasicConnection connection = new BasicConnection("PdPCs",node1, node2, GraphCanvas);
        BasicConnection connection2 = new BasicConnection("Pd2PCs",node1, node3, GraphCanvas);
        conns =
        [
            connection,
            connection2
        ];
    }

    private void DoTransfer()
    {
        string inputText = InputTextBox.Text;
        var steps = inputText.Split(" ").ToList();

        if (steps.Count == 0)
            return;

        var startNode = nodes.FirstOrDefault(n => n.Name == steps.First());

        if (startNode is null)
            return;
        
        startNode.Traverse(steps);
    }

    private void OnSubmitClicked(object sender, RoutedEventArgs e)
    {
        ResetElements();

        DoTransfer();
        
        UpdateElements();
    }
}