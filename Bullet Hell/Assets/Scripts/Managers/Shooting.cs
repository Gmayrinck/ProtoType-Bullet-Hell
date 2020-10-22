using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public Weapon settings;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Text ammoDisplayText;
    public PickUpController pickUpController;

    [HideInInspector]
    public int currentAmmo;

    float timer;
    AudioSource gunAudio;
    
    void Start()
    {
        ammoDisplayText = GameObject.FindGameObjectWithTag("AmmoText").GetComponent<Text>();
        pickUpController = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PickUpController>();
        currentAmmo = settings.weaponClipSize;
        gunAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (pickUpController.equipped)
        {
            ammoDisplayText.text = "Ammo: " + currentAmmo.ToString();
        }

        if (!pickUpController.equipped)
        {
            ammoDisplayText.text = "No Weapon";
        }

        if (Input.GetButton("Fire1") && timer >= settings.weaponFireRate && currentAmmo > 0)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        timer = 0f;

        gunAudio.Play();

        currentAmmo--;

        ammoDisplayText.text = "Ammo: " + currentAmmo.ToString();

        GameObject bullet =  Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        rb.AddForce(-firePoint.forward * settings.bulletForce, ForceMode.Impulse);
    }
}
