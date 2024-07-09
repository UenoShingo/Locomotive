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
    private float smokingDecreaseRate = 0.1f;
    private float smokingDeltaSpeed = 0f;

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

            smokingDeltaSpeed -= smokingDecreaseRate * Time.deltaTime;

            if (smokingDeltaSpeed < -1f)
            {
                smokingDeltaSpeed = -1f;
            }

            player.SetDeltaSpeed(smokingDeltaSpeed);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            rectTransform.anchoredPosition3D = startPosition;
            smokingDeltaSpeed = 0f;
            player.SetDeltaSpeed(smokingDeltaSpeed);
        }

        WhistleAction();
    }

    private void WhistleAction()
    {
        if (rectTransform.anchoredPosition3D.y < -400)
        {
            smokeController.SmokeUp();

            if (!isSmoking)
            {
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
}
