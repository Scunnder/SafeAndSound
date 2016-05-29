using UnityEngine;
using System.Collections;

public class RevealAudio : MonoBehaviour {

    public GameObject[] Objects = new GameObject[3];
    public AudioSource[] Audio = new AudioSource[3];
	
	// Update is called once per frame
    private int ObjectsActive = 0;
	void Update ()
    {
        int count = CountActive();
        if (count > ObjectsActive)
        {
            Audio[count-1].Play();
            ObjectsActive = count;
        }
    }

    public int CountActive()
    {
        int count = 0;
        foreach(GameObject go in Objects)
        {
            if(go.active)
            {
                count++;
            }
        }
        return count;
    }
}
