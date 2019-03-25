using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public float actualSpeed;

    [SerializeField] private KeyCode upKey = KeyCode.W;
    [SerializeField] private KeyCode downKey = KeyCode.S  ;
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private Transform legs;
    [SerializeField] private Transform playerDeath;
    [SerializeField] private float legsRotSpeed;
    [SerializeField] private float defaultSpeed;
    
    private Vector2 direction;
    private GameObject myDeath;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        actualSpeed = defaultSpeed;
        GameManager.Instance.playerMov = this;
    }

    private void FixedUpdate()
    {
        UpdateDirection();
        UpdateLegs();
    }
    private void UpdateDirection()
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
    public void Respawn()
    {
        gameObject.SetActive(true);
        if(myDeath != null) Destroy(myDeath);
    }
    public void PlayerDeath()
    {
        gameObject.SetActive(false);
        myDeath = Instantiate(playerDeath.gameObject, transform.position, transform.rotation);
        GameManager.Instance.effectsToDestroy.Add(myDeath.transform); 
    }
    private void UpdateLegs()
    {
        legs.gameObject.SetActive(direction != Vector2.zero);
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var q = Quaternion.AngleAxis(angle, Vector3.forward);
        legs.transform.rotation = Quaternion.Slerp(legs.transform.rotation, q, Time.fixedDeltaTime * legsRotSpeed );
    }
 
}
