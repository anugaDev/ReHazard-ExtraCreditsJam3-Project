using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingRecord 
{
    public float instant;
    public Vector2 shootingDir;
    
    public ShootingRecord(float instant, Vector2 shootingDir)
    {
        this.instant = instant;
        this.shootingDir = shootingDir;
    }
    
    
}
