using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public float maxSpeed;
    public float DefaultSpeed;
    public float jumpForce;
    public float airDashForce;
    public float wallDashForce;
    public float wallReleaseSpeedMultiplier = 1.5f;
    public float dashCooldown = 1f;
    public Transform groundCheck;
    public Transform wallCheck;
    public Transform wallSlidePoint;
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    [Header("Info")]
    public bool canDash = true;
    public bool isGrounded;
    public bool isTouchingWall;
    public bool isSlide;
    public bool canMove = true;
    public bool IsGrounded => isGrounded;
    public event Action Landed;
    public Vector3 lastPosition;


    public float speed;
    public float releaseSpeed;
    public int jumpCount = 0;
    public int maxJumpCount = 1;

    private void Start()
    {
        speed = DefaultSpeed;
    }

    private void Update()
    {
        if (groundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
            if (isGrounded)
            {
                lastPosition = transform.position;
                jumpCount = 0;
                Landed?.Invoke();
            }

        }

        if (wallCheck != null)
        {
            isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, 0.1f, wallLayer);
        }

        if (wallSlidePoint != null)
        {
            isSlide = Physics2D.OverlapCircle(wallSlidePoint.position, 0.1f, wallLayer);
        }
    }

    public void Move(float inputAction)
    {
        if (canMove)
        {
            if (rb != null)
            {
                rb.velocity = new Vector2(inputAction * speed, rb.velocity.y);
            }

            if (inputAction > 0 && !isSlide)
            {
                transform.localScale = new Vector2(1, 1);
            }
            else if (inputAction < 0 && !isSlide)
            {
                transform.localScale = new Vector2(-1, 1);
            }
        }
    }

    public void Jump(bool inputAction)
    {
        if (inputAction && (isGrounded || jumpCount < maxJumpCount&& !isSlide))
        {
            Debug.Log("Player jump.");
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
        }
        else if (inputAction && isSlide && !isGrounded)
        {
            Debug.Log("Wall jump.");
            int wallDirection = isTouchingWall ? (int)Mathf.Sign(transform.localScale.x) : 0;
            Vector2 jumpDirection = new Vector2(-wallDirection * wallDashForce, jumpForce);
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(jumpDirection, ForceMode2D.Impulse);
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    public void Dash(bool inputAction)
    {
        if (inputAction)
        {
            Debug.Log("Player Dash.");
            rb.velocity = new Vector2(0, rb.velocity.y);
            rb.AddForce(Vector2.right * (int)Mathf.Sign(transform.localScale.x) * airDashForce, ForceMode2D.Impulse);
            Invoke(nameof(ResetDash), dashCooldown);
        }
    }

    private void ResetDash()
    {
        canDash = true;
    }

    public void WallSlide(float inputAction)
    {
        bool isMovingHorizontally = inputAction == 0;

        if (isMovingHorizontally && isTouchingWall && !isSlide && !isGrounded)
        {
            transform.localScale = new Vector2(Mathf.Sign(inputAction), 1);
        }

        if (isMovingHorizontally && !isGrounded && isSlide)
        {
            Debug.Log("Player slide.");
            rb.velocity = new Vector2(rb.velocity.x, -1);
        }

        releaseSpeed = DefaultSpeed / wallReleaseSpeedMultiplier * transform.localScale.x;

        if (isSlide && Mathf.Abs(rb.velocity.x) > releaseSpeed)
        {
            canMove = true;
        }
        else if (isSlide && !isGrounded)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
    }

    public void Run(bool inputAction)
    {
        if (inputAction && isGrounded)
        {
            Debug.Log("Player run.");
            speed = maxSpeed;
        }
        else if (!inputAction || !isGrounded)
        {
            speed = DefaultSpeed;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheck.position, 0.1f);
        Gizmos.DrawWireSphere(wallCheck.position, 0.1f);
        Gizmos.DrawWireSphere(wallSlidePoint.position, 0.1f);
    }
}