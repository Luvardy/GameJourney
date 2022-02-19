using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMusic : MonoBehaviour
{
    void start()
    {
        SoundManager.instance.backGround.Stop();
    }
}
