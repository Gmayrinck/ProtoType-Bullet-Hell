using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Weapon settings;

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        if(collision.transform.tag == "Box")
        {
            collision.gameObject.GetComponent<Box>().TakeDamage(settings.weaponDMG);
        }
        if (collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(settings.weaponDMG);
        }
        if (collision.transform.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(settings.weaponDMG);
        }
    }
}
