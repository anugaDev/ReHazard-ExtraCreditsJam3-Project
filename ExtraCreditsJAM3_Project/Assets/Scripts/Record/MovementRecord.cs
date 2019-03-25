using UnityEngine;

public class MovementRecord
{
    public Vector3 position;
    public Vector2 direction;
    public MovementRecord(Vector3 position, Vector2 direction)
    {
        this.position = position;
        this.direction = direction;
    }
}
