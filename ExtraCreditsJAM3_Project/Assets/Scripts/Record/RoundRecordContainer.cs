using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundRecordContainer : MonoBehaviour
{

    public List<MovementRecord> roundMovementRecords;
    public List<ShootingRecord> roundShootingRecords;

    public RoundRecordContainer(List<MovementRecord> roundMovementRecords, List<ShootingRecord> roundShootingRecords)
    {
        this.roundMovementRecords = roundMovementRecords;
        this.roundShootingRecords = roundShootingRecords;
    }
}
