using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRenderer : MonoBehaviour
{

    public enum TargetType { green, blue, yellow, cyan, white }; //This sets every shard to a specific color
    Renderer renderer;
    public TargetType targetTypes = TargetType.green;

    [SerializeField] Material[] materials = new Material[5];

    // Start is called before the first frame update
    void Start()
    {

        renderer = gameObject.GetComponent<Renderer>();

        switch (targetTypes) //Defines each color state
        {
            case TargetType.green:
                renderer.material.color = Color.green;
                gameObject.GetComponent<MeshRenderer>().material = materials[0];
                break;
            case TargetType.blue:
                renderer.material.color = Color.blue;
                gameObject.GetComponent<MeshRenderer>().material = materials[1];
                break;
            case TargetType.yellow:
                renderer.material.color = Color.yellow;
                gameObject.GetComponent<MeshRenderer>().material = materials[2];
                break;
            case TargetType.cyan:
                renderer.material.color = Color.cyan;
                gameObject.GetComponent<MeshRenderer>().material = materials[3];
                break;
            case TargetType.white:
                renderer.material.color = Color.white;
                gameObject.GetComponent<MeshRenderer>().material = materials[4];
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
