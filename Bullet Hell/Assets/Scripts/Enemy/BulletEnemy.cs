using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public Weapon settings;

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        if (collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(settings.weaponDMG);
        }
    }
}
