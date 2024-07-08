using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    //色変更系
    public enum colorStete
    {
        redMode,
        yellowMode,
        blueMode,
    }
    colorStete mode = colorStete.redMode;

    [SerializeField]
    private Renderer trainRenderer;

    [SerializeField]
    private Color colorA = Color.red;

    [SerializeField]
    private Color colorS = Color.yellow;

    [SerializeField]
    private Color colorD = Color.blue;

    [SerializeField]
    private ParticleSystem smokeParticleSystem;  // パーティクルシステムの参照

    //移動系
    [SerializeField]
    [Tooltip("移動速度を指定します。")]
    private float speed = 2;

    [SerializeField]
    [Tooltip("ジャンプ力を指定します。")]
    private Vector2 jumpPower = new Vector2(0, 6);

    [SerializeField]
    [Tooltip("加速力を指定します。")]
    private float dashPower = 4.0f;

    private float deltaSpeed = 0f;

    private Rigidbody rigidbody;

    private bool isWhistleBlowing = false;  // 汽笛が鳴っているかどうかのフラグ

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeColor(colorA);
            mode = colorStete.redMode;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ChangeColor(colorS);
            mode = colorStete.yellowMode;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeColor(colorD);
            mode = colorStete.blueMode;
        }

        var velocity = rigidbody.velocity;
        velocity.x = speed + deltaSpeed;
        rigidbody.velocity = velocity;

        // deltaSpeedはRopeControllerからリセットされるためここではリセットしない
        //deltaSpeed = 0f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("yellowLeaf"))
        {
            if (mode == colorStete.yellowMode)
            {
                Debug.Log("yellowLeaf");
                deltaSpeed = dashPower;
            }
            else
            {
                if (!isWhistleBlowing)
                {
                    deltaSpeed = -0.25f * dashPower;
                }
            }
        }
    }

    public void SetDeltaSpeed(float newDeltaSpeed)
    {
        deltaSpeed = newDeltaSpeed;
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
}