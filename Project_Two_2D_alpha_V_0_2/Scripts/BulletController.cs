using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private string tagTarget;

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == tagTarget)
        {
            collision.GetComponent<IStatistics>().Damage(damage);
            Destroy(gameObject);
        }
    }
}