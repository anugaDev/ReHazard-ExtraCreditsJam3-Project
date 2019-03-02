using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingRecord 
{
    public float instant;
    public float shootingRotationZ;
    
    public ShootingRecord(float _instant, float _rotationZ)
    {
        this.instant = _instant;
        this.shootingRotationZ = _rotationZ;
    }
    
    
}
