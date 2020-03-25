using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwarmMovement : MonoBehaviour
{
    public Transform lookAt;
    public float attackRange;
    public float attackSpeed;
    public GameObject bulletPrefab;

    Transform target;
    NavMeshAgent agent;
    Transform bulletEmitter;
    bool shooting;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Target").transform;
        bulletEmitter = transform; //TODO change this to a gameobject later
        shooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
        if (Vector3.Distance(transform.position, lookAt.position) < attackRange)
        {
            gameObject.transform.LookAt(lookAt);
            if (!shooting)
            {
                shooting = true;
                StartCoroutine("Shoot");
            }
                
        }
           
    }

    IEnumerator Shoot()
    {
        
        //TODO fire bullet
        GameObject bulletHandler;
        bulletHandler = Instantiate(bulletPrefab, bulletEmitter.position, bulletEmitter.rotation) as GameObject;

        yield return new WaitForSeconds(attackSpeed);
        //bulletHandler.transform.Rotate(Vector3.left * 90);
        shooting = false;

    }

}
