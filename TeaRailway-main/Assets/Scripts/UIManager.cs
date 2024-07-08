using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI accelerationText;
    public TextMeshProUGUI countdownText;
    private float countdownTimer = 60f; // カウントダウンの初期値

    void Update()
    {
        // 加速度の計算と表示
        float acceleration = CalculateAcceleration();
        accelerationText.text = acceleration.ToString("F0") + " Km/s"; // Km/s形式で表示、小数点以下は表示しない

        // カウントダウンの計算と表示
        countdownTimer -= Time.deltaTime;
        if (countdownTimer <= 0f)
        {
            countdownTimer = 0f;
            // カウントダウンが終了した場合の処理を追加する（ゲームオーバーなど）
        }
        countdownText.text = Mathf.FloorToInt(countdownTimer).ToString() + "s"; // "60s" の表記に変更
    }

    float CalculateAcceleration()
    {
        // 実際の加速度計算処理を行う
        // ここでは仮の値を返す
        return 10.5f; // 仮の加速度値
    }
}
