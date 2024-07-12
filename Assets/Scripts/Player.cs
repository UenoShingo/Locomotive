using UnityEngine;

public class Player : MonoBehaviour
{
    public enum ColorState
    {
        Red,
        Yellow,
        Blue,
    }
    ColorState mode = ColorState.Red;

    [SerializeField] private Renderer trainRenderer;
    [SerializeField] private Color colorA = Color.red;
    [SerializeField] private Color colorS = Color.yellow;
    [SerializeField] private Color colorD = Color.blue;
    [SerializeField] private ParticleSystem smokeParticleSystem;

    [SerializeField] private float speed = 2;
    [SerializeField] private float jumpPower = 6;
    [SerializeField] private float baseDashPower = 4.0f; // 基本の加速力
    private float dashPower = 4.0f; // 現在の加速力

    private float deltaSpeed = 0f;
    private float whistleDeltaSpeed = 0f;
    private Rigidbody rb;
    private bool isWhistleBlowing = false;

    private RopeController ropeController; // RopeControllerを保持する変数を追加

    [SerializeField] private float maxSpeed = 27.78f; // 最大速度の設定

    private float LeafDownSpeed = -0.25f; // 茶葉との衝突時の減速率
    [SerializeField] private float BarrelDownSpeed = -0.27f; // バレルとの衝突時の減速率

    [SerializeField] private GameObject Canvas;

    private UIManager uIManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ropeController = FindObjectOfType<RopeController>(); // RopeControllerを取得

        uIManager = Canvas.GetComponent<UIManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeColor(colorD);
            mode = ColorState.Red;
            uIManager.UIChangeRed();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ChangeColor(colorS);
            mode = ColorState.Yellow;
            uIManager.UIChangeYellow();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeColor(colorA);
            mode = ColorState.Blue;
            uIManager.UIChangeBlue();
        }

        Vector3 velocity = rb.velocity;
        float currentSpeed = speed + deltaSpeed + whistleDeltaSpeed;

        // 最大速度を超えないように制限する
        if (currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }

        velocity.x = currentSpeed;

        // 後ろに進まないようにする条件を追加
        if (velocity.x < 2)
        {
            velocity.x = 2;
            deltaSpeed = 0;
            whistleDeltaSpeed = 0;
        }

        rb.velocity = velocity;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("yellowLeaf") || other.CompareTag("redLeaf") || other.CompareTag("blueLeaf"))
        {
            if (mode == ColorState.Yellow && other.CompareTag("yellowLeaf"))
            {
                Debug.Log("yellowLeaf");
                deltaSpeed += dashPower; // dashPowerを加算
                GetComponent<AudioSource>().Play();
            }
            else if (mode == ColorState.Red && other.CompareTag("redLeaf"))
            {
                Debug.Log("redLeaf");
                deltaSpeed += dashPower; // dashPowerを加算
                GetComponent<AudioSource>().Play();
            }
            else if (mode == ColorState.Blue && other.CompareTag("blueLeaf"))
            {
                Debug.Log("blueLeaf");
                deltaSpeed += dashPower; // dashPowerを加算
                GetComponent<AudioSource>().Play();
            }
            else
            {
                if (!isWhistleBlowing)
                {
                    Debug.Log("CollarError");
                    deltaSpeed += LeafDownSpeed * dashPower;
                }
            }
        }

        if (other.CompareTag("Barrel"))
        {
            if (!isWhistleBlowing && (ropeController == null || !ropeController.IsRopePulling())) // RopeControllerがないか、引っ張られていない場合
            {
                //if (mode == ColorState.Blue)
                //{
                //    Debug.Log("Barrel");
                //    deltaSpeed += dashPower; // dashPowerを加算
                //}
                //else
                //{
                Debug.Log("CollarError");
                deltaSpeed += BarrelDownSpeed * dashPower;
                //}
            }
        }
    }

    public void SetDeltaSpeed(float newDeltaSpeed)
    {
        whistleDeltaSpeed = newDeltaSpeed;
        
        // RopeControllerからの減速率を適用する
        deltaSpeed += newDeltaSpeed;
    }

    private void ChangeColor(Color newColor)
    {
        ChangeSmokeColor(newColor);  // 煙のパーティクルの色も更新
         //trainRenderer.material.color = newColor;

    }

    private void ChangeSmokeColor(Color newColor)
    {
        var main = smokeParticleSystem.main;
        main.startColor = newColor;
    }

    public void SetWhistleBlowing(bool isBlowing)
    {
        isWhistleBlowing = isBlowing;
    }

    public void SetDashPower(float newDashPower)
    {
        dashPower = newDashPower;
    }
}
