using UnityEngine;

public class MoveUp : MonoBehaviour
{
    public float moveSpeed = 1f; // Speed of movement

    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
}
