using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyShellShotgon : MonoBehaviour
{
    public Weapon settings;
    public GameObject bulletsPrefab;
    public int numberOfBullets;
    public float radius;

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        SpawnBullets(numberOfBullets); 
        if (collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(settings.weaponDMG);
        }
    }

    void SpawnBullets(int numberOfBullets)
    {
        float angleStep = 360f / numberOfBullets;
        float angle = 0f;

        for (int i = 0; i <= numberOfBullets - 1; i++)
        {
            float bulletDirXPos = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float bulletDirZPos = transform.position.z + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 bulletVector = new Vector3(bulletDirXPos, 0f, bulletDirZPos);
            Vector3 bulletsMoveDirection = (bulletVector - transform.position).normalized * settings.bulletForce;

            var bullet = Instantiate(bulletsPrefab, transform.position, Quaternion.identity);

            bullet.GetComponent<Rigidbody>().velocity = new Vector3(bulletDirXPos, 0f, bulletDirZPos);

            angle += angleStep;
        }
    }
}
