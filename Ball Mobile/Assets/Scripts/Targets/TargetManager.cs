using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetManager : MonoBehaviour
{
    [Header("Target Count")]

    public int ten;
    [SerializeField] int tenMax;

    public int twenty;
    [SerializeField] int twentyMax;

    public int fifty;
    [SerializeField] int fiftyMax;

    public int hundred;
    [SerializeField] int hundredMax;

    public int thousand;
    [SerializeField] int thousandMax;

    public int targetCount;
    public int maxTargetCount;

    [Header("Score")]
    public int levelScore;

    [Header("Bool")]
    public bool targetsCompleted;

    [Header("Target UI")]

    [SerializeField] Text targetCountUI;
    [SerializeField] Text scoreDisplayUI;

    void Start()
    {
        targetsCompleted = false;
        TargetCount();
    }

    // Update is called once per frame
    void Update()
    {
        targetCount = ten + twenty + fifty + hundred + thousand;

        TargetUI();

        if(targetCount >= maxTargetCount)
        {
            targetsCompleted = true;
        }
        else
        {
            targetsCompleted = false;
        }
    }

    void TargetCount()
    {
        foreach(GameObject target in GameObject.FindObjectsOfType<GameObject>())
        {
            if (target.GetComponent<TargetShatter>())
            {
                switch (target.GetComponent<TargetShatter>().targetTypes)
                {
                    case TargetShatter.TargetType.pointTen:
                        tenMax += 1;
                        break;

                    case TargetShatter.TargetType.pointTwenty:
                        twentyMax += 1;
                        break;

                    case TargetShatter.TargetType.pointFifty:
                        fiftyMax += 1;
                        break;

                    case TargetShatter.TargetType.pointHundred:
                        hundredMax += 1;
                        break;

                    case TargetShatter.TargetType.pointThousand:
                        thousandMax += 1;
                        break;
                }
            }
        }

        maxTargetCount = tenMax + twentyMax + fiftyMax + hundredMax + thousandMax;
    }

    void TargetUI()
    {
        targetCountUI.text = "Targets: " + targetCount + "/" + maxTargetCount;

        scoreDisplayUI.text = "Score: " + levelScore;
    }

    public void CannonReposition()
    {
        foreach(GameObject gameObject in GameObject.FindGameObjectsWithTag("Cannon"))
        {
            gameObject.transform.position = gameObject.GetComponent<CannonMovement>().originalPos;
            gameObject.GetComponent<CannonMovement>().timer = 0;
            gameObject.GetComponent<CannonMovement>().speed = 0;
            gameObject.GetComponent<CannonMovement>().direction = gameObject.GetComponent<CannonMovement>().storedDirection;
        }
    }
}
