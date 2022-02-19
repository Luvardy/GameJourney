using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Disapear : MonoBehaviour, IPointerClickHandler
{
    private int destroyCount = 0;

    public AudioClip right;
    public AudioClip wrong;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(ActionGameManager.order == 0 && this.gameObject.tag == "first")
        {
            Destroy (GameObject.FindWithTag("first"));

            SoundManager.instance.PlaySingle(right);

            ActionGameManager.order++;

            destroyCount++;
        }
        else
        { 
            if (ActionGameManager.order == 0)
            {
                SoundManager.instance.PlaySingle(wrong);

                ActionGameManager.hp--;
            }
            
        }

        if(ActionGameManager.order == 1 && this.gameObject.tag == "second")
        {
            SoundManager.instance.PlaySingle(right);

            Destroy (GameObject.FindWithTag("second"));
            ActionGameManager.order++;
        }
        else 
        {   
            if (ActionGameManager.order == 1 && this.gameObject.tag != "first")
            {
                SoundManager.instance.PlaySingle(wrong);

                ActionGameManager.hp--;
            }
            
        }

        if(ActionGameManager.order == 2 && this.gameObject.tag == "third")
        {
            SoundManager.instance.PlaySingle(right);

            Destroy (GameObject.FindWithTag("third"));
            ActionGameManager.order++;
        }
        else 
        {
            if (ActionGameManager.order == 2 && this.gameObject.tag != "second")
            {
                SoundManager.instance.PlaySingle(wrong);

                ActionGameManager.hp--;
            }
            
        }

        if(ActionGameManager.order == 3 && this.gameObject.tag == "fourth")
        {
            SoundManager.instance.PlaySingle(right);

            Destroy (GameObject.FindWithTag("fourth"));

            ActionGameManager.hp = 3;

            ActionGameManager.order = 0;

            ActionGameManager.level++;
            if(ActionGameManager.level <=3)
                SceneManager.LoadScene(ActionGameManager.level);
            else
                SceneManager.LoadScene(15);

        }
        else 
        {
            if (ActionGameManager.order == 3 && this.gameObject.tag != "third")
            {
                SoundManager.instance.PlaySingle(wrong);

                ActionGameManager.hp--;
            }
            
        }
    }
}