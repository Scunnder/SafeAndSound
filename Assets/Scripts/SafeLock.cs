using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SafeLock : MonoBehaviour {

    public int MillisecondsToUnlock = 100;
    private int Sweetspot = 0;
    public int Highest = 99;
    public Dial SafeDial;
    private int TickSize = 360;
    private int ActiveLock = 0;
    public bool Active = true;

    public void Start ()
    {
        if(TickSize == 0)
        {
            TickSize = 360;
        }
        NewSweetspot(true);
        TickSize = 360 / Highest;
    }

    public float count = 0;
    public void Update ()
    {
        if(Active)
        {
            //at
            int at = Mathf.RoundToInt(SafeDial.AtDegree) / TickSize;

            //vibration
            CheckDistance(at);
            VibratePhone();


            //check sweetspot
            if (at == Sweetspot)
            {
                count += 1f * Time.deltaTime;
            }
            else
            {
                count = 0;
            }

            if(count > MillisecondsToUnlock)
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
        while(newsweet == Sweetspot)
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
            Debug.Log("DONE");
        }
        Debug.Log("Sweetspot: " + Sweetspot);
    }


    public int Intensity = 0;
    public void CheckDistance(int at)
    {


        int result = Mathf.Abs(at - Sweetspot);

        if(result > 50)
        {
            result = (100 - at) - Sweetspot;
            
        }

        if(result > 30)
        {
            Intensity = 5;
        }
        else if(result <= 30 && result > 18.5) //1
        {
            Intensity = 4;

        }
        else if (result <= 18.5 && result > 8.5)//2
        {
            Intensity = 3;

        }
        else if (result <= 8.5 && result > 1.5)//3
        {
            Intensity = 2;

        }
        else if (result <= 1.5)//5
        {
            Intensity = 1;
        }
        else if (result == 0)//5
        {
            Intensity = 0;
        }
    }

    public float VibrateCount = 0;
    public void VibratePhone()
    {
        if(Intensity * 6 < VibrateCount)
        {
            Handheld.Vibrate();
        }
        VibrateCount += 1f * Time.deltaTime;
    }

    public void SafeUnlocked()
    {
        Active = false;
    }
}
