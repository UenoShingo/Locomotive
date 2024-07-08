using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField]
    private float delayBeforeClearScene = 2.0f; // クリア画面に移行する前の遅延時間（秒）

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("ゴールに到達しました！");

            // 遅延してクリア画面に移行するコルーチンを開始
            StartCoroutine(TransitionToClearScene());
        }
    }

    IEnumerator TransitionToClearScene()
    {
        // 指定された遅延時間だけ待機する
        yield return new WaitForSeconds(delayBeforeClearScene);

        // ClearSceneをロードする
        SceneManager.LoadScene("ClearScene");
    }
}
