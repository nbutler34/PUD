using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GrappleScript : MonoBehaviour
{
    public LineRenderer line;
    DistanceJoint2D joint;
    public GameObject aimObject;
    public float maxDistance = 10f;
    public float step = 0.2f;
    public float fixedRotation = 0;
    public float orbitSpeed = 350.0f;
    public float swingSpeed = 200f;
    public float buttonPressRate = 0.1f;
    public LayerMask layerMask;
    public bool actionUnreleased;
    public bool isSwinging;
    public bool gameOver;
    Rigidbody2D playerRB;
    Vector3 targetPos;
    RaycastHit2D hit;
    Transform player;
    public GameObject gameOverScreen;
    public GameObject winScreen;
    float timeLastPressed;

    void Start()
    {
        //Initialize variables
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        line.enabled = false;
        player = transform;
        actionUnreleased = false;
        isSwinging = false;
        playerRB = GetComponent<Rigidbody2D>();
        gameOver = false;
        timeLastPressed = 0f;
    }

    void Update()
    {
        //MAINTENANCE

        if (!gameOver) //checks if the game is over
        {
            //Locks player rotation
            LockRotation();

            if (line.enabled)
            {
                //Updates the location of the end of the rope to the player's location if the line is enabled
                UpdateLineEnd();
                //keeps the aim indicator in line with the rope
                alignAim();
            }

            if (joint.distance > 1f && actionUnreleased)
            {
                //Shrinks the rope if the player hasn't released the action button and it isn't the shortest length
                ShrinkRope();
            }

            if (isSwinging)
            {
                //Adds force to make swinging easier
                playerRB.AddForce(transform.up * -swingSpeed * 2);
                playerRB.drag = 0.5f;
            }
            else
            {
                //Sets no drag if not swinging to allow more distance to be covered
                playerRB.drag = 0;
            }
        }




        //MECHANICS

        if (Input.GetKeyUp(KeyCode.Space)) //checks if the action button is released
        {
            if (gameOver) { SceneManager.LoadScene(1); } //restarts the scene if the game is over
            else //if the game isn't over it sets the 2 variables to their correct vaules
            {
                actionUnreleased = false;
                if (joint.enabled) { isSwinging = true; }
            }
        }

        if (!gameOver) //checks if the game isn't over
        {
            if (Input.GetKeyDown(KeyCode.E)) //checks if the action button is pressed down
            {
                if (isSwinging) { BreakRope(); } //breaks the rope if there is one
                else if (Time.time > (timeLastPressed + buttonPressRate)) { ShootRope(); } //shoots a new rope if there isn't one and it's been long enough

                timeLastPressed = Time.time; //stores the time the button was pressed
            }

            if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)) //checks if both left and right are pressed
            {
                GrowRope();
            }
            else if (Input.GetKey(KeyCode.LeftArrow)) //checks if the left button is pressed
            {
                if (isSwinging) { playerRB.AddForce(transform.right * -swingSpeed); } //adds force to the left if swinging
                else { OrbitAround(orbitSpeed); } //spins the aim indicator if not
            }
            else if (Input.GetKey(KeyCode.RightArrow)) //checks if the right button is pressed
            {
                if (isSwinging) { playerRB.AddForce(transform.right * swingSpeed); } //adds force to the right if swinging
                else { OrbitAround(-orbitSpeed); } //spins the aim indicator if not
            }
        }
    }

    void OrbitAround(float ObjectSpeed)
    {
        aimObject.transform.RotateAround(transform.position, Vector3.forward, ObjectSpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("KillZone"))
        {
            //Debug.Log("regigster collosion");
            gameOverScreen.SetActive(true);
            gameOver = true;
        }

        if (collision.gameObject.tag.Equals("WinZone"))
        {
            //Debug.Log("regigster collosion");
            winScreen.SetActive(true);
            gameOver = true;

        }
    }

    void LockRotation()
    {
        //player.eulerAngles = new Vector3(player.eulerAngles.x, player.eulerAngles.y, player.eulerAngles.z);
        //example of unlocking rotation axes

        player.eulerAngles = new Vector3(fixedRotation, fixedRotation, fixedRotation);
        //locks all player rotation axes
    }

    void BreakRope()
    {
        joint.enabled = false;
        line.enabled = false;
        isSwinging = false;
    }

    void ShootRope()
    {
        actionUnreleased = true;

        targetPos = aimObject.transform.position;
        targetPos.z = 0;

        hit = Physics2D.Raycast(transform.position, targetPos - transform.position, maxDistance, layerMask);

        if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            joint.enabled = true;

            Vector2 connectPoint = hit.point - new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y);
            connectPoint.x = connectPoint.x / hit.collider.transform.localScale.x;
            connectPoint.y = connectPoint.y / hit.collider.transform.localScale.y;

            joint.connectedAnchor = connectPoint;
            joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
            joint.distance = Vector2.Distance(transform.position, hit.point);

            line.enabled = true;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, hit.point);
        }
    }

    void UpdateLineEnd()
    {
        line.SetPosition(0, transform.position - line.transform.position);
    }

    void ShrinkRope()
    {
        joint.distance -= step;
    }

    void GrowRope()
    {
        joint.distance += step;
    }

    void alignAim()
    {
        float correctedHitpointX = line.GetPosition(1).x - transform.position.x;
        float correctedHitpointY = line.GetPosition(1).y - transform.position.y;
        //Debug.Log(correctedHitpointX);
        //Debug.Log(correctedHitpointY);

        float calculatedPositionX = 0f;
        float calculatedPositionY = 0f;

        if (correctedHitpointX != 0 && correctedHitpointY != 0)
        {
            if (correctedHitpointX > 0)
            {
                if (correctedHitpointY > 0)
                {
                    calculatedPositionX = 1.273f * Mathf.Sqrt((correctedHitpointY * correctedHitpointY) / ((correctedHitpointX * correctedHitpointX) + (correctedHitpointY * correctedHitpointY)));
                    calculatedPositionY = 1.273f * Mathf.Sqrt((correctedHitpointX * correctedHitpointX) / ((correctedHitpointX * correctedHitpointX) + (correctedHitpointY * correctedHitpointY)));
                }
                else if (correctedHitpointY < 0)
                {
                    calculatedPositionX = -1.273f * Mathf.Sqrt((correctedHitpointY * correctedHitpointY) / ((correctedHitpointX * correctedHitpointX) + (correctedHitpointY * correctedHitpointY)));
                    calculatedPositionY = 1.273f * Mathf.Sqrt((correctedHitpointX * correctedHitpointX) / ((correctedHitpointX * correctedHitpointX) + (correctedHitpointY * correctedHitpointY)));
                }
            }
            else if (correctedHitpointX < 0)
            {
                if (correctedHitpointY > 0)
                {
                    calculatedPositionX = 1.273f * Mathf.Sqrt((correctedHitpointY * correctedHitpointY) / ((correctedHitpointX * correctedHitpointX) + (correctedHitpointY * correctedHitpointY)));
                    calculatedPositionY = -1.273f * Mathf.Sqrt((correctedHitpointX * correctedHitpointX) / ((correctedHitpointX * correctedHitpointX) + (correctedHitpointY * correctedHitpointY)));
                }
                else if (correctedHitpointY < 0)
                {
                    calculatedPositionX = -1.273f * Mathf.Sqrt((correctedHitpointY * correctedHitpointY) / ((correctedHitpointX * correctedHitpointX) + (correctedHitpointY * correctedHitpointY)));
                    calculatedPositionY = -1.273f * Mathf.Sqrt((correctedHitpointX * correctedHitpointX) / ((correctedHitpointX * correctedHitpointX) + (correctedHitpointY * correctedHitpointY)));
                }
            }

            //aimObject.transform.position = new Vector3(calculatedPositionX + transform.position.x, calculatedPositionY + transform.position.y, 0f);
            aimObject.transform.position = new Vector3(calculatedPositionY + transform.position.x, calculatedPositionX + transform.position.y, 0f); //I had to flip the x and y for some reason??????
        }
    }
}
