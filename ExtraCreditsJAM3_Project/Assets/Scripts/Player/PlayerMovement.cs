using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private KeyCode
        upKey = KeyCode.W,
        downKey = KeyCode.S,
        leftKey = KeyCode.A,
        rightKey = KeyCode.D
        
        ;

    [SerializeField] private Rigidbody2D playerRb;

  
    
    [SerializeField] private Transform 
        legs,
        playerDeath,
        playerSuccess
        ;

    [SerializeField] private float
        defaultSpeed,
        legsRotSpeed
        ;
    
    [HideInInspector] public float 
        actualSpeed
        ;

    private Vector2 direction;
        
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        actualSpeed = defaultSpeed;
        GameManager.instance.playerMov = this;
       
    }

    void FixedUpdate()
    {
      
        Updatedirection();

        UpdateLegs();
    }
    void Updatedirection()
    {
        playerRb.angularVelocity = 0;
        playerRb.velocity = Vector3.zero;
    

        var movement = Vector2.zero;

        if (Input.GetKey(upKey))
        {
            movement.y += 1;
        }
        if (Input.GetKey(downKey))
        {
            movement.y -= 1;

        }
        if (Input.GetKey(rightKey))
        {
            movement.x += 1;
        }
        if (Input.GetKey(leftKey))
        {
            movement.x -= 1;
        }



        direction = movement;
      

        movement *= actualSpeed * Time.fixedDeltaTime;




        playerRb.velocity = movement;
    }

    public void ResetToSpawn(Vector3 newPosition)
    {
        playerRb.velocity = Vector3.zero;
        transform.position = newPosition;
    }

    public void PlayerDeath()
    {
        gameObject.SetActive(false);
        GameManager.instance.effectsToDestroy.Add(Instantiate(playerDeath.gameObject, transform.position, transform.rotation).transform); 
    }

    public void PlayerPassLevel()
    {
        
    }

    void UpdateLegs()
    {
        
        if (direction != Vector2.zero)
        {
            legs.gameObject.SetActive(true);
        }
        else
        {
            legs.gameObject.SetActive(false);
        }
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var q = Quaternion.AngleAxis(angle, Vector3.forward);
        legs.transform.rotation = Quaternion.Slerp(legs.transform.rotation, q, Time.fixedDeltaTime * legsRotSpeed );
    }

    public void SetLegs(bool _state)
    {
        if (_state) //on
        {
            
        }
        else //off
        {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("coliding");
        Vector2 position = transform.position;
        
        var point= other.gameObject.GetComponent<Collider2D>().bounds.ClosestPoint(transform.position);

        
        Vector3 dir = point - transform.position ;
        var magnitude = dir.magnitude;
        // We then get the opposite (-Vector3) and normalize it
        dir = -dir.normalized;
//         And finally we add force in the direction of dir and multiply it by force. 
//         This will push back the player
        playerRb.AddForce(dir*magnitude);
    }

//    private void OnCollisionEnter2D(Collision2D other)
//    {  print("coliding");
//        Vector2 position = transform.position;
//        
//    }
}
