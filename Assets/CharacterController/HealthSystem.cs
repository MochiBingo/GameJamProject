using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private int health = 4;
    public int Health
    {
        get {  return health; }
        set { if (value <= 0)
                value = 0;
            if (value >= health)
                value = health;
            health = value;
        }
    }
    public bool isDead;
    public GameObject deathscreen;
    public TextMeshProUGUI healthText;

    public void Start()
    {
        Actions.interact += takeDamage;
        isDead = false;
    }
    public void Update()
    {
        if (health <= 0)
        {
            isDead = true;
            deathscreen.SetActive(true);
        }
        
        healthText.text = $"Health: {Health}";
    }
    public void takeDamage(/*int damage*/)
    {
        Health = Health - 1 /*damage*/;
    }
}
