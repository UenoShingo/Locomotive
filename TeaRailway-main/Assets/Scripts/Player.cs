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
    [SerializeField] private float baseDashPower = 4.0f; // äÓñ{ÇÃâ¡ë¨óÕ
    private float dashPower = 4.0f; // åªç›ÇÃâ¡ë¨óÕ

    private float deltaSpeed = 0f;
    private float whistleDeltaSpeed = 0f;
    private Rigidbody rb;
    private bool isWhistleBlowing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        velocity.x = speed + deltaSpeed + whistleDeltaSpeed;
        rb.velocity = velocity;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("yellowLeaf"))
        {
            if (mode == ColorState.Yellow)
            {
                Debug.Log("yellowLeaf");
                deltaSpeed += dashPower; // dashPowerÇâ¡éZ
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

        if (other.CompareTag("redLeaf"))
        {
            if (mode == ColorState.Red)
            {
                Debug.Log("redLeaf");
                deltaSpeed += dashPower; // dashPowerÇâ¡éZ
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

        if (other.CompareTag("blueLeaf"))
        {
            if (mode == ColorState.Blue)
            {
                Debug.Log("blueLeaf");
                deltaSpeed += dashPower; // dashPowerÇâ¡éZ
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
            if (!isWhistleBlowing)  // ãDìJÇ™ñ¬Ç¡ÇƒÇ¢ÇÈÇ»ÇÁÅAå∏ë¨ÇµÇ»Ç¢
            {
                if (mode == ColorState.Blue)
                {
                    Debug.Log("Barrel");
                    deltaSpeed += dashPower; // dashPowerÇâ¡éZ
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
    }

    private void ChangeColor(Color newColor)
    {
        trainRenderer.material.color = newColor;
        ChangeSmokeColor(newColor);  // âåÇÃÉpÅ[ÉeÉBÉNÉãÇÃêFÇ‡çXêV
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
