using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingRecord 
{
    private float instant;
    private float shootingRotationZ;
    
    public ShootingRecord(float _instant, float _rotationZ)
    {
        this.instant = _instant;
        this.shootingRotationZ = _rotationZ;
    }
    
    
}
