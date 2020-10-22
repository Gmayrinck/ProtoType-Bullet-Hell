using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ItemToSpawn
{
    public GameObject item;
    public float spawnRate;
    [HideInInspector]
    public float minSpawnProb, maxSpawnProb;
}

public class Box : MonoBehaviour
{
    public int boxHealth = 20;
    public int scoreValue = 10;
    public ItemToSpawn[] itemToSpawns;
    public Collider boxCollider;

    int boxCurrentHealth;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        boxCurrentHealth = boxHealth;

        for (int i = 0; i < itemToSpawns.Length; i++)
        {
            if (i == 0)
            {
                itemToSpawns[i].minSpawnProb = 0;
                itemToSpawns[i].maxSpawnProb = itemToSpawns[i].spawnRate - 1;
            }
            else
            {
                itemToSpawns[i].minSpawnProb = itemToSpawns[i - 1].maxSpawnProb + 1;
                itemToSpawns[i].maxSpawnProb = itemToSpawns[i].minSpawnProb + itemToSpawns[i].spawnRate + 1;
            }
        }
    }    

    public void TakeDamage(int damage)
    {
        boxCurrentHealth -= damage;

        if (boxCurrentHealth <= 0)
        {
            BreakBox();
            Spawnner();
        }
    }

    void BreakBox()
    {                
        anim.SetTrigger("Destroyed");

        boxCollider.enabled = false;

        ScoreManager.score += scoreValue;

        Destroy(gameObject, 1f); 
    }
    void Spawnner()
    {
        float randomNum = Random.Range(0, 100);

        for (int i = 0; i < itemToSpawns.Length; i++)
        {
            if (randomNum <= itemToSpawns[i].maxSpawnProb && randomNum >= itemToSpawns[i].minSpawnProb)
            {
                Instantiate(itemToSpawns[i].item, transform.position + new Vector3(0f, 2f, 0f), Quaternion.identity);
                break;
            }
        }
    }
}
