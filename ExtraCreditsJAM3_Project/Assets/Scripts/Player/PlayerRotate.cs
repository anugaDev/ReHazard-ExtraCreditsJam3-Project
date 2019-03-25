using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    [SerializeField] private Camera gameCamera;
    public Vector2 lookingVector;

    private void Start()
    {
        gameCamera = gameCamera == null ? Camera.main : gameCamera;
    }
    private void Update()
    {
        UpdateMouse();
    }
    private void UpdateMouse()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = -gameCamera.transform.position.z;
        var l_objectPos = gameCamera.WorldToScreenPoint(transform.position);

        mousePos.x -= l_objectPos.x;
        mousePos.y -= l_objectPos.y;

        var angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if ((Mathf.Sign(transform.localScale.x) < 0))
        {
            angle += 180;
        }
        lookingVector =  mousePos;
        lookingVector.Normalize();
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, angle );
    }
}
