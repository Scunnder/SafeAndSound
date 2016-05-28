using UnityEngine;
using System.Collections;


//This script will handle the movement of the dial

public class Dial : MonoBehaviour {
    
    public float StartDegree = 0;
    public float AtDegree = 0;
    public bool Active = true;

	void Start ()
    {
	
	}
    
    void Update()
    {
        if(Active)
        {         
            if(Input.touchCount > 0)
            {
                IsTouchedFinger();
            }
            else if(Input.GetMouseButton(0))
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


    public void IsTouchedFinger()
    {
        Vector2 mp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        Collider2D c = Physics2D.OverlapPoint(mp);

        if (c != null && c.gameObject.tag == "Dial")
        {
            Vector2 dir = mp - (Vector2)transform.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            if (angle < transform.eulerAngles.z)
            {
                transform.eulerAngles = new Vector3(0, 0, angle);
            }
        }
    }

    private bool DoOne = true;
    public void MouseUpdate()
    {
        Vector2 mousepoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1000f));
        Collider2D collider = Physics2D.OverlapPoint(mousepoint);

        if (collider != null && collider.gameObject.tag == "Dial")
        {
            
            Vector2 heading = mousepoint - (Vector2)transform.position;
            float distance = heading.magnitude;
            if(distance > 0.5)
            {
                Vector2 direction = heading / distance;

                float degrees = -CustomAngle(direction); //Mathf.Atan2(direction.y, direction.x) * 180;// / Mathf.PI;
                
                degrees = FixDegrees(degrees);

                if (DoOne)
                {
                    InitialPress(degrees);
                    DoOne = false;
                }
                UpdatePress(degrees);
            }
        }
        else
        {
            EndPress();
        }
    }

    public void InitialPress(float degrees)
    {
        StartDegree = degrees + 180;
    }

    public void UpdatePress(float degrees)
    {
        AtDegree = degrees;
    }

    public void EndPress()
    {
        DoOne = true;
    }

    public float FixDegrees(float degrees)
    {
        if (degrees > 360)
        {
            degrees -= 360;
        }
        else if (degrees < -360)
        {
            degrees += 360;
        }
        return degrees;
    } 

    public static float CustomAngle(Vector2 p_vector2)
    {
        if (p_vector2.x < 0)
        {
            return 360 - (Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg* -1);
        }
        else
        {
            return Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg;
        }
    }

}
