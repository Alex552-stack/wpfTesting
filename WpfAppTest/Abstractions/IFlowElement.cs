namespace WpfAppTest.Abstractions;

public interface IFlowElement
{
    string Value { get; set; }
    List<IFlowElement> Inputs { get; }
    List<IFlowElement> Outputs { get; }

    public void RefreshText();

    public void TransferDataTo(IFlowElement target);
}