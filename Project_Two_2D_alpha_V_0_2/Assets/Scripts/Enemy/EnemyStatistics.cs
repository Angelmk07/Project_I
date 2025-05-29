using UnityEngine;

public class EnemyStatistics : MonoBehaviour, IStatistics
{
    [SerializeField] private int health;
    [SerializeField] private int giveMoney;
    [SerializeField] private float speed;
    [SerializeField] private float detectionRange;
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private MonoBehaviour enemyAttack;

    private IAttack enemyIAttack;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyIAttack = enemyAttack as IAttack;
    }

    private void Update()
    {
        if (player != null)
        {
            if (enemyIAttack != null && Vector3.Distance(transform.position, player.transform.position) < detectionRange)
            {
                enemyIAttack.Attack(player.transform, rigidbody2D, speed);
            }
            else if (enemyMovement != null)
            {
                enemyMovement.MoveTowardsWaypoint(rigidbody2D, speed);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            player.GetComponent<PlayerStatistics>().money += giveMoney;
            Destroy(gameObject);
        }
    }
}