using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestPlayerMovement
{
    private GameObject playerObject;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;

    [SetUp]
    public void Setup()
    {
        playerObject = new GameObject("Player");
        rb = playerObject.AddComponent<Rigidbody2D>();
        playerMovement = playerObject.AddComponent<PlayerMovement>();

        playerMovement.DefaultSpeed = 5f;
        playerMovement.maxSpeed = 10f;
        playerMovement.jumpForce = 10f;

        playerMovement.groundCheck = new GameObject("GroundCheck").transform;
        playerMovement.wallCheck = new GameObject("WallCheck").transform;
        playerMovement.groundLayer = LayerMask.GetMask("Ground");
        playerMovement.wallLayer = LayerMask.GetMask("Wall");

        // Установка позиции groundCheck для тестов
        playerMovement.groundCheck.position = playerObject.transform.position - new Vector3(0, 0.1f, 0);
        playerMovement.wallCheck.position = playerObject.transform.position + new Vector3(0.1f, 0, 0);
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(playerObject);
    }

    [UnityTest]
    public IEnumerator TestPlayerMovement_Move()
    {
        playerMovement.Move(1f);
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(5f, rb.velocity.x, "Player should move to the right.");

        playerMovement.Move(-1f);
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(-5f, rb.velocity.x, "Player should move to the left.");
    }

    [UnityTest]
    public IEnumerator TestPlayerMovement_Jump()
    {
        playerMovement.isGrounded = true;
        playerMovement.Jump(true);
        yield return new WaitForFixedUpdate();
        Assert.Greater(rb.velocity.y, 0f, "Player should jump upwards.");
    }

    [UnityTest]
    public IEnumerator TestPlayerMovement_WallSlide()
    {
        playerMovement.isTouchingWall = true;
        playerMovement.isGrounded = false;
        playerMovement.WallSlide(0f); // Передаем 0, чтобы проверить скольжение
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(-1f, rb.velocity.y, "Player should slide down the wall.");
    }

    [UnityTest]
    public IEnumerator TestPlayerMovement_Run()
    {
        playerMovement.isGrounded = true;
        playerMovement.Run(true);
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(10f, playerMovement.speed, "Player speed should be max speed while running.");

        playerMovement.Run(false);
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(5f, playerMovement.speed, "Player speed should revert to default speed.");
    }
}