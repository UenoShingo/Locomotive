using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI countUpText;
    private float countUpTimer = 0f; // カウントの初期値

    [SerializeField] GameObject target;
    [SerializeField] float velocityX;
    [SerializeField] Text velocityText;
    private Rigidbody rb;

    void Start()
    {
        rb = target.GetComponent<Rigidbody>();
    }


    void Update()
    {

        // カウントダウンの計算と表示
        countUpTimer += Time.deltaTime;
        if (countUpTimer > 200f)
        {
            countUpTimer = 0f;
            // カウントダウンが終了した場合の処理を追加する（ゲームオーバーなど）
        }
        countUpText.text = Mathf.FloorToInt(countUpTimer).ToString() + "s"; // "60s" の表記に変更

        velocityX = rb.velocity.x;
        velocityText.text = velocityX.ToString("F2") + "km/s";
    }

}
