using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    List<Rigidbody> ObjectRbs = new List<Rigidbody>();

    public float bounceStrength;

    Vector3 bounceDirection = Vector3.up;

    DragShoot playerBallV;

    private void Start()
    {
        playerBallV = GameObject.FindGameObjectWithTag("Player").GetComponent<DragShoot>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody objectRigid = other.gameObject.GetComponent<Rigidbody>();

        if (objectRigid != null)
        {
            ObjectRbs.Add(objectRigid);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody objectRigid = other.gameObject.GetComponent<Rigidbody>();

        if (objectRigid != null)
        {
            ObjectRbs.Remove(objectRigid);
        }
    }

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
