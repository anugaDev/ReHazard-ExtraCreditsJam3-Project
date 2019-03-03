using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform bulletPrefab;

    [SerializeField] private Camera gameCamera;
    [SerializeField] private int shootButton;
    [SerializeField] private PlayerRecorder recorder;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private bool isReloaded;
    [SerializeField] private float
        bulletSpeed,
        timeBetweenShoots,
        shootingOffset
        ;

    private void OnEnable()
    {
        isReloaded = true;
    }

    void Start()
    {
        gameCamera = gameCamera == null ? Camera.main : gameCamera;
        
        isReloaded = true;
        recorder = GetComponent<PlayerRecorder>();
        movement = GetComponent<PlayerMovement>();

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(shootButton) && isReloaded)
        {
            Shoot();
        }
    }

    public void Shoot()
    {

        var bullet = Instantiate(bulletPrefab, transform.position + (GetForwardDir() * shootingOffset), transform.rotation);
        var bulletBehaviour = bullet.GetComponent<BulletBehaviour>();
        bulletBehaviour.SetBullet(bulletSpeed, GetForwardDir(),bullet.GetComponent<Rigidbody2D>());
        recorder.AddShootingRecord();
        StartCoroutine(Reload());
    }

    Vector3 GetForwardDir()
    {
        var direction = transform.right;

        
        return direction;
    }

    IEnumerator Reload()
    {
        isReloaded = false;
        yield return new WaitForSeconds(timeBetweenShoots);
        isReloaded = true;
    }
}
