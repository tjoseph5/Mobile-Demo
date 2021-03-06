﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShatter : MonoBehaviour
{

    GameObject playerBall; //The player Ball
    public GameObject [] destroyedVersion = new GameObject [5]; //The shattered versions
    Renderer renderer; //gameObject's renderer

    public enum TargetType { pointTen, pointTwenty, pointFifty, pointHundred, pointThousand }; //list of different types of targets. Each variable has their own unique score variable, scales, and material color
    public TargetType targetTypes = TargetType.pointTen;

    int score; //score the player recieves when the target is hit
    TargetManager targetManager;

    [SerializeField] Material[] materials = new Material[5];

    [SerializeField] ParticleSystem sparks;

    // Start is called before the first frame update
    void Start()
    {

        renderer = gameObject.GetComponent<Renderer>();
        playerBall = GameObject.FindGameObjectWithTag("Player");
        targetManager = GameObject.Find("Target Manager").GetComponent<TargetManager>();

        //This switch enum defines each list variable's score amount, size, color, and name
        switch (targetTypes)
        {
            case TargetType.pointTen:
                renderer.material.color = Color.green;
                gameObject.GetComponent<MeshRenderer>().material = materials[0];
                gameObject.transform.localScale = new Vector3(5, 5, 0.7f);
                gameObject.name = "Ten";

                score = 10;
                break;
            case TargetType.pointTwenty:
                renderer.material.color = Color.blue;
                gameObject.GetComponent<MeshRenderer>().material = materials[1];
                gameObject.transform.localScale = new Vector3(4, 4, 0.7f);
                gameObject.name = "Twenty";

                score = 20;
                break;
            case TargetType.pointFifty:
                renderer.material.color = Color.yellow;
                gameObject.GetComponent<MeshRenderer>().material = materials[2];
                gameObject.transform.localScale = new Vector3(3, 3, 0.7f);
                gameObject.name = "Fifty";

                score = 50;
                break;
            case TargetType.pointHundred:
                renderer.material.color = Color.cyan;
                gameObject.GetComponent<MeshRenderer>().material = materials[3];
                gameObject.transform.localScale = new Vector3(2, 2, 0.7f);
                gameObject.name = "Hundred";

                score = 100;
                break;
            case TargetType.pointThousand:
                renderer.material.color = Color.white;
                gameObject.GetComponent<MeshRenderer>().material = materials[4];
                gameObject.transform.localScale = new Vector3(1, 1, 0.3f);
                gameObject.name = "Thousand";

                score = 1000;
                break;
        }

        /* //Sets localScale of instantiated shatter object to the gameObject's current scale vectors
        foreach (GameObject gameObj in destroyedVersion)
        {
            gameObj.GetComponentInChildren<Transform>().localScale = gameObject.transform.localScale;
        }
        */
    }

    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == playerBall)
        {
            targetManager.levelScore += score;
            //StartCoroutine(DestroyBuildDestroy());
            Destroy(gameObject);
            switch (targetTypes) //Instantiates different shattered target types depending on their variable
            {
                case TargetType.pointTen:
                    gameObject.GetComponent<MeshCollider>().enabled = false;
                    Instantiate(destroyedVersion[0], transform.position, transform.rotation);
                    targetManager.ten += 1;
                    break;
                case TargetType.pointTwenty:
                    gameObject.GetComponent<MeshCollider>().enabled = false;
                    Instantiate(destroyedVersion[1], transform.position, transform.rotation);
                    targetManager.twenty += 1;
                    break;
                case TargetType.pointFifty:
                    gameObject.GetComponent<MeshCollider>().enabled = false;
                    Instantiate(destroyedVersion[2], transform.position, transform.rotation);
                    targetManager.fifty += 1;
                    break;
                case TargetType.pointHundred:
                    gameObject.GetComponent<MeshCollider>().enabled = false;
                    Instantiate(destroyedVersion[3], transform.position, transform.rotation);
                    targetManager.hundred += 1;
                    break;
                case TargetType.pointThousand:
                    gameObject.GetComponent<MeshCollider>().enabled = false;
                    Instantiate(destroyedVersion[4], transform.position, transform.rotation);
                    targetManager.thousand += 1;
                    break;
            }
            Instantiate(sparks, playerBall.transform.position, playerBall.transform.rotation);
        }
    }

    //Destroys gameObject after 1 second and deactivates it. No, I don't know why I named this Coroutine after that one live action Cartoon Network show starring Andrew W.K.
    IEnumerator DestroyBuildDestroy()
    {
        gameObject.SetActive(false); //This propertly removes the gameObject from the WindZones before being destroyed. Without this, the game would be constantly trying to access gameObjects that no longer exist, causing a memory leak 
        yield return new WaitForSeconds(5f); //Makes sure the previous line of code occurs first before completely deleting the object
        Destroy(gameObject);
    }
}
