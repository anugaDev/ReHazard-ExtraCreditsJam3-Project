using UnityEngine;

public class DestroySelfAnimation : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
