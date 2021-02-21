using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShatter : MonoBehaviour
{

    GameObject playerBall;
    public GameObject [] destroyedVersion = new GameObject [5];
    Renderer renderer;

    public enum TargetType { pointTen, pointTwenty, pointFifty, pointHundred, pointThousand };
    public TargetType targetTypes = TargetType.pointTen;

    int score;

    // Start is called before the first frame update
    void Start()
    {

        renderer = gameObject.GetComponent<Renderer>();
        playerBall = GameObject.FindGameObjectWithTag("Player");

        switch (targetTypes)
        {
            case TargetType.pointTen:
                renderer.material.color = Color.green;
                score = 10;
                break;
            case TargetType.pointTwenty:
                renderer.material.color = Color.blue;
                score = 20;
                break;
            case TargetType.pointFifty:
                renderer.material.color = Color.yellow;
                score = 50;
                break;
            case TargetType.pointHundred:
                renderer.material.color = Color.cyan;
                score = 100;
                break;
            case TargetType.pointThousand:
               renderer.material.color = Color.white;
                score = 1000;
                break;
        }
    }

    void Update()
    {
        foreach(GameObject gameObj in destroyedVersion)
        {
            gameObj.transform.localScale = gameObject.transform.localScale;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == playerBall)
        {
            switch (targetTypes)
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
            Destroy(gameObject);
        }
    }
}
