using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SafeLock : MonoBehaviour
{

    public int MillisecondsToUnlock = 100;
    private int Sweetspot = 0;
    public int Highest = 45;
    public Dial SafeDial;
    private int TickSize = 360;
    private int ActiveLock = 0;
    public bool Active = true;
    public AudioSource[] sources;
    public GameObject[] Locks;

    public void Start()
    {
        Input.gyro.enabled = false;
        if (TickSize == 0)
        {
            TickSize = 360;
        }
        NewSweetspot(true);
        TickSize = 8;
    }

    public int WasAt = 0;


    public float count = 0;
    public bool Once = true;
    public bool Opened = false;
    public bool NoTouch = false;
    public float DoorCount = 0;
    public void Update()
    {
        if (Active)
        {
            int at = Mathf.RoundToInt(SafeDial.AtDegree / TickSize);
            CheckDistance(at);

            if (WasAt != at)
            {
                WasAt = at;
                if (at == Sweetspot)
                {
                    Handheld.Vibrate();
                    Once = true;
                }
                else
                {
                    Debug.Log("Tick");
                    sources[1].Play();
                }
            }
            else
            {
                if(Once)
                {
                    Once = false;
                    if (at == Sweetspot)
                    {
                        Debug.Log("SWEETSPOT");
                        sources[0].Play();
                    }
                }
            }
            WasAt = at;

            //check sweetspot
            if (at == Sweetspot)
            {
                count += 12f * Time.deltaTime;
            }
            else
            {
                count = 0;
            }

            if (count > MillisecondsToUnlock)
            {
                LockDone();
                CheckWin();
            }
        }
        else if(!Opened)
        {
            if (Input.touchCount > 0 && NoTouch)
            {
                ClickVFX.transform.position = Input.GetTouch(0).position;
                ClickVFX.SetActive(true);
                Opened = true;
                SafeOpened();
            }
            else if(DoorCount > 1)
            {
                NoTouch = true;
            }
            else if(Input.touchCount == 0)
            {
                DoorCount += 1f * Time.deltaTime;
            }
        }
    }

    public void CheckWin()
    {
        if(ActiveLock > 2)
        {
            SafeUnlocked();
        }
    }

    public void LockDone()
    {
        Debug.Log("KLANK");
        Locks[ActiveLock].GetComponent<GUILock>().SetLocked(false);
        sources[2].Play();
        NewSweetspot();
    }

    public void NewSweetspot(bool first = false)
    {
        int newsweet = Random.Range(0, Highest);
        while (newsweet == Sweetspot)
        {
            newsweet = Random.Range(0, Highest);
        }
        Sweetspot = newsweet;

        if (!first)
        {
            ActiveLock++;
        }
        Debug.Log("Sweetspot: " + Sweetspot);
    }


    public float Intensity = 1;
    public void CheckDistance(int at)
    {

        int a = Sweetspot - at;
        if (a > 45)
        {
            a -= 45;
        }

        if (a < 0)
        {
            a += 45;
        }

        int b = at - Sweetspot;
        if (b > 45)
        {
            b -= 45;
        }

        if (b < 0)
        {
            b += 45;
        }

        int useThis = 0;
        if (a > b)
        {
            useThis = b;
        }
        else
        {
            useThis = a;
        }

        if (useThis <= 3)
        {
            Intensity = 1.2f;
        }
        if (useThis <= 2)
        {
            Intensity = 1.4f;
        }
        if (useThis <= 1)
        {
            Intensity = 1.6f;
        }
        if (useThis == 0)
        {
            Intensity = 1.8f;
        }
    }

    public GameObject UnlockVFX;
    public void SafeUnlocked()
    {
        Active = false;
        SafeDial.Active = false;
        UnlockVFX.SetActive(true);
    }

    public GameObject OpenVFX;
    public GameObject ClickVFX;
    public GameObject Door;
    public void SafeOpened()
    {
        Door.SetActive(false);
        UnlockVFX.SetActive(false);
        OpenVFX.SetActive(true);
        //start next scene here
    }
}
