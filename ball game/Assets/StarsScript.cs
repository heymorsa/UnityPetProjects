using UnityEngine;

public class StarsScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    Rigidbody2D rb;
    public static StarsScript instance;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, moveSpeed);
        if (transform.position.y > 6)
            Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            GameManager.Instance.IncScore();
            Destroy(gameObject);
        }
    }

}
