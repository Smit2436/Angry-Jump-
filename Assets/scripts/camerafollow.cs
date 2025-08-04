using UnityEngine;

public class CameraFollowUpOnly : MonoBehaviour
{
    public Transform player;         
    public float yOffset = 2f;       
    public float smoothSpeed = 5f;   

    private float currentY;          

    void Start()
    {
        currentY = transform.position.y;
    }

    void LateUpdate()
    {
        if (player == null) return;
        float targetY = player.position.y - yOffset;
        
        if (targetY > currentY)
        {
            currentY = Mathf.Lerp(currentY, targetY, smoothSpeed * Time.deltaTime);
        }
        transform.position = new Vector3(transform.position.x, currentY, transform.position.z);
    }
}
