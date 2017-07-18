public interface ISelectable
{
    bool Indicator { get; set; }
    void ChangeColor(TeamTag tag);
}
