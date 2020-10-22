using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int startingEneymHealth = 100;
    [HideInInspector]
    public int currentEnemyHealth;
    public int scoreValue = 20;
    bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        currentEnemyHealth = startingEneymHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }

        currentEnemyHealth -= damage;

        if(currentEnemyHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        ScoreManager.score += scoreValue;

        GetComponent<NavMeshAgent>().enabled = false;

        Destroy(gameObject, 1f);
    }
}
