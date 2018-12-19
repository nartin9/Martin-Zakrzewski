using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUps : MonoBehaviour
{
    private CharacterPlayerStats stats;
    float health, armor;
    int maxHealth, maxArmor;
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<CharacterPlayerStats>();
        health = stats.health;
        maxHealth = stats.maxHealth;
        armor = stats.armor;
        maxArmor = stats.maxArmor;
    }
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "BigHealthPowerUp")
        {
            if (health < maxHealth)
            {
                if (health < health + 30)
                    health = maxHealth;
                else
                    health += 30;
                Destroy(col.gameObject);
            }
        }
        else if(col.gameObject.name == "smallHealthPowerUp")
        {
            if (health < maxHealth)
            {
                if (health < health + 10)
                    health = maxHealth;
                else
                    health += 10;
                 Destroy(col.gameObject);
            }
        }
        else if (col.gameObject.name == "armorPowerUp")
        {
            if (armor < maxArmor)
            {
                if (armor < armor + 10)
                    armor = maxArmor;
                else
                    armor += 10;
               Destroy(col.gameObject);
            }
        }
        else if (col.gameObject.name == "armorBuff")
        {
                armor += 1;
                Destroy(col.gameObject);
        }
        else if (col.gameObject.name == "Suit")
        {
            if(stats.protectPrec < 100)
            {
               stats.protectPrec = 1000;
               Destroy(col.gameObject);
            }
            stats.health = health;
            stats.armor = armor;
        }
    }
}
