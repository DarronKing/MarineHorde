﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;

    public void TakeDamage(int dmg)
    {
        health -= dmg;
    }

    private void Update()
    {
        if (health <= 0)
            Destroy(gameObject);
    }
}
