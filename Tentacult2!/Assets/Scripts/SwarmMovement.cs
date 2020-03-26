using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwarmMovement : MonoBehaviour
{
    public Transform lookAt;
    public float attackRange;
    public float attackSpeed;
    public int damage = 1;
    public GameObject bulletPrefab;

    Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Target").transform;
        attackRange = 10;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }

    public void Shoot(Health enemy)
    {
        //TODO muzzle flash
        enemy.TakeDamage(damage);
    }
}
