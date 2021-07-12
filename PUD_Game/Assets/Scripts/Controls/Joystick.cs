using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour
{
    public Transform player; //pud
    public float speed = 2.0f;
    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;

    public Transform circle; //touch
    public Transform outerCircle; //threshold

    public GameObject joystickZone; //panel

    public List<touchLocation> touches = new List<touchLocation>();

    public int joystickID = 99;

    private bool grappled = false;

    void Start()
    {
        
    }

    void Update()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);
            if (t.phase == TouchPhase.Began)
            {
                touches.Add(new touchLocation(t.fingerId, joystickZone));

                if (joystickCheck(i))
                {
                    pointA = outerCircle.transform.position;

                    circle.transform.position = pointA;

                    joystickID = i;
                }
            }
            else if (t.phase == TouchPhase.Ended)
            {
                if(i == joystickID)
                {
                    touchStart = false;

                    circle.transform.position = outerCircle.transform.position;
                    joystickID = 99;
                }

                touchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchId == t.fingerId);
                touches.RemoveAt(touches.IndexOf(thisTouch));
            }
            else if (t.phase == TouchPhase.Moved)
            {
                touchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchId == t.fingerId);

                if (i == joystickID)
                {
                    touchStart = true;
                    pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(i).position.x, Input.GetTouch(i).position.y, Camera.main.transform.position.z));
                }
            }
            ++i;
        }
    }

    private void FixedUpdate()
    {
        if (touchStart) //moves the circle part of the joystick and calls the move character function
        {
            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
            moveCharacter(direction);

            circle.transform.position = new Vector2(pointA.x + direction.x, pointA.y + direction.y);
        }
    }

    void moveCharacter(Vector2 direction) //...
    {
        player.Translate(direction * speed * Time.deltaTime);
    }

    bool joystickCheck(int iD) //use a raycast at the given touch id to detect if it's in the joystick zone
    {
        bool output = false;

        RaycastHit2D hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(iD).position);
        if(hit = Physics2D.Raycast(ray.origin, new Vector2(0, 0)))
        {
            if(hit.collider.name == joystickZone.name)
            {
                output = true;
            }
        }

        return output;
    }

    Vector2 getTouchPosition(Vector2 touchPosition) //idk if we need this
    {
        return GetComponent<Camera>().ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z));
    }
}
