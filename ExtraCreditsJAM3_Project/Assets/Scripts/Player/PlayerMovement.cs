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

  
    
    [SerializeField] private Transform legs;

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

    

}
