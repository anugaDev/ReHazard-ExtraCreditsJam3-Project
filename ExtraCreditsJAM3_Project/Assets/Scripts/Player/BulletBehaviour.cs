using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Vector2 direction;
    [SerializeField] private Rigidbody2D bulletRb;
    [SerializeField] private Transform bulletDestroyParticle;

    private bool isHit = false;
    private void Update()
    {
        UpdateMovement();
    }
    private void UpdateMovement()
    {
        bulletRb.velocity = direction * bulletSpeed * Time.deltaTime;
    }
    public void SetBullet(float bulletSpeed, Vector2 direction, Rigidbody2D bulletRb)
    {
        this.bulletSpeed = bulletSpeed;
        this.direction = direction;
        this.bulletRb = bulletRb;
        GameManager.Instance.gameBullets.Add(this);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var tag = other.gameObject.tag;
        var maytag = gameObject.tag;

        if (isHit) return;
        if (maytag == "PlayerBullet")
        {
            if (tag == "Player" || tag == "ShadowBullet") return;
            isHit = true;
            switch (tag)
            {
                case "PlayerShadow":

                    GameManager.Instance.RemoveShadowAt(other.GetComponent<ShadowBehaviour>());
                    DestroyBullet();
                    break;
                    
                case "Enemy":


                    var enemy = other.GetComponent<Enemy>();
                    if (!enemy.isAlive) return;
                    GameManager.Instance.KillEnemy(enemy);
                    DestroyBullet();
                    break;
                
                default:
                    DestroyBullet();
                    break;
            }
        }
        else
        {
            if (tag == "PlayerShadow" || tag == "Enemy" || tag == "PlayerBullet" || tag == "ShadowBullet") return;
            print("collide");
            isHit = true;

            switch (tag )
            {
                case "Player":

                    GameManager.Instance.FinishRound(false);
                    DestroyBullet();
                    break;
                    
                default:
                        
                    DestroyBullet();
                    break;
            }
        }
    }
    private void DestroyBullet()
    {
       GameManager.Instance.effectsToDestroy.Add(Instantiate(bulletDestroyParticle, transform.position, Quaternion.identity));
        var index = GameManager.Instance.gameBullets.IndexOf(this);

        if (index >= 0)
        {
            GameManager.Instance.gameBullets.RemoveAt(index);
        }
        Destroy(this.gameObject);
       
    }
}
