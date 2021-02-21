using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrappyWorkAround : MonoBehaviour
{

    GameObject playerBall;
    Cannon cannonScript;


    // Start is called before the first frame update
    void Start()
    {
        cannonScript = gameObject.GetComponent<Cannon>();

        cannonScript.enabled = !cannonScript.enabled;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObject.GetComponent<Cannon>().enabled = gameObject.GetComponent<Cannon>().enabled;
            Debug.Log("bruh");
        }

    }

 }
