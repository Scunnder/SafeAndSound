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
    public AudioSource[] sources;

    public void Start ()
    {
        if(TickSize == 0)
        {
            TickSize = 360;
        }
        NewSweetspot(true);
        TickSize = 5;
    }

    public int WasAt = 0;


    public float count = 0;
    public void Update ()
    {
        if(Active)
        {
          // float Point1 = (SafeDial.AtDegree / (float)Highest) * 360f;
          // float Point2 = ((float)Sweetspot / (float)Highest) * 360f;
          //
          // float distance = Point1 - Point2;
          // if (distance > 180f)
          // {
          //     distance -= 360;
          // }
          // else if (distance < -180)
          // {
          //     distance += 360;
          // }
          // Debug.Log("Distance: " + distance);
          // distance = Mathf.Abs(distance);
          
            int at = Mathf.RoundToInt(SafeDial.AtDegree / TickSize);

            if (WasAt != at)
            {
                WasAt = at;
                if( at == Sweetspot )
                {
                    Debug.Log("Sweetspot!");
                    sources[0].Play();
                }
                else
                {
                    sources[1].Play();
                }
            }
            WasAt = at;

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
        }
        Debug.Log("Sweetspot: " + Sweetspot);
    }


    public int Intensity = 0;
    public void CheckDistance(int at)
    {

        int a = Sweetspot - at;
        if( a > 72)
        {
            a -= 72;
        }
        
        if (a < 0)
        {
            a += 72;
        }

        int b = at - Sweetspot;
        if (b > 72)
        {
            b -= 72;
        }

        if (b < 0)
        {
            b += 72;
        }

        int useThis = 0;
        if(a > b)
        {
            useThis = b;
        }
        else
        {
            useThis = a;
        }
        Debug.Log("useThis " + useThis);









        //fix results
        int result = Mathf.Abs(at - Sweetspot);
        
        if(result > 36 || result < 0)
        {
            result = (72 - at) - Sweetspot;
        }
        
        result = Mathf.Abs(result);
        //result

        if(a > 30)
        {
            Intensity = 5;
        }
        if(a <= 20)
        {
            Intensity = 4;
        }
        if (a <= 12)
        {
            Intensity = 3;
        }
        if (a <= 8)
        {
            Intensity = 2;
        }
        if (a <= 4)
        {
            Intensity = 1;
        }
        if (a == 0)
        {
            Intensity = 0;
        }
    }

    public float VibrateCount = 0;
    public void VibratePhone()
    {
        if(VibrateCount > Intensity && Intensity != 5)
        {
            //Handheld.Vibrate();
            VibrateCount = 0f;
        }
        
        VibrateCount += 4f * Time.deltaTime;
    }

    public void SafeUnlocked()
    {
        Active = false;
    }
}
