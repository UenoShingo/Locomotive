using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    // ボタンがクリックされたときに呼ばれるメソッド
    public void GoStagebutton()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void GoStagebutton2()
    {
        SceneManager.LoadScene("Stage2");
    }

    public void GoTitlebutton()
    {
        
        SceneManager.LoadScene("Title");
        
    }

    public void GoHelpbutton()
    {

        SceneManager.LoadScene("Help");

    }

    public void QuitButton()
    {
        Debug.Log("Quit button clicked."); // オプションのデバッグログ
        Application.Quit(); // アプリケーションを終了
    }

}