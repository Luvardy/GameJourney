using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogToAnimation2 : MonoBehaviour
{
    public float faderSpeed = 1.5f;
    Image image;
    bool sceneEnd = false;

    public static int sceneCheck = 1;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ClickLetter.endTalkedCount == 3) 
        {
            sceneEnd = true;
        }
        if (sceneEnd)
        {
            EndScene();
        }
    }

    void EndScene()
    {
        FaderToBlack();
        if (image.color.a > 0.95f)
        {
            SceneManager.LoadScene (14);
        }
    }

    void FaderToBlack()
    {
        image.color = Color.Lerp(image.color, Color.black, faderSpeed*Time.deltaTime);
    }
}
