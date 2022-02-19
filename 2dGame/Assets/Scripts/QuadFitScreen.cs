using UnityEngine;
using System.Collections;
 
public class QuadFitScreen : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        //摄像机必须是正交的
        float height = Camera.main.orthographicSize ;
        float width = height * Camera.main.aspect;
        transform.localScale = new Vector3(width, height, 0.1f);
    }

}