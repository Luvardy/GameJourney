using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReLoadBGM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.backGround.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
