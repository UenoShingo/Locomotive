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
    [SerializeField] private float baseDashPower = 4.0f; // äÓñ{ÇÃâ¡ë¨óÕ
    private float dashPower = 4.0f; // åªç›ÇÃâ¡ë¨óÕ

    private float deltaSpeed = 0f;
    private float whistleDeltaSpeed = 0f;
    private Rigidbody rb;
    private bool isWhistleBlowing = false;

    [SerializeField] private float maxSpeed = 27.78f; // ç≈ëÂë¨ìxÇÃê›íË

    [SerializeField] private float yellowLeafDownSpeed = -0.25f;
    [SerializeField] private float redLeafDownSpeed = -0.27f;
    [SerializeField] private float blueLeafDownSpeed = -0.25f;

    [SerializeField] private float blueLeafDashPower = 4.0f; // BlueLeafÇÃâ¡ë¨óÕ
    [SerializeField] private float redLeafDashPower = 4.0f; // RedLeafÇÃâ¡ë¨óÕ

    [SerializeField] private GameObject Canvas;
    private UIManager uIManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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

        // Clamp velocity to ensure it doesn't exceed maxSpeed
        if (currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }

        velocity.x = currentSpeed;

        // Ensure player doesn't move backward slower than 2 units per second
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
                deltaSpeed += dashPower;
                GetComponent<AudioSource>().Play();
            }
            else if (mode == ColorState.Red && other.CompareTag("blueLeaf"))
            {
                Debug.Log("blueLeaf");
                deltaSpeed += blueLeafDashPower; // Use blueLeafDashPower for BlueLeaf
                GetComponent<AudioSource>().Play();
            }
            else if (mode == ColorState.Blue && other.CompareTag("redLeaf"))
            {
                Debug.Log("redLeaf");
                deltaSpeed += redLeafDashPower; // Use redLeafDashPower for RedLeaf
                GetComponent<AudioSource>().Play();
            }
            else
            {
                Debug.Log("Unexpected collision: " + other.tag);
                if (!isWhistleBlowing)
                {
                    float decelerationRate = 0f;
                    if (other.CompareTag("yellowLeaf"))
                    {
                        decelerationRate = yellowLeafDownSpeed;
                    }
                    else if (other.CompareTag("redLeaf"))
                    {
                        decelerationRate = redLeafDownSpeed;
                    }
                    else if (other.CompareTag("blueLeaf"))
                    {
                        decelerationRate = blueLeafDownSpeed;
                    }

                    deltaSpeed += dashPower * decelerationRate;
                }
            }
        }
        else
        {
            Debug.Log("Non-leaf collider entered: " + other.tag);
        }
    }

    public void SetDeltaSpeed(float newDeltaSpeed)
    {
        whistleDeltaSpeed = newDeltaSpeed;
        deltaSpeed += newDeltaSpeed;
    }

    private void ChangeColor(Color newColor)
    {
        ChangeSmokeColor(newColor);
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
