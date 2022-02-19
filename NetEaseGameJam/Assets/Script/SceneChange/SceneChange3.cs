using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChange3 : MonoBehaviour
{
    public float faderSpeed = 1.5f;
    Image image;
    bool sceneStart = true;
    bool sceneEnd = false;

    public static int sceneCheck = 1;

    // Start is called before the first frame update
    void Start()
    {
        ClickLetter.endTalkedCount = 0;
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneStart)
        {
            StartScene();
        }
        if(ClickLetter.endTalkedCount == 2)
        {
            sceneEnd = true;
        }
        if (sceneEnd)
        {
            EndScene();
        }
    }

    void StartScene()
    {
        FaderToClear();
        if (image.color.a < 0.05f)
        {
            sceneStart = false;
        }
    }

    void EndScene()
    {
        FaderToBlack();
        if (image.color.a > 0.95f)
        {
            SceneManager.LoadScene (13);
        }
    }

    void FaderToClear()
    {
        image.color = Color.Lerp(image.color, Color.clear, faderSpeed*Time.deltaTime);
    }

    void FaderToBlack()
    {
        image.color = Color.Lerp(image.color, Color.black, faderSpeed*Time.deltaTime);
    }
}
