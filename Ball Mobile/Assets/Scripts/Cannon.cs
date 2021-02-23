using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private Transform shotPos;
    GameObject playerBall;
    DragShoot playerBallVel;
    bool inMyCannon;
    CannonMovement cannonMovement;

    public float cannonstrength;
    Vector3 shotDirection = -Vector3.right;

    public float movementSpeed;

    void Start()
    {
        inMyCannon = false;
        shotPos = transform.GetChild(0).transform;
        playerBall = GameObject.FindGameObjectWithTag("Player");
        playerBallVel = playerBall.GetComponent<DragShoot>();
        cannonMovement = gameObject.GetComponent<CannonMovement>();
    }


    void FixedUpdate()
    {

        shotDirection = -transform.right;

        if (inMyCannon)
        {

            

            if (Input.touchCount > 0)
            {
                if (playerBallVel.touch.phase == TouchPhase.Began)
                {
                    CannonFire();
                }
            }

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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && playerBallVel.waitForMove == 0)
        {
            playerBallVel.playerInCannon = true;
            inMyCannon = true;

            if (inMyCannon && playerBallVel.playerInCannon)
            {
                switch (cannonMovement.direction)
                {
                    case CannonMovement.DirectionalMovement.idle: cannonMovement.speed = 0; break;
                    case CannonMovement.DirectionalMovement.forward_backward: cannonMovement.speed = movementSpeed; break;
                    case CannonMovement.DirectionalMovement.up_down: cannonMovement.speed = movementSpeed; break;
                    case CannonMovement.DirectionalMovement.left_right: cannonMovement.speed = movementSpeed; break;
                }
            }
        }
    }

    void CannonFire()
    {
        if (playerBallVel.isShoot && playerBallVel.playerInCannon)
        {
            playerBallVel.playerInCannon = false;
            playerBallVel.canMove = true;
            playerBall.GetComponent<Rigidbody>().velocity = cannonstrength * shotDirection;
            inMyCannon = false;
            gameObject.GetComponent<CannonMovement>().direction = CannonMovement.DirectionalMovement.idle;
            playerBallVel.timeManager.DoSlowmotion();
        }

    }
}
