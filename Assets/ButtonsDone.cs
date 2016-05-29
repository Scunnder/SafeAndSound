using UnityEngine;
using System.Collections;

public class ButtonsDone : MonoBehaviour {

    private int Count = 0;
    private bool NextSceneOnClick = false;
    public BoxCollider2D Collider;

	void Update ()
    {
	    if(Count > 2)
        {
            NextSceneOnClick = true;
            Collider.enabled = true;
        }

        if(NextSceneOnClick)
        {
            if (Input.touchCount > 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
                if (hit.collider != null)
                {
                    if (hit.collider.IsTouching(Collider))
                    {
                        Application.LoadLevel(0);
                    }
                }
            }
        }
	}

    public void Increment()
    {
        Count++;
    }
}
