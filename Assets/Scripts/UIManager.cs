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

    [SerializeField] GameObject ClearBad;
    [SerializeField] GameObject ClearNormal;
    [SerializeField] GameObject ClearGood;
    [SerializeField] GameObject TitleButton;
    [SerializeField] GameObject QuitButton;


    void Start()
    {
        Time.timeScale = 1; //リスタート時にタイムスケールを1に戻す

        rb = target.GetComponent<Rigidbody>();
        UiCollar_Red.SetActive(true);
        UiCollar_Blue.SetActive(false);
        UiCollar_Yellow.SetActive(false);
        //クリアUI用（非表示）
        ClearBad.SetActive(false);
        ClearNormal.SetActive(false);
        ClearGood.SetActive(false);
        TitleButton.SetActive(false);
        QuitButton.SetActive(false);
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

    public void ShwoClearUI()
    {
        Time.timeScale = 0;

        if(countUpTimer < 10f)
        {
            ClearGood.SetActive(true);
            TitleButton.SetActive(true);
            QuitButton.SetActive(true);
        }
        else if(countUpTimer < 20f)
        {
            ClearNormal.SetActive(true);
            TitleButton.SetActive(true);
            QuitButton.SetActive(true);
        }
        else 
        { 
            ClearBad.SetActive(true);
            TitleButton.SetActive(true);
            QuitButton.SetActive(true);
        }
    }
}
