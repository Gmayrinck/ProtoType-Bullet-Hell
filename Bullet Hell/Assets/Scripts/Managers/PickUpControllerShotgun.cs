using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpControllerShotgun : MonoBehaviour
{
    public Shooting gunScript;
    public Shooting gunScript2;
    public Shooting gunScript3;
    public Rigidbody rb;
    public BoxCollider boxCollider;
    public Transform player, gunContainer, cam;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    void Start()
    {
        if (!equipped)
        {
            gunScript.enabled = false;
            gunScript2.enabled = false;
            gunScript3.enabled = false;
            rb.isKinematic = false;
            boxCollider.isTrigger = false;
        }
        if (equipped)
        {
            gunScript.enabled = true;
            gunScript2.enabled = true;
            gunScript3.enabled = true;
            rb.isKinematic = true;
            boxCollider.isTrigger = true;
            slotFull = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;

        if (!equipped
            && distanceToPlayer.magnitude <= pickUpRange
            && Input.GetKeyDown(KeyCode.E)
            && !slotFull)
        {
            PickUpWeapon();
        }

        if (equipped && Input.GetKeyDown(KeyCode.Q))
        {
            DropWeapon();
        }
    }

    void PickUpWeapon()
    {
        equipped = true;
        slotFull = true;

        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        rb.isKinematic = true;
        boxCollider.isTrigger = true;

        gunScript.enabled = true;
        gunScript2.enabled = true;
        gunScript3.enabled = true;
    }

    void DropWeapon()
    {
        equipped = false;
        slotFull = false;

        transform.SetParent(null);

        rb.isKinematic = false;
        boxCollider.isTrigger = false;

        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        rb.AddForce(cam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(cam.up * dropUpwardForce, ForceMode.Impulse);

        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        gunScript.enabled = false;
        gunScript2.enabled = false;
        gunScript3.enabled = false;
    }
}
