using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCamera : MonoBehaviour
{
    [SerializeField]
    [Tooltip("追尾対象を指定します。")]
    private Transform target = null;

    [SerializeField]
    [Tooltip("追尾対象とのオフセット値を指定します。")]
    private Vector2 offset = new Vector2(4, 1.5f);

    void Start()
    {
        var position = transform.position;
        position.x = target.position.x + offset.x;
        position.y = target.position.y + offset.y;
        transform.position = position;
    }

    void Update()
    {
        var position = transform.position;
        position.x = target.position.x + offset.x;
        position.y = target.position.y + offset.y;
        transform.position = position;
    }
}
