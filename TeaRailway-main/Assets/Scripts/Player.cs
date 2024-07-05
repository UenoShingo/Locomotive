using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    [Tooltip("移動速度を指定します。")]
    private float speed = 2;

    [SerializeField]
    [Tooltip("ジャンプ力を指定します。")]
    private Vector2 jumpPower = new Vector2(0, 6);

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

        var velocity = rigidbody.velocity;
        velocity.x = speed;
        rigidbody.velocity = velocity;
    }
}
