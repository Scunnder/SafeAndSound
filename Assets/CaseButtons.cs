using UnityEngine;
using System.Collections;

public class CaseButtons : MonoBehaviour {

    public GameObject Deactivate;
    public GameObject Activate;
    public Collider2D HitThis;
    //public ButtonsDone BD;
    
    public void Update()
    {
        if(Input.touchCount > 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.IsTouching(HitThis))
                {
                    Deactivate.SetActive(false);
                    Activate.SetActive(true);
                }
                Debug.Log("Touched it");
            }
        }
    }

    void OnMouseDown()
    {
        Deactivate.SetActive(false);
        Activate.SetActive(true);
       // BD.Increment();
    }
}
