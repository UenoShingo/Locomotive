//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.tvOS;

//public class TrainColorChanger : MonoBehaviour
//{
//    //public enum colorStete
//    //{
//    //    redMode,
//    //    yellowMode, 
//    //    blueMode,
//    //}
//    colorStete mode = colorStete.redMode;

//    [SerializeField]
//    private Renderer trainRenderer;

//    [SerializeField]
//    private Color colorA = Color.red;

//    [SerializeField]
//    private Color colorS = Color.yellow;

//    [SerializeField]
//    private Color colorD = Color.blue;

//    private void Start()
//    {

//        Debug.Log(mode);
//    }

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.A))
//        {
//            ChangeColor(colorA);
//            mode = colorStete.redMode;
//        }
//        else if (Input.GetKeyDown(KeyCode.S))
//        {
//            ChangeColor(colorS);
//            mode = colorStete.yellowMode;
//        }
//        else if (Input.GetKeyDown(KeyCode.D))
//        {
//            ChangeColor(colorD);
//            mode = colorStete.blueMode;
//        }
//        Debug.Log(mode);
//    }

//    private void ChangeColor(Color newColor)
//    {
//        trainRenderer.material.color = newColor;
//    }
//}
