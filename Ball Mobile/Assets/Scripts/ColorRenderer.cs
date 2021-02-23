using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRenderer : MonoBehaviour
{

    public enum TargetType { green, blue, yellow, cyan, white };
    Renderer renderer;
    public TargetType targetTypes = TargetType.green;

    // Start is called before the first frame update
    void Start()
    {

        renderer = gameObject.GetComponent<Renderer>();

        switch (targetTypes)
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

        StartCoroutine(DestroyBuildDestroy());

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestroyBuildDestroy()
    {
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
