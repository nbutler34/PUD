using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour
{
    //General
    public Transform player; //pud
    Vector2 lastLocation;

    //Joystick & Touch Controls
    public float speed = 2.0f;
    private bool touchStart = false; //whether or not the joystick is being used
    private Vector2 pointA;
    private Vector2 pointB;
    public Transform circle; //touch
    public Transform outerCircle; //threshold
    public GameObject joystickZone; //panel
    public List<touchLocation> touches = new List<touchLocation>(); //finger ID index
    public int joystickID = 99;

    //Grappling
    private bool grappled = false;
    DistanceJoint2D joint;
    Vector3 targetPos;
    RaycastHit2D hit;
    public float maxDistance = 10f;
    public LayerMask mask;
    public float step = 0.01f;
    public LineRenderer line;
    public bool actionUnreleased;
    Rigidbody2D playerRB;
    public float swingSpeed = 5f;

    //Effects
    public ParticleSystem hitParticles;
    ParticleSystem deleteObject;
    

    void Start()
    {
        joint = player.GetComponent<DistanceJoint2D>(); //sets joint to Pud's distance joint
        joint.enabled = false;
        grappled = false;
        actionUnreleased = false;
        line.enabled = false;
        playerRB = player.GetComponent<Rigidbody2D>();
        lastLocation = player.position;
        
    }

    void Update()
    {
        
        if (player.position.x > lastLocation.x)
        {
            player.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if(player.position.x < lastLocation.x)
        {
            player.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (line.enabled)
        {
            UpdateLineEnd();
        }
        if (grappled)
        {
            //playerRB.AddForce(player.transform.up * -swingSpeed * 2);
            playerRB.drag = 0.5f;
        }
        else
        {
            playerRB.drag = 0;
        }
        if (actionUnreleased)
        {
            ShrinkRope(false);
        }



        int i = 0;
        while (i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);

            if (t.phase == TouchPhase.Began)
            {
                touches.Add(new touchLocation(t.fingerId, joystickZone)); //adds the touch to the ID index

                if (joystickCheck(i)) //if inside the joystick zone,
                {
                    pointA = outerCircle.transform.position; //sets a base position for the joystick

                    circle.transform.position = pointA; //sets the circle to the middle of the joystick

                    joystickID = i; //set this finger ID to control the joystick
                }
                else if (!grappled)
                {
                    targetPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(i).position.x, Input.GetTouch(i).position.y, 0)); //stores the position of the tap in world space
                    ShootRope();
                }
                else if (grappled)
                {
                    BreakRope();
                }
            }
            else if (t.phase == TouchPhase.Ended)
            {
                if (i == joystickID) //if this finger is controlling the joystick, all variables are set to their defaults
                {
                    touchStart = false;

                    circle.transform.position = outerCircle.transform.position;
                    joystickID = 99;

                }
                else if (grappled)
                {
                    actionUnreleased = false;
                }

                touchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchId == t.fingerId); //finds the finger in the ID index
                touches.RemoveAt(touches.IndexOf(thisTouch)); //and removes it
            }
            else if (t.phase == TouchPhase.Moved)
            {
                touchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchId == t.fingerId); //find the finger in the ID index

                if (i == joystickID) //if this finger is controlling the joystick
                {
                    touchStart = true;
                    pointA = outerCircle.transform.position;
                    pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(i).position.x, Input.GetTouch(i).position.y, Camera.main.transform.position.z)); //sets the postion of the joystick
                }
                else if (grappled)
                {
                    
                }
            }
            else if (t.phase == TouchPhase.Stationary)
            {
                if(i == joystickID) //if this finger is controlling the joystick
                {
                    pointA = outerCircle.transform.position;
                    pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(i).position.x, Input.GetTouch(i).position.y, Camera.main.transform.position.z)); //sets the postion of the joystick
                }
            }
            ++i;
        }

        lastLocation = player.position;
    }

    private void FixedUpdate()
    {
        if (touchStart) //if there is a finger on the joystick, it moves the player
        {
            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
            //moveCharacter(direction);

            if(grappled)
            {
                if(direction.x > 0.4)
                {
                    playerRB.AddForce(player.right * swingSpeed);
                }
                else if(direction.x < -0.4)
                {
                    playerRB.AddForce(player.right * -swingSpeed);
                }

                if (direction.y > 0.6)
                {
                    ShrinkRope(true);
                }
                else if (direction.y < -0.6)
                {
                    GrowRope(true);
                }
            }

            circle.transform.position = new Vector2(pointA.x + direction.x, pointA.y + direction.y);
        }
    }

    bool joystickCheck(int iD) //use a raycast at the given touch id to detect if it's in the joystick zone
    {
        bool output = false;

        RaycastHit2D hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(iD).position);
        if (hit = Physics2D.Raycast(ray.origin, new Vector2(0, 0)))
        {
            if (hit.collider.name == joystickZone.name)
            {
                output = true;
            }
        }

        return output;
    }

    void ShootRope()
    {
        actionUnreleased = true;

        hit = Physics2D.Raycast(player.position, targetPos - player.position, maxDistance, mask); //stores the hit object

        if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>() != null) //if the raycast hits an object with a rigidbody2d,
        {
            joint.enabled = true;

            Vector2 connectPoint = hit.point - new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y); //gets the corrected anchor point

            joint.connectedAnchor = connectPoint; //sets anchor point to the corrected value
            joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>(); //connects the hit rigibody2d
            joint.distance = Vector2.Distance(player.position, hit.point); //sets the distance between player and hit point

            line.enabled = true;
            line.SetPosition(0, player.position);
            line.SetPosition(1, hit.point);

            grappled = true;

            deleteObject = Instantiate(hitParticles, new Vector3(hit.point.x, hit.point.y, 0), new Quaternion(0, 0, 180, 0));
            //deleteObject.transform.position = connectPoint;

            Debug.Log(hit.point + ", " + deleteObject.transform.position);
            //Destroy(deleteObject, 1f);
        }
    }

    void BreakRope()
    {
        joint.enabled = false;
        line.enabled = false;
        grappled = false;
    }

    void ShrinkRope(bool joystick)
    {
        if (joystick)
        {
            joint.distance -= 4 * step;
        }
        else
        {
            joint.distance -= 2 * step;
        }
        
    }

    void GrowRope(bool joystick)
    {
        if (joystick)
        {
            joint.distance += 4 * step;
        }
        else
        {
            joint.distance += 2 * step;
        }
    }

    void UpdateLineEnd()
    {
        line.SetPosition(0, player.position - line.transform.position);
    }

    Vector2 getTouchPosition(Vector2 touchPosition) //idk if we need this
    {
        return GetComponent<Camera>().ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z));
    }

    IEnumerator DeleteClone(GameObject prefab, float time)
    {
        yield return new WaitForSeconds(time);
    }
}
