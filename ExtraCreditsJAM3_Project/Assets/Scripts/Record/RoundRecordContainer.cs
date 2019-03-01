using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundRecordContainer : MonoBehaviour
{

    private List<MovementRecord> roundMovementRecords;
    private List<ShootingRecord> roundShootingRecords;

    public RoundRecordContainer(List<MovementRecord> roundMovementRecords, List<ShootingRecord> roundShootingRecords)
    {
        this.roundMovementRecords = roundMovementRecords;
        this.roundShootingRecords = roundShootingRecords;
    }
}
