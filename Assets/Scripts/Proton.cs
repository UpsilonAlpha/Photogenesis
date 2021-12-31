﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proton : MonoBehaviour
{
    int col = 0;
    Rigidbody2D rb;
    Vector2[] dirs = {new Vector2(1,1), new Vector2(1,-1), new Vector2(-1,0), new Vector2(-1,-1), new Vector2(-1,1), new Vector2(1,0),};
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity = dirs[Random.Range(0,6)].normalized*1;
    }

    public void Break()
    {
        col++;
        if (col==1)
        {
            LeanTween.scale(gameObject, new Vector3(0,0,0), 1f);
            ProtonGenerator.RemoveProton(gameObject);
        }
    }
}
