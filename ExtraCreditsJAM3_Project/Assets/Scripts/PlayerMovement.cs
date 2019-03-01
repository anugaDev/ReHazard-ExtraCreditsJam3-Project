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
    
    public Vector2 m_LookingVector, m_abilityVelocity;

    [SerializeField] private float
        defaultSpeed;
    
    [HideInInspector] public float 
        actualSpeed
        ;
      
        
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        actualSpeed = defaultSpeed;
        GameManager.instance.player = this;
        
    }

    // Update is called once per frame
    void Update()
    {
      
        UpdateMovement();
    }
    void UpdateMovement()
    {
    

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

       

      

        movement *= actualSpeed * Time.fixedDeltaTime;

        print(movement);



        playerRb.velocity = movement;
    }
}
