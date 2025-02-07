using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyStatistics),typeof(Rigidbody2D))]
public class Scientist : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float throwForce = 10f;
    [SerializeField] private float throwUp = 4;
    [SerializeField, ReadOnly] private bool canThrow = true;
    [SerializeField] private float throwCooldown = 2f;
    [SerializeField] private EnemyStatistics enemyStatistics;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject PotionPrefub;
    [SerializeField] private GameObject ThrowPlace;
    [SerializeField] private float speed;
    [SerializeField] private float detectionRadius = 5f; 
    [SerializeField] private float attackDistance = 2f; 
    [SerializeField] private bool canMove = true; 

    [SerializeField,ReadOnly] private bool isTouchingWall;
    [SerializeField,ReadOnly] private GameObject Potion;
    [SerializeField,ReadOnly] private Rigidbody2D PotionRb;
    [SerializeField, ReadOnly]  private bool movingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Potion = Instantiate(PotionPrefub);
        PotionRb = Potion.AddComponent<Rigidbody2D>();
        Potion.SetActive(false);
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

        CheckForPlayerInRange();

        if (enemyStatistics._AiState == EnemyStatistics.AiState.Move&&player==null)
        {
            Move();
        }
        else if (enemyStatistics._AiState == EnemyStatistics.AiState.Move)
        {
            MoveToPlayer();
        }
        else if (enemyStatistics._AiState == EnemyStatistics.AiState.Attack)
        {
            ThrowObject(player.position);
        }
    }

    private void CheckForPlayerInRange()
    {
        Collider2D playerInRange = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);

        if (playerInRange != null)
        {
            player = playerInRange.transform;
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer > attackDistance)
            {
                enemyStatistics._AiState = EnemyStatistics.AiState.Move;
            }
            else
            {
                enemyStatistics._AiState = EnemyStatistics.AiState.Attack;
            }
        }
        else
        {
            enemyStatistics._AiState = EnemyStatistics.AiState.Move;
        }
    }

    private void Move()
    {
        if (canMove)
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

    }

    private void MoveToPlayer()
    {
        if (player != null&&canMove)
        {
            Vector2 direction = new Vector2( (player.position.x - transform.position.x),0).normalized;

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
        if (!canThrow) return;

        canThrow = false;
        Invoke(nameof(ResetThrowCooldown), throwCooldown);

        Potion.transform.position = ThrowPlace.transform.position;
        Potion.SetActive(true);

 
        Vector2 throwVelocity = direction.normalized * throwForce + Vector2.up * throwUp; 

        PotionRb.AddForce(throwVelocity,ForceMode2D.Impulse);
    }

    private void ResetThrowCooldown()
    {
        canThrow = true; 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(wallCheck.position, 0.1f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}