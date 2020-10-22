using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpController : MonoBehaviour
{
    public Shooting gunScript;
    public Rigidbody rb;
    public BoxCollider boxCollider;
    public Transform player, gunContainer, cam;
    public Text ammoDisplayText;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    void Start()
    {
        ammoDisplayText = GameObject.FindGameObjectWithTag("AmmoText").GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gunContainer = GameObject.FindGameObjectWithTag("GunContainer").transform;
        cam = Camera.main.transform;

        if (!equipped)
        {
            gunScript.enabled = false;
            rb.isKinematic = false;
            boxCollider.isTrigger = false;
        }
        if (equipped)
        {
            gunScript.enabled = true;
            rb.isKinematic = true;
            boxCollider.isTrigger = true;
            slotFull = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;

        if(!equipped 
            && distanceToPlayer.magnitude <= pickUpRange
            && Input.GetKeyDown(KeyCode.E) 
            && !slotFull)
        {
            PickUpWeapon();
        }

        if(equipped && Input.GetKeyDown(KeyCode.Q))
        {
            DropWeapon();
        }
    }

    public void PickUpWeapon()
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
    }

    public void DropWeapon()
    {
        ammoDisplayText.text = "No Weapon";
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

        Destroy(gameObject, 2f);
    }
}
