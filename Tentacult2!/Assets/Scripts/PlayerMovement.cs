using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 movement;
    Rigidbody rb;

    float horizontal;
    float vertical;
    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        movement.Set(horizontal, 0, vertical);
        movement.Normalize();
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);

        //rb.velocity = new Vector3(horizontal * speed, rb.velocity.y, vertical * speed);
    }
}
