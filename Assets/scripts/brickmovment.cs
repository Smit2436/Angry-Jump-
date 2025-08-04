using UnityEngine;

public class LeftRightMover : MonoBehaviour
{
    public float moveSpeed = 5f;
    private int direction = 1;

    private Rigidbody rb;
    private float objectHalfWidthWorld;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        objectHalfWidthWorld = GetComponent<Collider>().bounds.extents.x;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(direction * moveSpeed, rb.linearVelocity.y, rb.linearVelocity.z);

      
        Vector3 leftEdgeScreen = Camera.main.WorldToScreenPoint(transform.position - new Vector3(objectHalfWidthWorld, 0, 0));
        Vector3 rightEdgeScreen = Camera.main.WorldToScreenPoint(transform.position + new Vector3(objectHalfWidthWorld, 0, 0));

     
        if (rightEdgeScreen.x >= Screen.width)
        {
            direction = -1;
        }
        else if (leftEdgeScreen.x <= 0)
        {
            direction = 1;
        }
    }
}
