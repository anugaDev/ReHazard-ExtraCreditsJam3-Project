using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform bulletPrefab;
    [SerializeField] private float 
        shadowSpeed,
        shadowRotSpeed,
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
    private bool isMoving = false, allActionsMade;
    
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
            UpdateRotation();
            CheckForPosition();
            CheckForShooting();
        }
        
    }
    public void SetBehaviour(RoundRecordContainer _shadowActionsRecord)
    {
        this.shadowActionsRecord = _shadowActionsRecord;
        
        actualPositionTarget = shadowActionsRecord.roundMovementRecords[0];
        if (_shadowActionsRecord.roundShootingRecords.Count > 0)
            actualInQeueAction = shadowActionsRecord.roundShootingRecords[0];
        else allActionsMade = true;
        
        startMovingTime = Time.time;

        transform.position = actualPositionTarget.position;

        StartCoroutine(WaitTime(startingWaitTime));
        

    }

    private void UpdateMovement()
    {
        var direction = actualPositionTarget.position - transform.position;
        direction.Normalize();
        shadowRb.velocity = direction * shadowSpeed * Time.fixedDeltaTime;
    }

    private void UpdateRotation()
    {
      
        var rotationZ = Mathf.Atan2(actualPositionTarget.direction.y, actualPositionTarget.direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        
       

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

       
        if (!allActionsMade)
        {
            if ((actualTime - startMovingTime) >= actualInQeueAction.instant)
            {
                ShadowShoot();

            }
        }
       


    }

    private void ShadowShoot()
    {
        if (actualInQeueAction ==
            shadowActionsRecord.roundShootingRecords[shadowActionsRecord.roundShootingRecords.Count - 1])
        {
            
            allActionsMade = true;
        }
        else
        {
           
            SetNextAction();
        }
        var rotationZ = Mathf.Atan2(actualInQeueAction.shootingDir.y, actualInQeueAction.shootingDir.x) * Mathf.Rad2Deg;
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ)).GetComponent<BulletBehaviour>();
        
        
        
        bullet.SetBullet(bulletSpeed,actualInQeueAction.shootingDir,bullet.GetComponent<Rigidbody2D>());


       
        
        
    }

   

    



    private void ResetShadow()
    {
        
        actualPositionTarget = shadowActionsRecord.roundMovementRecords[0];
        if (shadowActionsRecord.roundShootingRecords.Count > 0)
        {
            actualInQeueAction = shadowActionsRecord.roundShootingRecords[0];
            allActionsMade = false;
        }
           
        else allActionsMade = true;

        shadowRb.velocity = Vector3.zero;
        
        StartCoroutine (WaitTime(loopPauseTime));
        
       

    }

    IEnumerator WaitTime(float _waitTime)
    {
       
        isMoving = false;
        yield return new WaitForSeconds(_waitTime);
        isMoving = true;
        startMovingTime = Time.time;
        
        transform.position = actualPositionTarget.position;
       
        
    }
}
