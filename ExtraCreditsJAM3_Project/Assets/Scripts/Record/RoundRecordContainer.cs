using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundRecordContainer
{
    public readonly List<MovementRecord> roundMovementRecords;
    public readonly List<ShootingRecord> roundShootingRecords;
    public RoundRecordContainer(List<MovementRecord> roundMovementRecords, List<ShootingRecord> roundShootingRecords)
    {
        this.roundMovementRecords = roundMovementRecords;
        this.roundShootingRecords = roundShootingRecords;
    }

    protected bool Equals(RoundRecordContainer other)
    {
        return Equals(roundMovementRecords, other.roundMovementRecords) && Equals(roundShootingRecords, other.roundShootingRecords);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((RoundRecordContainer) obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return ((roundMovementRecords != null ? roundMovementRecords.GetHashCode() : 0) * 397) ^ (roundShootingRecords != null ? roundShootingRecords.GetHashCode() : 0);
        }
    }
}
