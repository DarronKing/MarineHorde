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
    HashSet<Collider> enemies;
    HashSet<Collider> allies;
    SphereCollider sphere;
    DrawCirlce circle;
    bool shooting;
    int number;
    public Transform closestEnemy;
    
    // Start is called before the first frame update
    void Start()
    {
        children = GameObject.FindGameObjectsWithTag("Player");
        childrenAnimators = new List<Animator>();
        sphere = GetComponent<SphereCollider>();
        circle = GetComponent<DrawCirlce>();
        foreach (GameObject child in children)
        {
            childrenAnimators.Add(child.GetComponent<Animator>());
        }
        enemies = new HashSet<Collider>();
        allies = new HashSet<Collider>();
        shooting = false;


        // remove the following code 
        number = (int)children.Length / 4;//
        circle.xradius = circle.yradius = 9 + number;//
        sphere.radius = attackRange = 10 + number;//
        circle.CreatePoints();//
    }

    // Update is called once per frame
    void Update()
    {
        number = (int)children.Length / 4;//
        circle.xradius = circle.yradius = sphere.radius = attackRange = 10 + number;//
        //TODO cannot update visual circle without calling CreatePoints()
        
        CalculateCenter();
        closestEnemy = ClosestEnemy();
        if (closestEnemy != null)
        {
            foreach (GameObject child in children)
            {
                child.transform.LookAt(closestEnemy);
            }
        }

        if ((Vector3.Distance(transform.position, closestEnemy.position) <= attackRange))
        {
            foreach (GameObject child in children)
            {
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

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
            enemies.Add(other);
        if (other.CompareTag("Player"))
            allies.Add(other);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
            enemies.Remove(other);
        if (other.CompareTag("Player"))
            allies.Remove(other);
    }

    Transform ClosestEnemy()
    {
        Transform closest = null;// make this the first element of hashset.
        float distToClosest = 0;
        float newDist;
        foreach (Collider enemy in enemies)
        {
            if (enemy == null)
            {
                enemies.Remove(enemy);
            }

            if (closest != null)
            {
                newDist = Vector3.Distance(enemy.transform.position, transform.position);
                if (distToClosest > newDist)
                {
                    closest = enemy.transform;
                    distToClosest = newDist;
                }
            }
            else
            {
                closest = enemy.transform;
                distToClosest = Vector3.Distance(enemy.transform.position, transform.position);
            }
        }
        return closest;
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
        Health enemyHealth = closestEnemy.GetComponent<Health>();
        SwarmMovement allyScript;
        foreach(Collider ally in allies)
        {
            allyScript = ally.GetComponent<SwarmMovement>();
            enemyHealth.TakeDamage(allyScript.damage);
        }
        //TODO play animation when shooting = true

        yield return new WaitForSeconds(attackSpeed);
        shooting = false;
    }
}
