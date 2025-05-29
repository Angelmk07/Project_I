using TMPro;
using UnityEngine;

public class SkillTreeButtonController : MonoBehaviour
{
    [SerializeField] private PlayerStatistics playerStatistics;
    [SerializeField] private PlayerMelee playerMelee;
    [SerializeField] private PlayerShoot playerShoot;
    [SerializeField] private int prise;
    [SerializeField] private string description;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private bool isHealth;
    [SerializeField] private bool isDamageMelee;
    [SerializeField] private bool isDamageShoot;

    public void OnButtonClicked()
    {
        if (playerStatistics.money >= prise)
        {
            if (isHealth)
                playerStatistics.health += 20;
            if (isDamageMelee)
                playerMelee.damage++;
            if (isDamageShoot)
                playerShoot.damage++;
            playerStatistics.money -= prise;
        }
    }
}