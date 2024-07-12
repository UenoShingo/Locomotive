using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Player;

public class Pause : MonoBehaviour
{
    bool PauseFlag = false;
    [SerializeField] GameObject PauseUI;
    // Start is called before the first frame update
    void Start()
    {
        PauseUI.SetActive(false);
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
                PauseUI.SetActive(true);
            }
            else
            {
                PauseFlag = false;
                Time.timeScale = 1f;
                PauseUI.SetActive(false);
            }
        }
    }

    public void RestartButton()
    {
        Debug.Log("リスタート");
        SceneManager.LoadScene("Stage1");
    }


}
