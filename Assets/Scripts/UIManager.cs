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
    [SerializeField] GameObject UiCollar_Red;
    [SerializeField] GameObject UiCollar_Blue;
    [SerializeField] GameObject UiCollar_Yellow;



    void Start()
    {
        rb = target.GetComponent<Rigidbody>();
        UiCollar_Red.SetActive(true);
        UiCollar_Blue.SetActive(false);
        UiCollar_Yellow.SetActive(false);
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
        velocityText.text = velocityX.ToString("F0") + "km/s";
    }


    public void UIChangeRed()
    {
        UiCollar_Red.SetActive(true);
        UiCollar_Blue.SetActive(false);
        UiCollar_Yellow.SetActive(false);
    }
   public void UIChangeBlue()
    {
        UiCollar_Red.SetActive(false);
        UiCollar_Blue.SetActive(true);
        UiCollar_Yellow.SetActive(false);
    }

   public void UIChangeYellow()
    {
        UiCollar_Red.SetActive(false);
        UiCollar_Blue.SetActive(false);
        UiCollar_Yellow.SetActive(true);
    }

}
