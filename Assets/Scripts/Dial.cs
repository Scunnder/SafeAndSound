using UnityEngine;
using System.Collections;


//This script will handle the movement of the dial

public class Dial : MonoBehaviour {
    
    public int AtDegree = 0;
    public bool Active = true;

	void Start ()
    {
	
	}
    
    void Update()
    {
        if(Input.touchCount > 0)
        {
            IsTouchedFinger();
        }
        else if(Input.GetMouseButton(0))
        {
            IsTouchedMouse();
        }
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
    
    public void IsTouchedMouse()
    {

        Vector2 mousepoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D collider = Physics2D.OverlapPoint(mousepoint);

        if (collider != null && collider.gameObject.tag == "Dial")
        {
            //get distance and heading
            Vector2 heading = mousepoint - (Vector2)transform.position;
            float distance = heading.magnitude;
            if(distance > 0.2)
            {
                //normalized
                Vector2 direction = heading / distance;

                float degrees = Mathf.Atan2(direction.y, direction.x);
                transform.eulerAngles = new Vector3(0, 0, degrees);
            }





            //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; //I dont get this
            //transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }

}
