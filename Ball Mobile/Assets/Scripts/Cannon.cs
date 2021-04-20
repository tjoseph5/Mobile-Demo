using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private Transform shotPos; //Player's set position when entering a cannon
    GameObject playerBall; //the Player Ball
    DragShoot playerBallVel; //The velocity of the player ball
    bool inMyCannon; //Specifies if the player has entered an indivisual cannon rather than any regular cannon (this fixes a glitch that causes the player to only launch from the latest cannon direction)
    CannonMovement cannonMovement; //The CannonMovement script that allows some cannons to move depending on their state

    public float cannonstrength; //Launch strength
    Vector3 shotDirection = -Vector3.right; //the Direction of the launch/cannon

    public float movementSpeed; //the speed of the Cannon's movement

    void Start()
    {
        inMyCannon = false;
        shotPos = transform.GetChild(0).transform; //Gets the shot position first
        playerBall = GameObject.FindGameObjectWithTag("Player");
        playerBallVel = playerBall.GetComponent<DragShoot>();
        cannonMovement = gameObject.GetComponent<CannonMovement>();
        gameObject.name = "Cannon";
    }


    void FixedUpdate()
    {

        shotDirection = -transform.right; //Prevents the cannon from firing the player in the opposite direction of the nossle

        if (inMyCannon) //This if statement helps specify if the cannon has entered a specific cannon
        {

            

            if (Input.touchCount > 0)
            {
                if (playerBallVel.touch.phase == TouchPhase.Began)
                {
                    CannonFire(); //Launches cannon
                }
            }

            //Keeps the ball in the position of the shot position
            switch (playerBallVel.playerInCannon)
            {
                case true:

                    if (playerBallVel.isShoot == true)
                    {
                        playerBall.transform.position = shotPos.transform.position;
                        playerBall.transform.rotation = shotPos.transform.rotation;
                        playerBallVel.canMove = false;
                        playerBallVel.waitForMove = 3;
                    }
                    break;

                case false:
                    playerBall.transform.position = playerBall.transform.position;
                    playerBall.transform.rotation = playerBall.transform.rotation;
                    break;

            }
        }
    }

    //Dictates the following events of the player ball entering a cannon
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && playerBallVel.waitForMove == 0)
        {
            playerBallVel.playerInCannon = true;
            inMyCannon = true;

            if (inMyCannon && playerBallVel.playerInCannon)
            {
                switch (cannonMovement.direction) //This starts the cannon movement
                {
                    case CannonMovement.DirectionalMovement.idle: cannonMovement.speed = 0; break;
                    case CannonMovement.DirectionalMovement.forward_backward: cannonMovement.speed = movementSpeed; break;
                    case CannonMovement.DirectionalMovement.up_down: cannonMovement.speed = movementSpeed; break;
                    case CannonMovement.DirectionalMovement.left_right: cannonMovement.speed = movementSpeed; break;
                }
            }
        }
    }

    void CannonFire() //A function that launches the ball
    {
        if (playerBallVel.isShoot && playerBallVel.playerInCannon)
        {
            playerBallVel.playerInCannon = false; //Stops ball from being frozen in one position
            playerBallVel.canMove = true;
            playerBall.GetComponent<Rigidbody>().velocity = cannonstrength * shotDirection;
            inMyCannon = false;
            gameObject.GetComponent<CannonMovement>().direction = CannonMovement.DirectionalMovement.idle; //Sets the cannon's movement state back to zero
            playerBallVel.timeManager.DoSlowmotion(); //Triggers a 2 second slowmotion effect from the TimeManager script
        }

    }
}
