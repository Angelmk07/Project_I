using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemyStatistics enemyStatistics;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float speed;

    private bool isTouchingWall;
    private bool movingRight = true;

    private void Update()
    {
        HandleAIState();
    }

    private void CheckWallCollision()
    {
        if (wallCheck != null)
        {
            isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, 0.1f, wallLayer);

            if (isTouchingWall)
            {
                movingRight = !movingRight;
            }
        }
    }

    private void HandleAIState()
    {
        CheckWallCollision();

        if (enemyStatistics != null)
        {
            switch (enemyStatistics._AiState)
            {
                case EnemyStatistics.AiState.Move:
                    Move();
                    break;
                case EnemyStatistics.AiState.Attack:
                    MoveToPlayer();
                    break;
            }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawWireSphere(wallCheck.position, 0.1f);
    }
}