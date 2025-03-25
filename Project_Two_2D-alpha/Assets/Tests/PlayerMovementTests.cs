using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerMovementTests
{
    private GameObject playerObject;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;

    [SetUp]
    public void Setup()
    {
        playerObject = new GameObject();
        playerMovement = playerObject.AddComponent<PlayerMovement>();
        rb = playerObject.AddComponent<Rigidbody2D>();

        playerMovement.DefaultSpeed = 5f;
        playerMovement.maxSpeed = 10f;
        playerMovement.jumpForce = 10f;
        playerMovement.airDashForce = 15f;
        playerMovement.wallDashForce = 15f;

        playerMovement.groundCheck = new GameObject().transform;
        playerMovement.wallCheck = new GameObject().transform;
        playerMovement.wallSlidePoint = new GameObject().transform;

        playerMovement.groundLayer = LayerMask.GetMask("Ground");
        playerMovement.wallLayer = LayerMask.GetMask("Wall");
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(playerObject);
    }

    [UnityTest]
    public IEnumerator PlayerCanJumpWhenGrounded()
    {
        playerMovement.groundCheck.position = playerObject.transform.position;
        playerMovement.isGrounded = true;

        playerMovement.Jump(true);
        yield return null;

        Assert.Greater(rb.velocity.y, 0);
    }

    [UnityTest]
    public IEnumerator PlayerCannotJumpWhenInAir()
    {
        playerMovement.isGrounded = false;

        playerMovement.Jump(true);
        yield return null;

        Assert.AreEqual(rb.velocity.y, 0);
    }

    [UnityTest]
    public IEnumerator PlayerCanDash()
    {
        playerMovement.isGrounded = true;
        playerMovement.canDash = true;

        playerMovement.Dash(true);
        yield return null;

        Assert.Greater(rb.velocity.x, 0);
    }

    [UnityTest]
    public IEnumerator PlayerWallSlide()
    {
        playerMovement.isTouchingWall = true;
        playerMovement.isSlide = true;

        playerMovement.WallSlide(0);
        yield return null;

        Assert.Less(rb.velocity.y, 0);
    }
}