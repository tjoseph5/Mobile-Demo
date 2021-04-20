using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DragShoot : MonoBehaviour
{
    public Rigidbody rb; //Player Rigidbody

    public Vector3 dragStartPos; //The vector3 that is equal to the player's start touch position
    public Vector3 dragReleasePos; //The vector3 that is equal to the player's touch position when they release their finger from the screen

    public Touch touch; //Touch Variable

    public bool isShoot; //Bool that is used to check if the player has swiped to throw the ball
    public bool canMove; //Bool that is used to check if the player is able to move left and right

    public float forceMultiplier; //Float Variable that is used to multiply the Shoot() Force Vector 3 results. Essentially adds extra force to the ball for it to launch

    public float speed; //Float that determines the speed of the ball's left and right movement
    public float moveLeft; //Float that limits the amount of time the player can turn left in a round
    public float moveRight; //Float that limits the amount of time the player can turn right in a round

    public TimeManager timeManager; //The time manager gameObject. This is used to call the slow motion effect whenever the player move left or right
    public float waitForMove; //Prevents the ball from entering slow motion at the start of getting launched

    public float ballVelocity; //Gets the ball's rigidbody velocity

    public float ballHeightVelocity; //Gets the ball's rigidbody y velocity

    public bool playerInCannon; // bool that checks if the player is currently inside a cannon

    public float velocityCap; //Float that caps the velocity of the ball

    GameObject starterPlane; // The plane where the ball is spawned on
    public Transform spawnPoint;

    bool canFlick;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); //Sets rb to player rigidbody
        canMove = false;
        isShoot = false;
        playerInCannon = false;
        starterPlane = GameObject.FindGameObjectWithTag("Start Plane"); //Gets the plane that the player is spawned on
        spawnPoint = GameObject.FindGameObjectWithTag("Spawn Point").transform;
        canFlick = false;
    }

    private void Update()
    {
        ballVelocity = rb.velocity.magnitude; //Sets ballVelocity to rb's velocity
        ballHeightVelocity = rb.velocity.y; //Sets ballHeightVelocity to rb's y velocity

        //Restarts the game when the ball is standing still
        if (canMove && isShoot && ballVelocity < 0.09)
        {
            StartCoroutine(GameRestart());
        }

        //Checks if the player is touching the screen and which phase of touch they're acting upon
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0); //Sets the touch variable to the player's touch

            if (touch.phase == TouchPhase.Began) //Checks to see if the player has touched the screen
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
                RaycastHit hit;

                if(Physics.Raycast(ray, out hit))
                {
                    if(hit.collider != null)
                    {
                        if(hit.collider.gameObject.name == "Main Ball")
                        {
                            Debug.Log("the ball is being touched");
                            DragStart();
                        }
                    }
                }  
            }

            /*if (touch.phase == TouchPhase.Moved)
            {
                Dragging();
            }*/

            if (touch.phase == TouchPhase.Ended) //Checks to see if the player is no longer touching the screen after touching it
            {
                if(canFlick == true)
                {
                    DragRelease();
                }

                if (isShoot) //Keeps the game from continuously remaining in slowmotion after the player is no longer moving left or right
                {
                    timeManager.UndoSlowmotion(); //Function from the TimeManager script that sets time back to normal
                }
                
            }

            //Checks if the player is touching the left side of the screen after launching the ball. This will grant them the ability to dictate the ball to the left side of the x axis (moving the ball left)
            if (touch.position.x < Screen.width/2 && canMove && moveLeft > 0)
            {
                //transform.Translate(-speed * Time.unscaledDeltaTime, 0, 0);
                rb.AddForce(-speed, 0, 0);
                //moveLeft -= 1 * Time.deltaTime;

                if (isShoot && canMove && waitForMove <= 0) //Slow Motion effect
                {
                    timeManager.DoSlowmotion(); //Function from the TimeManager script that sets time to slowdown
                }
                
            }

            //Checks if the player is touching the right side of the screen after launching the ball. This will grant them the ability to dictate the ball to the right side of the x axis (moving the ball right)
            if (touch.position.x > Screen.width / 2 && canMove && moveRight > 0)
            {
                //transform.Translate(speed * Time.unscaledDeltaTime, 0, 0);
                rb.AddForce(speed, 0, 0);
                //moveRight -= 0.1f;

                if (isShoot && canMove && waitForMove <= 0) //Slow Motion effect
                {
                    timeManager.DoSlowmotion(); //Function from the TimeManager script that sets time to slowdown
                }
            }
        }

        if (canMove && playerInCannon == false) //Subtracts waitForMove until it reaches zero. Not doing this permanately disables left and right movement
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

    private void FixedUpdate()
    {
        if(rb.velocity.magnitude > velocityCap) //Limits velocity for the ball so it won't break the sound barrier and cause multiple glitches with collision detection
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, velocityCap);
        }
    }

    void DragStart()
    {
        dragStartPos = touch.position; //Sets the dragStartPos to the player's current touch position
        canFlick = true;
    }

    /*void Dragging()
    {

    }*/

    void DragRelease()
    {
        dragReleasePos = touch.position; //Sets the dragReleasePos to the player's current touch position
        canFlick = false;
        Shoot(Force: dragStartPos - dragReleasePos); //Launches the ball by using the difference between the dragStartPos and the dragReleasePos, and multiplying it by the forceMultipler float
    }

    //This function is used to launch the ball after the ball releases their finger from the screen at the start of the scene
    public void Shoot(Vector3 Force)
    {
        if(isShoot == false && playerInCannon == false)
        {
            rb.AddForce(new Vector3(Force.x, Force.y, z: Force.y) * forceMultiplier);
            isShoot = true;
            canMove = true;

            starterPlane.SetActive(false); //disables the plane gameObject
        }

    }

    IEnumerator GameRestart()
    {
        starterPlane.SetActive(true);
        yield return new WaitForSeconds(1f);
        Respawn(spawnPoint);
        gameObject.transform.eulerAngles = Vector3.zero;
    }

    
    void Respawn(Transform transform)
    {
        if(isShoot && canMove)
        {
            isShoot = false;
            canMove = false;
            waitForMove = 1;
            gameObject.transform.position = transform.position;
            gameObject.transform.rotation = transform.rotation;
        }
        
    }


    //Restarts the game
    /*
    IEnumerator GameRestart()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    */
}
