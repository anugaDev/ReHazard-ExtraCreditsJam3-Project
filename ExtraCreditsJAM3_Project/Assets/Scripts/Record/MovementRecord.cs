using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRecord
{
    // Start is called before the first frame update
    private Vector3 position;
    private Vector2 direction;

    public MovementRecord(Vector3 _position, Vector2 _direction)
    {
        this.position = _position;
        this.direction = _direction;
    }
}
