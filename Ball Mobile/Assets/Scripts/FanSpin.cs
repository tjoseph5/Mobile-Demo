using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//All this script does is animate the fans to spin
public class FanSpin : MonoBehaviour
{
    public float speed;
    GameObject windZone;
    Quaternion rotation;

    void Awake()
    {
        windZone = gameObject.transform.GetChild(0).gameObject;

        rotation = windZone.transform.rotation;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, speed);
    }

    void LateUpdate()
    {
        windZone.transform.rotation = rotation;
    }
}
