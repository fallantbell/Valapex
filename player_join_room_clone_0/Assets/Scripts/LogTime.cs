using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogTime : MonoBehaviour
{
    bool state = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(logTime());
        StartCoroutine(waitASec());
    }

    // Update is called once per frame
    
    private IEnumerator logTime()
    {
        int step = 0;
        while (step < 1000)
        {
            print(Time.frameCount);
            step++;
            yield return new WaitUntil(() => state);
        }
    }
    private IEnumerator waitASec()
    {
        while (true)
        {
            state = !state;
            yield return new WaitForSeconds(1f);
        }
    }
}
