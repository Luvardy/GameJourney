using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMp3 : MonoBehaviour
{
    public BoxCollider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        if(GlobalControl.instance.mp3)
            gameObject.SetActive(false);
    }
}
