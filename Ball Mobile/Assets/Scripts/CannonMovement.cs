using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMovement : MonoBehaviour
{

    private float timer = 0.0f;
    [HideInInspector]public float speed;
    public float flipTimer;

    public enum DirectionalMovement { idle, left_right, up_down, forward_backward };
    public DirectionalMovement direction = DirectionalMovement.idle;


    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;

        switch (direction)
        {
            case DirectionalMovement.idle: transform.Translate(0, 0, 0); break;
            case DirectionalMovement.forward_backward: transform.Translate(speed * Time.deltaTime, 0, 0); break;
            case DirectionalMovement.up_down: transform.Translate(0, speed * Time.deltaTime, 0); break;
            case DirectionalMovement.left_right: transform.Translate(0, 0, speed * Time.deltaTime); break;
        }

        if (timer >= flipTimer)
        {
            speed *= -1;
            timer = 0;
        }
    }
}
