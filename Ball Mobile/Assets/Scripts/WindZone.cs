using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{

    public List<Rigidbody> WindZoneRbs = new List<Rigidbody>(); //A list that contains all of the rigidbodies of objects that have interacted with a windzone

    Vector3 windDirection = Vector3.forward; //the direction of the wind

    public float windStrength; //strength of each windzone.

    private void Update()
    {
        WindZoneRbs.RemoveAll(WindZoneRbs => WindZoneRbs == null);
    }

    //This adds any gameObject with a rigidbody component to the WindZoneRbs list on collision
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody objectRigid = other.gameObject.GetComponent<Rigidbody>();

        if(objectRigid != null)
        {
            WindZoneRbs.Add(objectRigid);
        }
    }

    //This removes any gameObject with a rigidbody component to the WindZoneRbs list out of collision
    private void OnTriggerExit(Collider other)
    {
        Rigidbody objectRigid = other.gameObject.GetComponent<Rigidbody>();

        if (objectRigid != null)
        {
            WindZoneRbs.Remove(objectRigid);
        }
    }

    //This updates will blow any object with a rigid component away in the respected windzone's Z axis
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
