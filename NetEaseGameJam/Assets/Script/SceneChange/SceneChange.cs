using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public float faderSpeed = 1.5f;
    Image image;
    bool sceneStart = true;
    bool sceneEnd = false;

    public static int sceneCheck = 0;

    public static int thirdSceneCheck = 8;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneStart)
        {
            StartScene();
        }
        if(ClickGrandpa.endTalkedCount == 3)
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
            SceneManager.LoadScene (19);
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
