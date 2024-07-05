using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainColorChanger : MonoBehaviour
{
    [SerializeField]
    private Renderer trainRenderer;

    [SerializeField]
    private Color colorA = Color.red;

    [SerializeField]
    private Color colorS = Color.green;

    [SerializeField]
    private Color colorD = Color.blue;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeColor(colorA);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ChangeColor(colorS);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeColor(colorD);
        }
    }

    private void ChangeColor(Color newColor)
    {
        trainRenderer.material.color = newColor;
    }
}
