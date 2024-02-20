using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoveScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    Rigidbody2D rb;
    public static PlatformMoveScript instence;
    private void Awake()
    {
        if (instence == null)
            instence = this;
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
        if(transform.position.y > 6)
        {
            Destroy(gameObject);
        }
    }
    public void DestroyPlatforms()
    {
        Destroy(gameObject);
    }
}
