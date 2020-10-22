using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBox : MonoBehaviour
{
    public GameObject boxPrefab;
    public Vector3 center;
    public Vector3 size;
    public float spawnRate;
    float timer;
   
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= spawnRate)
        {
            SpawnBoxes();
        }
    }

    public void SpawnBoxes()
    {
        timer = 0f;

        Vector3 spawnPos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 3f, Random.Range(-size.z / 2, size.z / 2));

        Instantiate(boxPrefab, spawnPos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1,0,1,0.3f);
        Gizmos.DrawCube(center, size);
    }
}
