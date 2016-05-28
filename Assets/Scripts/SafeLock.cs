using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SafeLock : MonoBehaviour
{

    public int MillisecondsToUnlock = 100;
    private int Sweetspot = 0;
    public int Highest = 99;
    public Dial SafeDial;
    private int TickSize = 360;
    private int ActiveLock = 0;
    public bool Active = true;
    public AudioSource[] sources;

    public void Start()
    {
        if (TickSize == 0)
        {
            TickSize = 360;
        }
        NewSweetspot(true);
        TickSize = 8;
    }

    public int WasAt = 0;


    public float count = 0;
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
                    sources[0].Play();
                }
                else
                {
                    sources[1].pitch = Intensity;
                    sources[1].Play();
                }
            }
            WasAt = at;

            //vibration


            //check sweetspot
            if (at == Sweetspot)
            {
                count += 1f * Time.deltaTime;
            }
            else
            {
                count = 0;
            }

            if (count > MillisecondsToUnlock)
            {
                LockDone();
            }
        }
    }

    public void LockDone()
    {
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

        if (ActiveLock > 2)
        {
            SafeUnlocked();
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
        //result

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

    public void SafeUnlocked()
    {
        Active = false;
    }
}
