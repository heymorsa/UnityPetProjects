using UnityEngine;

public class Player : MonoBehaviour
{
    public bool canMove = true;
    Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    private float xInput;
    [SerializeField] private float maxPos;
    private float xPos;
    public static Player instance;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (canMove)
        {
            Movement();
        }
    }

    private void Movement()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        xPos = Mathf.Clamp(transform.position.x, -maxPos, maxPos);
        transform.position = new Vector2(xPos, transform.position.y);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Saw")
        {
            Destroy(gameObject);
            
            GameManager.Instance.GameOver();
            
        }
    }
}
