using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlackboard : MonoBehaviour
{
    public float playerDetectionRadius;

    public static EnemyBlackboard instance;
    public PlayerMovement player;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        player = GameManager.instance.playerMov;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
