using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpFollow : MonoBehaviour
{ 
    public Photon photon = Photon.instance;
    public float initPos;

    void Update()
    {
        if (photon.boosting)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, initPos + 100, -10), 2*Time.deltaTime);
            
            if (transform.position.y > initPos+99)
                photon.boosting = false;
        }
        else if (photon.transform.position.y != -4 && photon.rb.velocity.y >= 0)
        {
            initPos = transform.position.y;
            this.transform.position = new Vector3(0, Mathf.Lerp(this.transform.position.y, photon.transform.position.y + 3, Time.deltaTime), -10);
        }
    }
}
