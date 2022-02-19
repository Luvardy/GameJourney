using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnimationToGame2 : MonoBehaviour
{
    public GameObject aToG;
    public float faderSpeed = 1.5f;
    Image image;
    bool sceneEnd = false;

    public static int sceneCheck = 1;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        
        Destroy(aToG.gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if(aToG.gameObject == null)
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
            SceneManager.LoadScene (20);
        }
    }

    void FaderToBlack()
    {
        image.color = Color.Lerp(image.color, Color.black, faderSpeed*Time.deltaTime);
    }
}
