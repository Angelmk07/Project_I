using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints;

    private int currentWaypointIndex;

    public void MoveTowardsWaypoint(Rigidbody2D rigidbody2D, float speed)
    {
        if (waypoints.Count == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector2 direction = new Vector2(targetWaypoint.position.x - transform.position.x, 0).normalized;
        rigidbody2D.MovePosition(rigidbody2D.position + direction * speed * Time.fixedDeltaTime);

        if (Mathf.Abs(transform.position.x - targetWaypoint.position.x) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        }
    }
}