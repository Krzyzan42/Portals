using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        int sleepTime = Mathf.FloorToInt(16 - (Time.deltaTime - 0.01666f)* 1000f);
        sleepTime = Mathf.Clamp(sleepTime, 0, 16);
        //Debug.Log("Deltatime: " + Time.deltaTime.ToString());
        //Debug.Log("Sleeping for " + sleepTime.ToString() + " miliseconds");
        //System.Threading.Thread.Sleep(sleepTime);
        System.Threading.Thread.Sleep(15);
    }
}
