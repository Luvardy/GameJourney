using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour {
 
    public static GlobalControl instance;
 
    //要保存使用的数据;
    public int fileNum;
    public bool ruler = false;
    public bool mp3 = false;
    public bool file = false;
    public bool diary = false;
 
   //初始化
    private void Awake()
    {
         if(instance==null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
         else if(instance!=null)
        {
            Destroy(gameObject);
        }
    }
}