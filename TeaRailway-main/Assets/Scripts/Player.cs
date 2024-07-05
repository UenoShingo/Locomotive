using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField]
    [Tooltip("移動速度を指定します。")]
    private float speed = 2;

    [SerializeField]
    [Tooltip("ジャンプ力を指定します。")]
    private Vector2 jumpPower = new Vector2(0, 6);

    [SerializeField]
    [Tooltip("ジャンプ力を指定します。")]
    private float dashPower = 4.0f;

    private float deltaSpeed = 0f;

    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rigidbody.AddForce(jumpPower, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            deltaSpeed = dashPower;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            deltaSpeed = dashPower -2;
        }



        var velocity = rigidbody.velocity;
        velocity.x = speed + deltaSpeed;
        rigidbody.velocity = velocity;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GreenLeaf"))
        {
            Debug.Log("GreenLeaf");
            deltaSpeed = dashPower;
        }
    }

}
