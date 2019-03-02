using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed;

    [SerializeField] private Vector2 direction;
    [SerializeField] private Rigidbody2D bulletRb;
    
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

    private void OnTriggerEnter(Collider other)
    {
        var tag = other.gameObject.tag;
        var mytag = gameObject.tag;
        if (mytag == "PlayerBullet")
        {
            if (other.tag != "Player" || other.tag != "PlayerShadow")
            {
                
                
                
                GameManager.instance.gameBullets.RemoveAt(GameManager.instance.gameBullets.IndexOf(this));
                Destroy(this.gameObject);
            }
            
        }
        else
        {
            if (other.tag != "PlayerShadow" || other.tag != "Enemy")
            {
                GameManager.instance.gameBullets.RemoveAt(GameManager.instance.gameBullets.IndexOf(this));
                Destroy(this.gameObject);
            }
        }
    }
}
