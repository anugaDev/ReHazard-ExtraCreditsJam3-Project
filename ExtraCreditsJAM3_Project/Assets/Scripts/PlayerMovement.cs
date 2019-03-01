using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private KeyCode
        m_UpKey = KeyCode.W,
        m_DownKey = KeyCode.S,
        m_LeftKey = KeyCode.A,
        m_RightKey = KeyCode.D
        
        ;
    public Vector2 m_LookingVector, m_abilityVelocity;

    [SerializeField] private float
        m_defaultSpeed;
    
    [HideInInspector] public float 
        m_actualSpeed
        ;
      
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.m_instance.m_Player = this;
        m_actualSpeed = m_defaultSpeed;
    }
    void UpdateMovement()
    {
        //float l_Vertical = Mathf.Round( Input.GetAxis("Horizontal"));
        //float l_Horizontal = Mathf.Round ( Input.GetAxis("Vertical"));

        Vector2 l_Movement = new Vector2();

        if (Input.GetKey(m_UpKey))
        {
            l_Movement.y += 1;
        }
        if (Input.GetKey(m_DownKey))
        {
            l_Movement.y -= 1;

        }
        if (Input.GetKey(m_RightKey))
        {
            l_Movement.x += 1;
        }
        if (Input.GetKey(m_LeftKey))
        {
            l_Movement.x -= 1;
        }

       

      

        l_Movement *= m_actualSpeed * Time.deltaTime;



        m_PlayerRb.velocity = l_Movement;
    }
}
