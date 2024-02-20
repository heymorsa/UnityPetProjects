using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySkeleton : Entity
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private LayerMask whatIsPlayer;
    private RaycastHit2D isPlayerDetected;
    bool isAttacking;
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        if (isPlayerDetected)
        {
            if (isPlayerDetected.distance > 1)
            {
                rb.velocity = new Vector2(moveSpeed * 1.5f * facingDirection, rb.velocity.y);
                Debug.Log("i see the player");
                isAttacking = false;
            }
            else
            {
                Debug.Log("attacking" + isPlayerDetected);
                isAttacking = true;
            }
        }
        if (!isGrounded || isWallDetected)
        {
            Flip();
        }
        Movement();
    }

    private void Movement()
    {
        if (!isAttacking)
        {
            rb.velocity = new Vector2(moveSpeed * facingDirection, rb.velocity.y);
        }
        }


    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right, playerCheckDistance * facingDirection, whatIsPlayer);
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + playerCheckDistance * facingDirection, transform.position.y));
    }
}
