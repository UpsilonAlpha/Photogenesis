using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    public float timeBtwSpawns;
    public float startTimeBtwSpawns;
    public GameObject echo;
    void Update()
    {
        if (timeBtwSpawns <= 0)
        {
            GameObject instance = Instantiate(echo, transform.position, Quaternion.identity);
            timeBtwSpawns = startTimeBtwSpawns;
            Destroy(instance, 2f);
        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }
}
