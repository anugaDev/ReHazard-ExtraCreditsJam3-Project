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

   

    [SerializeField] private float
        defaultSpeed;
    
    [HideInInspector] public float 
        actualSpeed
        ;
      
        
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        actualSpeed = defaultSpeed;
        GameManager.instance.playerMov = this;
        
    }

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




        playerRb.velocity = movement;
    }
}
