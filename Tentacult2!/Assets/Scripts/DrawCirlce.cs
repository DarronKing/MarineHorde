﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class DrawCirlce : MonoBehaviour
{
    
    public int segments = 50;
    public float xradius = 5;
    public float yradius = 5;
    LineRenderer line;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();

        line.SetVertexCount(segments + 1);
        line.useWorldSpace = false;
        CreatePoints();
    }

    public void CreatePoints()
    {
        float x;
        float y;
        float z;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition(i, new Vector3(x, .1f, y));

            angle += (360f / segments);
        }
    }
}
