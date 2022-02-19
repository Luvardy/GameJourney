using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hp : MonoBehaviour
{
    public Image Hp1;
    public Image Hp2;
    public Image Hp3;
    // Update is called once per frame
    void Update()
    {
        LeftHpCheck();
    }

    void LeftHpCheck()
    {
        if(ActionGameManager.hp == 2)
        {
            Hp3.gameObject.SetActive(false);
        }

        if(ActionGameManager.hp == 1)
        {
            Hp2.gameObject.SetActive(false);
        }

        if (ActionGameManager.hp == 0)
        {
            Hp1.gameObject.SetActive(false);
            SceneManager.LoadScene(4);
        }
    }
}
