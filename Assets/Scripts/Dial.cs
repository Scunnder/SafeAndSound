using UnityEngine;
using System.Collections;


//This script will handle the movement of the dial

public class Dial : MonoBehaviour
{
    public float PressPoint = 0f;
    public float StartDegree = 0f;
    public float AtDegree = 0;
    public bool Active = true;

    void Start()
    {

    }

    void Update()
    {
        if (Active)
        {
            if (Input.touchCount > 0)
            {
                IsTouchedFinger();
            }
            else if (Input.GetMouseButton(0))
            {
                MouseUpdate();
            }
            else
            {
                EndPress();
            }
        }
        UpdateRotation();
    }

    public void UpdateRotation()
    {

        transform.eulerAngles = new Vector3(0, 0, AtDegree);
    }


    private bool DoOneFinger = true;
    public float StartPoint = 0;
    public void IsTouchedFinger()
    {

        Vector2 fingerpoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 1000f));
        Collider2D collider = Physics2D.OverlapPoint(fingerpoint);

        if (collider != null && collider.gameObject.tag == "Dial")
        {

            Vector2 heading = fingerpoint - (Vector2)transform.position;
            float distance = heading.magnitude;
            if (distance > 0.5)
            {
                Vector2 direction = heading / distance;

                float degrees = -CustomAngle(direction); //Mathf.Atan2(direction.y, direction.x) * 180;// / Mathf.PI;

                if (DoOneFinger)
                {
                    StartDegree = AtDegree;
                    PressPoint = degrees;
                    DoOneFinger = false;
                }
                UpdatePress(degrees);
            }
        }
        else
        {
            EndPress();
        }
    }



    private bool DoOneMouse = true;
    public void MouseUpdate()
    {
        Vector2 mousepoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1000f));
        Collider2D collider = Physics2D.OverlapPoint(mousepoint);

        if (collider != null && collider.gameObject.tag == "Dial")
        {

            Vector2 heading = mousepoint - (Vector2)transform.position;
            float distance = heading.magnitude;
            if (distance > 0.5)
            {
                Vector2 direction = heading / distance;

                float degrees = -CustomAngle(direction); //Mathf.Atan2(direction.y, direction.x) * 180;// / Mathf.PI;
                

                if (DoOneMouse)
                {
                    StartDegree = AtDegree;
                    PressPoint = degrees;
                    DoOneMouse = false;
                }
                UpdatePress(degrees);
            }
        }
        else
        {
            EndPress();
        }
    }
    
    public void UpdatePress(float degrees)
    {
        AtDegree = FixDegrees((degrees - PressPoint) + StartDegree);
    }

    public void EndPress()
    {
        DoOneFinger = true;
        DoOneMouse = true;
    }

    public float FixDegrees(float degrees)
    {
        while(degrees > 360)
        {
            degrees -= 360;
        }
        while (degrees < 0)
        {
            degrees += 360;
        }
        return degrees;
    }

    public static float CustomAngle(Vector2 p_vector2)
    {
        if (p_vector2.x < 0)
        {
            return 360 - (Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg * -1);
        }
        else
        {
            return Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg;
        }
    }

}
