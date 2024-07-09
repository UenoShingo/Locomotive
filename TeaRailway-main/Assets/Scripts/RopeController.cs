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
    private bool isWhistleBlowing = false;  // ‹D“J‚ª–Â‚Á‚Ä‚¢‚é‚©‚Ç‚¤‚©‚Ìƒtƒ‰ƒO
    private float smokingDecreaseRate = 0.1f;  // ‰Á‘¬“x‚ÌŒ¸­—¦
    private float smokingDeltaSpeed = 0f;  // Œ¸­‚µ‚½‰Á‘¬“x‚Ì‡Œv

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

            // ƒ[ƒv‚ğˆø‚Á’£‚Á‚Ä‚¢‚éŠÔ‚É‰Á‘¬“x‚ğŒ¸­‚³‚¹‚é
            Debug.Log(smokingDeltaSpeed);
            smokingDeltaSpeed -= smokingDecreaseRate * Time.deltaTime;

            if (smokingDeltaSpeed < -1f)
            {
                Debug.Log(smokingDeltaSpeed + "”÷­‘¬“x");
                smokingDeltaSpeed = -1f;
            }

            player.SetDeltaSpeed(smokingDeltaSpeed);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            rectTransform.anchoredPosition3D = startPosition;
            smokingDeltaSpeed = 0f;  // ƒŠƒZƒbƒg
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
            // ‹D“J‚ª–Â‚Á‚Ä‚¢‚éó‘Ô‚É‚·‚é
            isWhistleBlowing = true;
            player.SetWhistleBlowing(true);  // ‹D“Jó‘Ô‚ğPlayer‚É“`‚¦‚é
        }
        else
        {
            isSmoking = false;
            smokeController.SmokeDown();
            // ‹D“J‚ª–Â‚Á‚Ä‚¢‚È‚¢ó‘Ô‚É‚·‚é
            isWhistleBlowing = false;
            player.SetWhistleBlowing(false);  // ‹D“Jó‘Ô‚ğPlayer‚É“`‚¦‚é
        }
    }
}
