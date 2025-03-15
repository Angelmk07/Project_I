using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatistics : MonoBehaviour, IStatistics
{
    public int money;

    [SerializeField] private int health;
    [SerializeField] private Slider healthSlider;

    void Start()
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    void Update()
    {
        healthSlider.value = health;
    }

    public void Damage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}