using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{
    public GameObject prefab;
    public int amount;

    private void OnDestroy()
    {
        for(int i = 0; i < amount; i++)
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}
