using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    public int punchDamage = 5;
    public Transform punchSensor;
    public float punchRange = 0.7f;
    public LayerMask boxLayer;

    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Punch();
        }
    }

    void Punch()
    {        
        anim.SetTrigger("Punch");

        Collider[] hitBoxs = Physics.OverlapSphere(punchSensor.position, punchRange, boxLayer);

        foreach (Collider box in hitBoxs)
        {
            box.GetComponent<Box>().TakeDamage(punchDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (punchSensor == null)
            return;

        Gizmos.DrawWireSphere(punchSensor.position, punchRange);
    }
}
