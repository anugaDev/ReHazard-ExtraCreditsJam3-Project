using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBehaviour : MonoBehaviour
{
    [SerializeField] private float shadowSpeed;
    [SerializeField] private float shadowRotSpeed;
    [SerializeField] private float inPositionRadius;
    [SerializeField] private float startMovingTime;
    [SerializeField] private float startingWaitTime;
    [SerializeField] private float loopPauseTime;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletOffset;
    [SerializeField] private float timeBetweenChangingPositions;
    [SerializeField] private float windUpTime;
    
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private AudioClip shadowShootClip;
    [SerializeField] private Transform legs;
    [SerializeField] private Transform resetAffordance, deathShadowParticle;
    
    [HideInInspector] public RoundRecordContainer shadowActionsRecord;
    [HideInInspector] public Transform affordanceInstance;

    private float timeForPosChange;
    private MovementRecord actualPositionTarget;
    private ShootingRecord actualInQeueAction;
    private AudioSource shadowSource;
    private Animator shadowAnimator;
    
    private Rigidbody2D shadowRb;
    private bool isMoving, allActionsMade, allPositionsMade, windingUp;
    
    private void Start()
    {
        shadowSource = GetComponent<AudioSource>();
        shadowAnimator = GetComponent<Animator>();
        shadowRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isMoving) return;
        UpdateMovement();
        UpdateRotation();
        CheckForPosition();
        CheckForShooting();

        if (allActionsMade && allPositionsMade)
        {
            ResetShadow();
        }

    }
    public void SetBehaviour(RoundRecordContainer shadowActionsRecord)
    {
       this.shadowActionsRecord = shadowActionsRecord;
        
        actualPositionTarget = this.shadowActionsRecord.roundMovementRecords[0];
        if (shadowActionsRecord.roundShootingRecords.Count > 0)
            actualInQeueAction = this.shadowActionsRecord.roundShootingRecords[0];
        else allActionsMade = true;
        
        startMovingTime = Time.time;

        transform.position = actualPositionTarget.position;

        timeBetweenChangingPositions = GameManager.Instance.playerRec.recordTime;

        StartCoroutine(WaitTime(startingWaitTime));

        allPositionsMade = false;
    }

    private void UpdateMovement()
    {
        var direction = actualPositionTarget.position - transform.position;
        direction.Normalize();
        
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var q = Quaternion.AngleAxis(angle, Vector3.forward);
        legs.transform.rotation = Quaternion.Slerp(legs.transform.rotation, q, Time.fixedDeltaTime * shadowRotSpeed);
        
        if ((actualPositionTarget.position - transform.position).magnitude > inPositionRadius)
        {
            shadowRb.velocity = direction * shadowSpeed * Time.fixedDeltaTime;
            legs.gameObject.SetActive(true);
        }
        else
        {
            shadowRb.velocity = Vector3.zero;
            legs.gameObject.SetActive(false);
        }
    }
    private void UpdateRotation()
    {
        var angle = Mathf.Atan2(actualPositionTarget.direction.y, actualPositionTarget.direction.x) * Mathf.Rad2Deg;
        var q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.fixedDeltaTime * shadowRotSpeed);
    }

    private void CheckForPosition()
    {
        if (allPositionsMade) return;
        timeForPosChange += Time.deltaTime;
        if (!(timeForPosChange >= timeBetweenChangingPositions)) return;
        if (actualPositionTarget ==
            shadowActionsRecord.roundMovementRecords[shadowActionsRecord.roundMovementRecords.Count - 1])
        {
            allPositionsMade = true;
        }
        else
        {
            SetNextPosition();
        }

        timeForPosChange = 0;

    }
    private void CheckForShooting()
    {
        shadowAnimator.ResetTrigger("WindUp");
        shadowAnimator.ResetTrigger("Shoot");
        var actualTime = Time.time;

        if ((actualTime - startMovingTime) >= (actualInQeueAction.instant - windUpTime)  && !windingUp )
        {
            windingUp = true;
            shadowAnimator.SetTrigger("WindUp");
        }

        if (allActionsMade) return;
        if ((actualTime - startMovingTime) >= actualInQeueAction.instant)
        {
            ShadowShoot();
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
    private void ShadowShoot()
    {
        shadowSource.clip = shadowShootClip;
        shadowSource.Play();
        shadowAnimator.SetTrigger("Shoot");
        windingUp = false;
        
        var rotationZ = Mathf.Atan2(actualInQeueAction.shootingDir.y, actualInQeueAction.shootingDir.x) * Mathf.Rad2Deg;
        Vector3 direction = actualInQeueAction.shootingDir;
        
        var bullet = Instantiate(bulletPrefab, transform.position + (direction * bulletOffset) , Quaternion.Euler(0.0f, 0.0f, rotationZ)).GetComponent<BulletBehaviour>();
        bullet.SetBullet(bulletSpeed,actualInQeueAction.shootingDir,bullet.GetComponent<Rigidbody2D>());
        
        if (actualInQeueAction ==
            shadowActionsRecord.roundShootingRecords[shadowActionsRecord.roundShootingRecords.Count - 1])
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
        if (shadowActionsRecord.roundShootingRecords.Count > 0)
        {
            actualInQeueAction = shadowActionsRecord.roundShootingRecords[0];
            allActionsMade = false;
        }
           
        else allActionsMade = true;

        shadowRb.velocity = Vector3.zero;

        affordanceInstance = Instantiate(resetAffordance, actualPositionTarget.position, Quaternion.identity);
        
        GameManager.Instance.effectsToDestroy.Add(affordanceInstance);
        
        shadowAnimator.ResetTrigger("StartLoop");
        shadowAnimator.SetTrigger("EndLoop");
        StartCoroutine (WaitTime(loopPauseTime));

        allPositionsMade = false;

    }
    private IEnumerator WaitTime(float waitTime)
    {
        legs.gameObject.SetActive(false);

        isMoving = false;
        yield return new WaitForSeconds(waitTime);
        
        shadowAnimator.SetTrigger("StartLoop");
        isMoving = true;
        startMovingTime = Time.time;
        
        transform.position = actualPositionTarget.position;

        if(affordanceInstance != null) Destroy((affordanceInstance.gameObject));
        
        windingUp = false;
        
        timeForPosChange = 0;
    }
    public void DestroyShadow()
    {
        if(affordanceInstance!= null) Destroy(affordanceInstance.gameObject);
        GameManager.Instance.effectsToDestroy.Add(Instantiate(deathShadowParticle, transform.position, transform.rotation)); ;
    }
}
