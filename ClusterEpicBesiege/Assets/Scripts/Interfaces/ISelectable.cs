using UnityEngine;

public interface ISelectable
{
    Vector3 Positions { get; }
    bool Indicator { get; set; }
    IMove referenceIMove { get; }
}
