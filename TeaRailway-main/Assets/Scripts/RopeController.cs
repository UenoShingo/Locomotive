using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RopeController : MonoBehaviour
{
    public Player player;

    RectTransform rectTransform;
    Vector3 startPosition;
    Vector3 lastMousePosition;

    [SerializeField]
    private GameObject smokeObject;
    private SmokeController smokeController;

    private bool isSmoking = false;
    private bool isWhistleBlowing = false;  // 汽笛が鳴っているかどうかのフラグ
    private float smokingDecreaseRate = 0.1f;  // 加速度の減少率
    private float smokingDeltaSpeed = 0f;  // 減少した加速度の合計

    void Start()
    {
        player = FindObjectOfType<Player>();

        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition3D;
        lastMousePosition = Input.mousePosition;

        if (smokeObject != null)
        {
            smokeController = smokeObject.GetComponent<SmokeController>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = rectTransform.anchoredPosition3D;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            rectTransform.anchoredPosition3D += new Vector3(0, delta.y, 0);
            lastMousePosition = Input.mousePosition;

            // ロープを引っ張っている間に加速度を減少させる
            Debug.Log(smokingDeltaSpeed);
            smokingDeltaSpeed -= smokingDecreaseRate * Time.deltaTime;

            if (smokingDeltaSpeed < -1f)
            {
                Debug.Log(smokingDeltaSpeed + "微少速度");
                smokingDeltaSpeed = -1f;
            }

            player.SetDeltaSpeed(smokingDeltaSpeed);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            rectTransform.anchoredPosition3D = startPosition;
            smokingDeltaSpeed = 0f;  // リセット
            player.SetDeltaSpeed(smokingDeltaSpeed);
        }

        WhistleAction();
    }

    private void WhistleAction()
    {
        if (rectTransform.anchoredPosition3D.y < -400)
        {
            smokeController.SmokeUp();
            Debug.Log(rectTransform.anchoredPosition3D);
            if (!isSmoking)
            {
                isSmoking = true;
                GetComponent<AudioSource>().Play();
            }
            // 汽笛が鳴っている状態にする
            isWhistleBlowing = true;
        }
        else
        {
            isSmoking = false;
            smokeController.SmokeDown();
            // 汽笛が鳴っていない状態にする
            isWhistleBlowing = false;
        }
    }
}
