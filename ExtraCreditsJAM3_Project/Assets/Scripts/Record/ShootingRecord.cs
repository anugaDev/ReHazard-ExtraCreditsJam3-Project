using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingRecord 
{
    public float instant;
    public Vector2 shootingDir;
    
    public ShootingRecord(float _instant, Vector2 _shootingDir)
    {
        this.instant = _instant;
        this.shootingDir = _shootingDir;
    }
    
    
}
