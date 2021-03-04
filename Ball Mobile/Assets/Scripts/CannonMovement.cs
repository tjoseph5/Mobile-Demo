using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMovement : MonoBehaviour
{

    private float timer = 0.0f;
    [HideInInspector]public float speed; //The speed of movement (Hidden because the Cannon's movementSpeed variable will be used to dictate speed in the inspector instead)
    public float flipTimer; //How long the cannon will move in a given direction before it starts moving in the opposite direction

    public enum DirectionalMovement { idle, left_right, up_down, forward_backward }; //Directional states (idle doesn't move, left_right moves by Z axis, up_down moves by Y axis, and forward_backward move by X axis)
    public DirectionalMovement direction = DirectionalMovement.idle;


    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime; //Makes timer equal the computer's time

        switch (direction) //Defines each state (refer to the comments above)
        {
            case DirectionalMovement.idle: transform.Translate(0, 0, 0); break;
            case DirectionalMovement.forward_backward: transform.Translate(speed * Time.deltaTime, 0, 0); break;
            case DirectionalMovement.up_down: transform.Translate(0, speed * Time.deltaTime, 0); break;
            case DirectionalMovement.left_right: transform.Translate(0, 0, speed * Time.deltaTime); break;
        }

        if (timer >= flipTimer) //Checks to see if the timer surpasses flipTimer. The cannon will move the opposite direction if the timer surpasses the flipTimer 
        {
            speed *= -1; //multiplies whatever value speed current is by -1 to have it to the opposite direction
            timer = 0; //sets timer back to 0
        }
    }
}
