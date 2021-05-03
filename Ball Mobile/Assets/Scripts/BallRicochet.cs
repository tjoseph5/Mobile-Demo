using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRicochet : MonoBehaviour
{
    //This script helps the ball ricochet from a Bumper Pin
    private Rigidbody rb;
    Vector3 lastVelocity;

    public float speedStrengh; //Sets the knockback strength of the object

    [SerializeField] ParticleSystem sparks;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        lastVelocity = rb.velocity; //Takes whatever direction the ball was moving and stores in into a vector3 variable
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ricochet")
        {
            Instantiate(sparks, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            var speed = lastVelocity.magnitude * speedStrengh; //Multiples the ball velocity's magnitude by the speedStrength to essentially double the ricochet effect 
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal); //I'm still new to Vector3.Reflect so I don't exactly what's happening other than knowing that this sets the ball in the opposite direction from lastVelocity

            rb.velocity = direction * Mathf.Max(speed, 0f); //This also does a thing I can't fully comprehend yet lol. I do have a general idea of what is happening at the very least, which is that this is setting the ball's velocity to equal direction :/
        }
    }
}
