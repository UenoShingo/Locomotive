using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class Pause : MonoBehaviour
{
    bool PauseFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!PauseFlag)
            {
                PauseFlag = true;
                Time.timeScale = 0;
            }
            else
            {
                PauseFlag = false;
                Time.timeScale = 1f;
            }
        }
    }
}
