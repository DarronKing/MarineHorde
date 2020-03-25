using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmCenter : MonoBehaviour
{
    //TODO find closest enemy
    //TODO fire when enemy is in range

    public float attackRange;
    public float attackSpeed;

    Vector3 centroid = Vector3.zero;
    GameObject[] children;
    List<Animator> childrenAnimators;
    bool shooting;
    public Transform closestEnemy;
    
    // Start is called before the first frame update
    void Start()
    {
        children = GameObject.FindGameObjectsWithTag("Player");
        childrenAnimators = new List<Animator>();
        foreach (GameObject child in children)
        {
            childrenAnimators.Add(child.GetComponent<Animator>());
        }
        shooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateCenter();
        if (Vector3.Distance(transform.position, closestEnemy.position) < attackRange)
        {
            foreach (GameObject child in children)
            {
                child.transform.LookAt(closestEnemy);
                if (!shooting)
                {
                    shooting = true;
                    StartCoroutine("Shoot");
                }
            }
        }
        foreach (Animator anim in childrenAnimators)
        {
            anim.SetBool("Running", shooting);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    void CalculateCenter()
    {
        centroid = Vector3.zero;
        foreach (GameObject child in children)
        {
            centroid += child.transform.position;
        }
        centroid /= children.Length;

        transform.position = centroid;
    }

    IEnumerator Shoot()
    {
        //TODO damage enemy
        //TODO play animation when shooting = true
        print("shooting");

        yield return new WaitForSeconds(attackSpeed);
        shooting = false;
    }
}
