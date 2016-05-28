using UnityEngine;
using System.Collections;

public class GUILock : MonoBehaviour {

    public int Lock = 0;
    public GameObject Locked;
    public GameObject Unlocked;

    public void Start()
    {
        SetLocked(true);
    }
	
	public void SetLocked(bool set)
    {
        Locked.SetActive(set);
        Unlocked.SetActive(!set);
    }
}
