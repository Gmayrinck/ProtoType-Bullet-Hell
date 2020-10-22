using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingWall : MonoBehaviour
{
    public Vector3 slidingDirection = Vector3.back;
    public float slidingSpeed = 2f;
    public float slidingDelay = 2f;
    int direction = 1;

    void Awake()
    {
        InvokeRepeating("ChangeDirection", slidingDelay, slidingDelay);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(slidingDirection 
            * direction 
            * slidingSpeed 
            * Time.deltaTime, 
            Space.World);
    }
    void ChangeDirection()
    {
        direction *= -1;
    }
}
