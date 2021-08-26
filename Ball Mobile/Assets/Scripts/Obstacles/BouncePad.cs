using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    List<Rigidbody> ObjectRbs = new List<Rigidbody>(); //A list that contains all of the rigidbodies of objects that have interacted with a bounce pad

    public float bounceStrength; //the strength of a bounce pad

    Vector3 bounceDirection = Vector3.up; //the direction that the bounce pad will be sending any object

    DragShoot playerBallV; //Ball's DragShoot Component

    private void Start()
    {
        playerBallV = GameObject.FindGameObjectWithTag("Player").GetComponent<DragShoot>(); //Sets variable to Player ball
    }

    //This adds any gameObject with a rigidbody component to the ObjectRbs list on collision
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody objectRigid = other.gameObject.GetComponent<Rigidbody>();

        if (objectRigid != null)
        {
            ObjectRbs.Add(objectRigid);
        }
    }

    //This removes any gameObject with a rigidbody component to the ObjectRbs list out of collision
    private void OnTriggerExit(Collider other)
    {
        Rigidbody objectRigid = other.gameObject.GetComponent<Rigidbody>();

        if (objectRigid != null)
        {
            ObjectRbs.Remove(objectRigid);
        }
    }

    //This updates will bounce any object with a rigid component away in the respected Bouncepad's Y axis
    private void FixedUpdate()
    {

        bounceDirection = transform.up;

        if (ObjectRbs.Count > 0)
        {
            foreach (Rigidbody rigid in ObjectRbs)
            {
                rigid.velocity = bounceDirection * (bounceStrength * playerBallV.ballVelocity);
            }
        }
    }
}
