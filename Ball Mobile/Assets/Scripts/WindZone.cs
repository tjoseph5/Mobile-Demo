using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{

    public List<Rigidbody> WindZoneRbs = new List<Rigidbody>();

    Vector3 windDirection = Vector3.forward;

    public float windStrength;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody objectRigid = other.gameObject.GetComponent<Rigidbody>();

        if(objectRigid != null)
        {
            WindZoneRbs.Add(objectRigid);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody objectRigid = other.gameObject.GetComponent<Rigidbody>();

        if (objectRigid != null)
        {
            WindZoneRbs.Remove(objectRigid);
        }
    }

    private void FixedUpdate()
    {
        windDirection = transform.forward;

        if(WindZoneRbs.Count > 0)
        {
            foreach(Rigidbody rigid in WindZoneRbs)
            {
                rigid.AddForce(windDirection * windStrength);
            }
        }
    }

}
