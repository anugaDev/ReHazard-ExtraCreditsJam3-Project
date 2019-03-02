using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform bulletPrefab;
    [SerializeField] private float 
        shadowSpeed,
        inPositionRadius,
        startMovingTime,
        startingWaitTime,
        loopPauseTime,
        bulletSpeed
        ;
    

    [HideInInspector] public RoundRecordContainer shadowActionsRecord;

    private MovementRecord actualPositionTarget;
    private ShootingRecord actualInQeueAction;

    private Rigidbody2D shadowRb;
    private bool isMoving, allActionsMade;
    
    // Start is called before the first frame update
    void Start()
    {
        shadowRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            UpdateMovement();
            CheckForPosition();
            CheckForShooting();
        }
        
    }
    public void SetBehaviour(RoundRecordContainer _shadowActionsRecord)
    {
        this.shadowActionsRecord = _shadowActionsRecord;
        
        actualPositionTarget = shadowActionsRecord.roundMovementRecords[0];
        actualInQeueAction = shadowActionsRecord.roundShootingRecords[0];

        transform.position = actualPositionTarget.position;

        StartCoroutine(WaitTime(startingWaitTime));
        allActionsMade = false;

    }

    private void UpdateMovement()
    {
        var direction = actualPositionTarget.position - transform.position;
        direction.Normalize();
        shadowRb.velocity = direction * shadowSpeed * Time.fixedDeltaTime;
    }

    private void UpdateRotation()
    {
        Vector3 newDir = Vector3.RotateTowards(transform.forward, actualPositionTarget.direction, Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    private void CheckForPosition()
    {
        if (!((actualPositionTarget.position - transform.position).magnitude < inPositionRadius)) return;
        if (actualPositionTarget ==
            shadowActionsRecord.roundMovementRecords[shadowActionsRecord.roundMovementRecords.Count - 1])
        {
            ResetShadow();
        }
        else
        {
            SetNextPosition();
        }
    }

    private void SetNextPosition()
    {
        actualPositionTarget =
            shadowActionsRecord.roundMovementRecords
            [shadowActionsRecord.roundMovementRecords.IndexOf(actualPositionTarget) + 1];
    }
    private void SetNextAction()
    {
        actualInQeueAction =
            shadowActionsRecord.roundShootingRecords
                [shadowActionsRecord.roundShootingRecords.IndexOf(actualInQeueAction) + 1];
    }

    private void CheckForShooting()
    {
        var actualTime = Time.time;
        if ((actualTime - startMovingTime) >= actualInQeueAction.instant && !allActionsMade) return;
        
        ShadowShoot();
        
    }

    private void ShadowShoot()
    {
        var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        var bulletBehaviour = bullet.GetComponent<BulletBehaviour>();
        
        var direction = new Vector2(Mathf.Cos(actualInQeueAction.shootingRotationZ),Mathf.Sin(actualInQeueAction.shootingRotationZ));
        bulletBehaviour.SetBullet(bulletSpeed,direction,bullet.GetComponent<Rigidbody2D>());


        if (actualInQeueAction ==
            shadowActionsRecord.roundShootingRecords[shadowActionsRecord.roundMovementRecords.Count - 1])
        {
            allActionsMade = true;
        }
        else
        {
            SetNextAction();
        }
    }

   

    



    private void ResetShadow()
    {
        actualPositionTarget = shadowActionsRecord.roundMovementRecords[0];
        actualInQeueAction = shadowActionsRecord.roundShootingRecords[0];

        WaitTime((loopPauseTime));
        
        transform.position = actualPositionTarget.position;
        allActionsMade = false;

    }

    IEnumerator WaitTime(float _waitTime)
    {
        isMoving = false;
        yield return new WaitForSeconds(_waitTime);
        isMoving = true;
        startMovingTime = Time.time;
    }
}
