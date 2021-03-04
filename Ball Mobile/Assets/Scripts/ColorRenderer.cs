using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRenderer : MonoBehaviour
{

    public enum TargetType { green, blue, yellow, cyan, white }; //This sets every shard to a specific color
    Renderer renderer;
    public TargetType targetTypes = TargetType.green;

    // Start is called before the first frame update
    void Start()
    {

        renderer = gameObject.GetComponent<Renderer>();

        switch (targetTypes) //Defines each color state
        {
            case TargetType.green:
                renderer.material.color = Color.green;
                break;
            case TargetType.blue:
                renderer.material.color = Color.blue;
                break;
            case TargetType.yellow:
                renderer.material.color = Color.yellow;
                break;
            case TargetType.cyan:
                renderer.material.color = Color.cyan;
                break;
            case TargetType.white:
                renderer.material.color = Color.white;
                break;
        }

        StartCoroutine(DestroyBuildDestroy()); //Automatically starts a coroutine that removes the shards from the scene in order to clean up the scenes and also for optimization purposes

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    //Destroys gameObject after 1 second and deactivates it. No, I don't know why I named this Coroutine after that one live action Cartoon Network show starring Andrew W.K.
    IEnumerator DestroyBuildDestroy()
    {
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false); //This propertly removes the gameObject from the WindZones before being destroyed. Without this, the game would be constantly trying to access gameObjects that no longer exist, causing a memory leak 
        yield return new WaitForSeconds(1f); //Makes sure the previous line of code occurs first before completely deleting the object
        Destroy(gameObject);
    }
}
