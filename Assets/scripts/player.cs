using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 7f;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundMask;
    public float moveSpeed = 5f;
    public float fallBuffer = 2f;
    public float extraGravity = 20f;
    private Rigidbody rb;
    private float halfWidth;
    private bool isGrounded;
    private Transform currentPlatform;
    public gameoverscript gamemanger;
    public AudioClip jumpSound;
    private AudioSource audioSource;
 public float gravityScale = 4f;  
 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        halfWidth = GetComponent<Renderer>().bounds.extents.x;
        audioSource = GetComponent<AudioSource>();
        Physics.gravity = new Vector3(0, -9.81f * gravityScale, 0);
      
   
    }

    void Update()
    {

        float moveX = Input.GetAxis("Horizontal");
        transform.position += new Vector3(moveX, 0, 0) * moveSpeed * Time.deltaTime;


        RaycastHit hit;
        float height = GetComponent<Collider>().bounds.extents.y;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, height + groundCheckDistance, groundMask);

        if (isGrounded)
        {

            currentPlatform = hit.transform;
            
            if (IsJumpPressed())
            {
                Jump();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {

            currentPlatform = null;
        }
        bool IsJumpPressed()
        {

            if (Input.GetKeyDown(KeyCode.Space))
                return true;


            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                return true;

            return false;
        }

        ClampToScreen();


        float cameraBottomY = Camera.main.transform.position.y - Camera.main.orthographicSize;
        if (transform.position.y < cameraBottomY)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("game over");
        }
    }

    void OnDestroy()
    {
        if (gameoverscript.instance != null)
        {
            SceneManager.LoadScene("game over");
        }
    }

    public void Jump()
    {

        rb.linearVelocity = new Vector3(0, 0, 0);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        if (jumpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }
    void FixedUpdate()
    {

        if (!isGrounded && rb.linearVelocity.y < 0)
        {
            rb.AddForce(Vector3.down * extraGravity);
        }
    }
    void ClampToScreen()
    {
        float zDepth = Camera.main.WorldToViewportPoint(transform.position).z;
        float screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, zDepth)).x + halfWidth;
        float screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, zDepth)).x - halfWidth;

        Vector3 clampedPos = transform.position;
        clampedPos.x = Mathf.Clamp(transform.position.x, screenLeft, screenRight);
        transform.position = clampedPos;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        float height = GetComponent<Collider>() ? GetComponent<Collider>().bounds.extents.y : 0.5f;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * (height + groundCheckDistance));
    }

    void OnTriggerEnter(Collider other)
    {
     
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Player hit by enemy!");
            Destroy(gameObject);
        }
    }

    void Die()
    {

       SceneManager.LoadScene("game over");
    }

}
