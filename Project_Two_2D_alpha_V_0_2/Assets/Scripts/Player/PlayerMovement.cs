using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int maxJumpCount;
    [SerializeField] private float speedWalk;
    [SerializeField] private float jumpForce;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private Transform playerSprite;
    [SerializeField] private Transform floorCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Rigidbody2D rigidbody2D;

    [Header("Info")]
    [SerializeField] private int jumpCount;
    [SerializeField] private float moveDirection;
    [SerializeField] private float dashTime;
    [SerializeField] private bool touchFloor;
    [SerializeField] private bool touchWall;
    [SerializeField] private bool isDashing;

    private void Start()
    {

    }

    private void Update()
    {
        touchFloor = Physics2D.OverlapBox(floorCheck.position, new Vector2(0.8f, 0.25f), 0, wallLayer);
        touchWall = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.25f, 0.8f), 0, wallLayer);

        if (isDashing)
        {
            dashTime -= Time.deltaTime;
            if (dashTime <= 0)
            {
                isDashing = false;
                rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
            }
        }

        if (touchFloor)
        {
            jumpCount = 0;
        }
        else if (touchWall)
        {
            jumpCount = 1;
        }
    }

    public void Walk()
    {
        moveDirection = Input.GetAxis("Horizontal");
        if (!isDashing)
        {
            rigidbody2D.velocity = new Vector2(moveDirection * speedWalk, rigidbody2D.velocity.y);
            if (moveDirection > 0)
            {
                playerSprite.localScale = new Vector2(1, 1);
            }
            else if (moveDirection < 0)
            {
                playerSprite.localScale = new Vector2(-1, 1);
            }
        }
    }

    public void Jump(bool inputAction)
    {
        if (inputAction && jumpCount < maxJumpCount)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
            jumpCount++;
        }
    }

    public void Dash(bool inputAction)
    {
        if (inputAction && !isDashing)
        {
            isDashing = true;
            dashTime = dashDuration;

            float dashDirection = Input.GetAxis("Horizontal");
            if (dashDirection == 0) dashDirection = playerSprite.localScale.x;
            rigidbody2D.velocity = new Vector2(dashDirection * dashSpeed, rigidbody2D.velocity.y);
        }
    }

    public void WallSlide()
    {
        if (!touchFloor && touchWall)
        {
            rigidbody2D.gravityScale = 0.45f;
        }
        else if (touchFloor || !touchWall)
        {
            rigidbody2D.gravityScale = 1;
        }
    }
}