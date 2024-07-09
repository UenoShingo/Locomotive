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

    [SerializeField] private float maxSpeed = 10.0f; // 最大速度の設定

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ropeController = FindObjectOfType<RopeController>(); // RopeControllerを取得
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeColor(colorA);
            mode = ColorState.Red;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ChangeColor(colorS);
            mode = ColorState.Yellow;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeColor(colorD);
            mode = ColorState.Blue;
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
        if (velocity.x < 0)
        {
            velocity.x = 0;
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
            }
            else if (mode == ColorState.Red && other.CompareTag("redLeaf"))
            {
                Debug.Log("redLeaf");
                deltaSpeed += dashPower; // dashPowerを加算
            }
            else if (mode == ColorState.Blue && other.CompareTag("blueLeaf"))
            {
                Debug.Log("blueLeaf");
                deltaSpeed += dashPower; // dashPowerを加算
            }
            else
            {
                if (!isWhistleBlowing)
                {
                    Debug.Log("CollarError");
                    deltaSpeed = -0.25f * dashPower;
                }
            }
        }

        if (other.CompareTag("Barrel"))
        {
            if (!isWhistleBlowing && (ropeController == null || !ropeController.IsRopePulling())) // RopeControllerがないか、引っ張られていない場合
            {
                if (mode == ColorState.Blue)
                {
                    Debug.Log("Barrel");
                    deltaSpeed += dashPower; // dashPowerを加算
                }
                else
                {
                    Debug.Log("CollarError");
                    deltaSpeed = -0.27f * dashPower;
                }
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
        trainRenderer.material.color = newColor;
        ChangeSmokeColor(newColor);  // 煙のパーティクルの色も更新
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
