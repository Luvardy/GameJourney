using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image image;

    public float loadTime = 10f;

    public bool isClick = false;

    public AudioClip right;
    public AudioClip wrong;

    void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isClick)
            image.color = new Color(210/255f, 247/255f, 1, 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!isClick)
            image.color = new Color(1, 1, 1, 1);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(this.gameObject.tag == "Answer")
        {   
            SoundManager.instance.PlaySingle(right);
            isClick = true;
            image.color = new Color(200/255f, 248/255f, 200/255f, 1);
            Invoke("Loading", 1f);
        }

        else
        {
            SoundManager.instance.PlaySingle(wrong);
            isClick = true;
            image.color = new Color(251/255f, 67/255f, 86/255f, 1);
            Invoke("Loading", 1f);
        }
    }

    public void Loading()
    {
        Debug.Log(SceneChange.thirdSceneCheck);
        SceneManager.LoadScene(SceneChange.thirdSceneCheck++);
    }
}