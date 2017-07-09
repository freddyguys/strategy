using UnityEngine;

public interface IMove
{
    void MoveTo(Vector3 position, GameObject target = null);
}
