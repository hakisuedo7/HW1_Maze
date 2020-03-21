using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool IsStart = false;
    private int timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TimeStart()
    {
        timer = 0;
        IsStart = true;
        StartCoroutine(CoroutineTimer());
    }
    public void TimeStop()
    {
        StopAllCoroutines();
        IsStart = false;
    }

    public int GetTime()
    {
        return timer;
    }

    IEnumerator CoroutineTimer()
    {
        while (IsStart)
        {
            yield return new WaitForSeconds(1f);
            timer++;
        }
    }
}
