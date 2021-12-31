using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCollider : MonoBehaviour
{
    public BoxCollider2D col;
    public BoxCollider2D col2;
    float MaxX;
    float MinX;
    float MaxY;
    float MinY;

    public Photon photon = Photon.instance;

    void Start()
    {
        float size = Camera.main.GetComponent<Camera>().orthographicSize;
        MaxY = size;
        MinY = -size;
        MaxX = size*Screen.width/Screen.height;
        MinX = -size*Screen.width/Screen.height;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.name == "Photon")
        {
            other.attachedRigidbody.velocity = Vector2.Reflect(other.attachedRigidbody.velocity, new Vector2(1, 0));
        }
    }
}
