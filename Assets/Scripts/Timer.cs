using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
public Text timerText;
private float startTime;

void Start()
{
    startTime = Time.time;
}

void Update()
{
    float t = Time.time - startTime;
    string seconds = t.ToString("f2");
    timerText.text = seconds;
}
}