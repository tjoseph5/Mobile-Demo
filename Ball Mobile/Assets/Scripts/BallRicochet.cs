using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRicochet : MonoBehaviour
{

    private Rigidbody rb;
    Vector3 lastVelocity;

    public float speedStrengh;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ricochet")
        {
            var speed = lastVelocity.magnitude * speedStrengh;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            rb.velocity = direction * Mathf.Max(speed, 0f);
        }
    }
}
