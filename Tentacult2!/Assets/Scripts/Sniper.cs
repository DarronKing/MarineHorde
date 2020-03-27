using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    enum State
    {
        Roaming = 0,
        Shooting = 1,
        Charging = 2
    }

    public Transform swarmLocation;

    public float range;
    public float health;
    public float chargeSpeedWhileFiring;
    public float fireRate; // time between charging beam and firing
    public float reloadTime;
    float currentCharge;
    Animator anim;

    State sniperState;

    void Start()
    {
        sniperState = State.Roaming;
        currentCharge = 0f;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetInteger("SniperState", (int)sniperState);
        if (reloadTime > currentCharge && sniperState != State.Shooting)
        {
            currentCharge += Time.deltaTime;
            sniperState = State.Charging;
            Flee();
        }
        else if (sniperState != State.Shooting)
        {
            sniperState = State.Roaming;
            Roam();
        }

        if ((Vector3.Distance(swarmLocation.position, transform.position) <= range) && sniperState == State.Roaming)
        {
            currentCharge = 0f;
            sniperState = State.Shooting;
            StartCoroutine("Shoot");
        }

        print("Sniper State: " + sniperState);
    }

    void Roam()
    {
        //TODO create AI that patrols between points  
    }

    void Flee()
    {
        //TODO create AI that runs to max distance from player
    }

    IEnumerator Shoot()
    {
        //TODO raycast to center of swarm
        transform.LookAt(swarmLocation);
        yield return new WaitForSeconds(chargeSpeedWhileFiring);
        //TODO freeze raycast
        yield return new WaitForSeconds(fireRate);
        //TODO kill player raycast is touching
        sniperState = State.Roaming;
    }
}
