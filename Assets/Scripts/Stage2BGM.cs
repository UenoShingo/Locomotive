using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2BGM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Stage2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
