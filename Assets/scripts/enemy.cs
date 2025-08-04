using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyPatrolRigidbodyPlatform : MonoBehaviour
{
    public Transform platform;            
    public Rigidbody platformRb;         
    public float patrolSpeed = 2f;        
    public float edgeOffset = 0.5f;      
    public GameObject deathFXPrefab;
         

    private int direction = 1;

    void FixedUpdate()
    {
        
        Vector3 platformVelocity = platformRb.linearVelocity;
        transform.position += platformVelocity * Time.fixedDeltaTime;

        
        transform.Translate(Vector3.right * direction * patrolSpeed * Time.fixedDeltaTime, Space.World);

     
        float halfWidth = platform.localScale.x / 2f;
        float leftEdge = platform.position.x - halfWidth + edgeOffset;
        float rightEdge = platform.position.x + halfWidth - edgeOffset;

      
        float clampedX = Mathf.Clamp(transform.position.x, leftEdge, rightEdge);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

      
        if (transform.position.x <= leftEdge || transform.position.x >= rightEdge)
        {
            direction *= -1;
        }
    }

   private void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Player"))
    {
        ContactPoint contact = collision.contacts[0];

        if (contact.normal.y < -0.5f)
        {
         
            if (deathFXPrefab != null)
                Instantiate(deathFXPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
        else
        {
            
            if (deathFXPrefab != null)
                Instantiate(deathFXPrefab, collision.transform.position, Quaternion.identity);

         
            SceneManager.LoadScene("game over");
        }
    }
}

}
