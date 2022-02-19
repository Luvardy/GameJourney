using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{
    float radian = 0;//弧度
    float perRadian = 0.06f;//每帧增加的高度
    float radius = 0.8f;//半径

    Vector2 oldPos;//原来的位置

    void Start()
    {
        oldPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        radian += perRadian;
        float dy = Mathf.Cos(radian) * radius;
        transform.position = oldPos + new Vector2(0, dy);
    }
}
