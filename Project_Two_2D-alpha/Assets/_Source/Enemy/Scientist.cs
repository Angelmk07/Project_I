using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scientist : MonoBehaviour
{
    public Rigidbody2D rb; 
    public float throwForce = 10f;
    [SerializeField] private EnemyStatistics enemyStatistics;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float speed;

    private bool isTouchingWall;
    private bool movingRight = true;
    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (wallCheck != null)
        {
            isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, 0.1f, wallLayer);

            if (isTouchingWall)
            {
                movingRight = !movingRight;
            }
        }

        if (enemyStatistics != null && enemyStatistics._AiState == EnemyStatistics.AiState.Move)
        {
            Move();
        }
        else if (enemyStatistics != null && enemyStatistics._AiState == EnemyStatistics.AiState.Attack)
        {
            MoveToPlayer();
        }
    }

    private void Move()
    {
        if (movingRight)
        {
            transform.localScale = new Vector2(1, 1);
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    private void MoveToPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            if (direction.x > 0)
            {
                transform.localScale = new Vector2(1, 1);
            }
            else
            {
                transform.localScale = new Vector2(-1, 1);
            }

            transform.Translate(direction * speed * Time.deltaTime);
        }
    }



    public void ThrowObject(Vector2 direction)
    {
        rb.velocity = Vector2.zero; 
        rb.AddForce(direction * throwForce, ForceMode2D.Impulse);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawWireSphere(wallCheck.position, 0.1f);
    }
}
