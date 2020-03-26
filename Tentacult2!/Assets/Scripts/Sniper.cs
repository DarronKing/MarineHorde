using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    enum State
    {
        Roaming,
        Shooting,
        Charging
    }

    public Transform swarmLocation;
    public float range;
    public float health;
    public float chargeSpeedWhileFiring;
    public float fireRate;
    public float chargeSpeedAfterFire;
    float currentCharge;

    State sniperState;

    void Start()
    {
        sniperState = State.Roaming;
        currentCharge = 0f;
    }

    void Update()
    {
        if (chargeSpeedAfterFire > currentCharge && sniperState != State.Shooting)
        {
            currentCharge += Time.deltaTime;
            sniperState = State.Charging;
        }
        else
            sniperState = State.Roaming;

        if ((Vector3.Distance(swarmLocation.position, transform.position) <= range) && sniperState == State.Roaming)
        {
            currentCharge = 0f;
            sniperState = State.Shooting;
            StartCoroutine("Shoot");
        }

        print("Sniper State: " + sniperState);
    }

    IEnumerator Shoot()
    {
        print("Sniper Shooting");
        //TODO raycast to center of swarm
        yield return new WaitForSeconds(chargeSpeedWhileFiring);
        //TODO freeze raycast
        yield return new WaitForSeconds(fireRate);
        //TODO kill player raycast is touching
        sniperState = State.Roaming;
    }
}
