using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {

        renderer = gameObject.GetComponent<Renderer>();
        playerBall = GameObject.FindGameObjectWithTag("Player");

        //This switch enum defines each list variable's score amount, size, color, and name
        switch (targetTypes)
        {
            case TargetType.pointTen:
                renderer.material.color = Color.green;
                gameObject.transform.localScale = new Vector3(5, 5, 0.7f);
                gameObject.name = "Ten";

                score = 10;
                break;
            case TargetType.pointTwenty:
                renderer.material.color = Color.blue;
                gameObject.transform.localScale = new Vector3(4, 4, 0.7f);
                gameObject.name = "Twenty";

                score = 20;
                break;
            case TargetType.pointFifty:
                renderer.material.color = Color.yellow;
                gameObject.transform.localScale = new Vector3(3, 3, 0.7f);
                gameObject.name = "Fifty";

                score = 50;
                break;
            case TargetType.pointHundred:
                renderer.material.color = Color.cyan;
                gameObject.transform.localScale = new Vector3(2, 2, 0.7f);
                gameObject.name = "Hundred";

                score = 100;
                break;
            case TargetType.pointThousand:
                renderer.material.color = Color.white;
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
            switch (targetTypes) //Instantiates different shattered target types depending on their variable
            {
                case TargetType.pointTen:
                    Instantiate(destroyedVersion[0], transform.position, transform.rotation);
                    break;
                case TargetType.pointTwenty:
                    Instantiate(destroyedVersion[1], transform.position, transform.rotation);
                    break;
                case TargetType.pointFifty:
                    Instantiate(destroyedVersion[2], transform.position, transform.rotation);
                    break;
                case TargetType.pointHundred:
                    Instantiate(destroyedVersion[3], transform.position, transform.rotation);
                    break;
                case TargetType.pointThousand:
                    Instantiate(destroyedVersion[4], transform.position, transform.rotation);
                    break;
            }
            StartCoroutine(DestroyBuildDestroy());
        }
    }

    //Destroys gameObject after 1 second and deactivates it. No, I don't know why I named this Coroutine after that one live action Cartoon Network show starring Andrew W.K.
    IEnumerator DestroyBuildDestroy()
    {
        gameObject.SetActive(false); //This propertly removes the gameObject from the WindZones before being destroyed. Without this, the game would be constantly trying to access gameObjects that no longer exist, causing a memory leak 
        yield return new WaitForSeconds(1f); //Makes sure the previous line of code occurs first before completely deleting the object
        Destroy(gameObject);
    }
}
