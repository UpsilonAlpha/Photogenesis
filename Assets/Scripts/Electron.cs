using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electron : MonoBehaviour
{
    public int e;
    Rigidbody2D rb;
    Vector2[] dirs = {new Vector2(1,1), new Vector2(1,-1), new Vector2(-1,0), new Vector2(-1,-1), new Vector2(-1,1), new Vector2(1,0),};
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = dirs[Random.Range(0,6)].normalized*e;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Photon")
        {
            Time.timeScale = 0.1f;
            other.transform.position = this.transform.position;
            other.gameObject.GetComponent<Rigidbody2D>().velocity = rb.velocity*-2;
            rb.velocity = dirs[Random.Range(0,6)].normalized*e;
            other.GetComponent<Photon>().fireTime = 0.76f;
            other.GetComponent<Photon>().speaker.Play("Bzzt");
            Time.timeScale = 1;
        }
        if (other.tag == "Proton")
        {
            rb.velocity = dirs[Random.Range(0,6)].normalized*e;
        }
    }

    void Update()
    {
        rb.velocity = rb.velocity.normalized*e;
    }
}
