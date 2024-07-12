using UnityEngine;

public class RopeController : MonoBehaviour
{
    public Player player;

    private RectTransform rectTransform;
    private Vector3 startPosition;
    private Vector3 lastMousePosition;

    [SerializeField] private GameObject smokeObject;
    private SmokeController smokeController;

    private bool isSmoking = false;
    private bool isWhistleBlowing = false;
    private float initialSmokingDecreaseRate = 0.05f; // 初期の減少率
    private float smokingDecreaseRate = 0.01f; // 最終的な減少率
    private float smokingDeltaSpeed = 0f;

    private float decreaseRateChangeSpeed = 0.005f; // 減少率の変化速度

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
            smokingDeltaSpeed = 0f; // 減速率をリセット
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            rectTransform.anchoredPosition3D += new Vector3(0, delta.y, 0);
            lastMousePosition = Input.mousePosition;

            // 減少率を設定する（初期の減少率で）
            smokingDeltaSpeed = -initialSmokingDecreaseRate;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            rectTransform.anchoredPosition3D = startPosition;
            smokingDeltaSpeed = 0f; // 減速率をリセット
        }

        // プレイヤーに減速率を即座に適用する
        player.SetDeltaSpeed(smokingDeltaSpeed);

        WhistleAction();

        // ロープを引っ張っている場合の減速処理
        if (IsRopePulling())
        {
            // ここに減速の具体的な処理を記述する
            if (!IsHittingBarrel())
            {
                smokingDeltaSpeed = -initialSmokingDecreaseRate;
                player.SetDeltaSpeed(smokingDeltaSpeed);
            }
        }
    }

    private void WhistleAction()
    {
        if (rectTransform.anchoredPosition3D.y < -100f)
        {
            Debug.Log("smokeeeeeeeeeee");
            smokeController.SmokeUp();

            if (!isSmoking)
            {
                Debug.Log("test");
                isSmoking = true;
                GetComponent<AudioSource>().Play();
            }

            isWhistleBlowing = true;
        }
        else
        {
            isSmoking = false;
            smokeController.SmokeDown();
            isWhistleBlowing = false;
        }
    }

    public bool IsRopePulling()
    {
        // ロープが引っ張られている場合の条件
        return rectTransform.anchoredPosition3D != startPosition;
    }

    private bool IsHittingBarrel()
    {
        // ロープが樽に当たっているかどうかの判定
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Barrel"))
            {
                return true;
            }
        }

        return false;
    }
}
