using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragShoot : MonoBehaviour
{
    public float power;
    public float maxDrag;
    public Rigidbody rb;

    public Vector3 dragStartPos;
    public Vector3 dragReleasePos;

    public Touch touch;

    public bool isShoot;
    public bool canMove;

    public float forceMultiplier;

    public float speed;
    public float moveLeft;
    public float moveRight;

    public TimeManager timeManager;
    public float waitForMove = 100;

    public float ballVelocity;

    public float ballHeightVelocity;

    public bool playerInCannon;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        canMove = false;
        isShoot = false;
        playerInCannon = false;
    }

    private void Update()
    {

        ballVelocity = rb.velocity.magnitude;
        ballHeightVelocity = rb.velocity.y;

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                DragStart();
            }

            /*if (touch.phase == TouchPhase.Moved)
            {
                Dragging();
            }*/

            if (touch.phase == TouchPhase.Ended)
            {
                DragRelease();

                if (isShoot)
                {
                    timeManager.UndoSlowmotion();
                }
                
            }

            if (touch.position.x < Screen.width/2 && canMove && moveLeft > 0)
            {
                //transform.Translate(-speed * Time.unscaledDeltaTime, 0, 0);
                rb.AddForce(-speed, 0, 0);
                moveLeft -= 0.1f;

                if (isShoot && canMove && waitForMove <= 0)
                {
                    timeManager.DoSlowmotion();
                }
                
            }

            if (touch.position.x > Screen.width / 2 && canMove && moveRight > 0)
            {
                //transform.Translate(speed * Time.unscaledDeltaTime, 0, 0);
                rb.AddForce(speed, 0, 0);
                moveRight -= 0.1f;

                if (isShoot && canMove && waitForMove <= 0)
                {
                    timeManager.DoSlowmotion();
                }
            }
        }

        if (canMove && playerInCannon == false) 
        {
            if (waitForMove > 0)
            {
                waitForMove -= 1;
            }
            else if (waitForMove < 0)
            {
                waitForMove -= 0;
                waitForMove = 0;
            }
        }
    }

    void DragStart()
    {
        dragStartPos = touch.position;
    }

    /*void Dragging()
    {

    }*/

    void DragRelease()
    {
        dragReleasePos = touch.position;
        Shoot(Force: dragStartPos - dragReleasePos);
    }

    void Shoot(Vector3 Force)
    {
        if(isShoot == false && playerInCannon == false)
        {
            rb.AddForce(new Vector3(Force.x, Force.y, z: Force.y) * forceMultiplier);
            isShoot = true;
            canMove = true;
        }

    }
}
