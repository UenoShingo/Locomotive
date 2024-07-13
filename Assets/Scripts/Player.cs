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
    [SerializeField] private Color colorA = Color.blue;
    [SerializeField] private Color colorS = Color.yellow;
    [SerializeField] private Color colorD = Color.red;
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

    [SerializeField] private float yellowLeafDownSpeed = -0.25f;
    [SerializeField] private float redLeafDownSpeed = -0.27f; // バレルとの衝突時の減速率を参照して-0.27fに修正
    [SerializeField] private float blueLeafDownSpeed = -0.25f;

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
            ChangeColor(colorA);
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
            ChangeColor(colorD);
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
            else if (mode == ColorState.Blue && other.CompareTag("redLeaf")) // BlueLeafの処理をRedLeafと入れ替え
            {
                Debug.Log("blueLeaf"); // 実際にはRedLeafですが、処理の中身を修正する必要があります
                deltaSpeed += dashPower; // dashPowerを加算
                GetComponent<AudioSource>().Play();
            }
            else if (mode == ColorState.Red && other.CompareTag("blueLeaf")) // RedLeafの処理をBlueLeafと入れ替え
            {
                Debug.Log("redLeaf"); // 実際にはBlueLeafですが、処理の中身を修正する必要があります
                deltaSpeed += dashPower; // dashPowerを加算
                GetComponent<AudioSource>().Play();
            }
            else
            {
                if (!isWhistleBlowing)
                {
                    Debug.Log("CollarError");
                    if (other.CompareTag("yellowLeaf"))
                    {
                        deltaSpeed += yellowLeafDownSpeed * dashPower;
                    }
                    else if (other.CompareTag("redLeaf"))
                    {
                        deltaSpeed += redLeafDownSpeed * dashPower;
                    }
                    else if (other.CompareTag("blueLeaf"))
                    {
                        deltaSpeed += blueLeafDownSpeed * dashPower;
                    }
                }
            }
        }

        // ここにOnTriggerEnterの残りの処理を記述する...
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
