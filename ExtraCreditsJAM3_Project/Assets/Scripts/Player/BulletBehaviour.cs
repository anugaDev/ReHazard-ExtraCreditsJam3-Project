using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed;

    [SerializeField] private Vector2 direction;
    [SerializeField] private Rigidbody2D bulletRb;
    private bool isHit = false;

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
    }

    void UpdateMovement()
    {
        bulletRb.velocity = direction * bulletSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        throw new System.NotImplementedException();
    }

    public void SetBullet(float bulletSpeed, Vector2 direction, Rigidbody2D bulletRb)
    {
        this.bulletSpeed = bulletSpeed;
        this.direction = direction;
        this.bulletRb = bulletRb;

        GameManager.instance.gameBullets.Add(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        var tag = other.gameObject.tag;
        var mytag = gameObject.tag;

        if (isHit) return;
        if (mytag == "PlayerBullet")
        {
            if (tag != "Player" && tag != "ShadowBullet")
            {
                isHit = true;
                switch (tag)
                {
                    case "PlayerShadow":

                        GameManager.instance.RemoveShadowAt(other.GetComponent<ShadowBehaviour>());
                        
                        break;
                    
            
                    
                }
                
                
                DestroyBullet();
            }
            
        }
        else
        {
            if (tag != "PlayerShadow" && tag != "Enemy" && tag != "PlayerBullet" && tag != "ShadowBullet")
            {
                print("collide");
                isHit = true;

                switch (tag )
                {
                    case "Player":

                        GameManager.instance.FinishRound(false);
                        
                        break;
                    
                    case "PlayerBullet":

                        //other.GetComponent<BulletBehaviour>().DestroyBullet();
                        
                        break;
                    
                }

               
                DestroyBullet();
            }
        }





    }
    public void DestroyBullet()
    {
        var index = GameManager.instance.gameBullets.IndexOf(this);

//        print(index);
        if (index >= 0)
        {
            GameManager.instance.gameBullets.RemoveAt(index);
           
        }
        Destroy(this.gameObject);
       
    }
}
