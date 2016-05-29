using UnityEngine;
using System.Collections;

public class ButtonsDone : MonoBehaviour {

    public int Count = 0;
    public bool NextSceneOnClick = false;

	void Update ()
    {
	    if(Count > 2)
        {
            NextSceneOnClick = true;
        }
	}

    public void Increment()
    {
        Count++;
    }

    public void OnMouseDown()
    {
        if(NextSceneOnClick)
        {
            Application.LoadLevel(0);
        }
    }
}
