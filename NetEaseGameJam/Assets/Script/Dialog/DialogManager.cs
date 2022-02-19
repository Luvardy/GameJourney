using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DialogManager : MonoBehaviour
{
    [Header("UI组件")]
    public Text charName;
    public Text content;
    public Image backgroundPic;
    public Image leftPic;
    public Image ticket;
    public Image rightPic;
    [Header("脚本参数")]
    public string pictureForlder;

    public static bool endTxt = false;


    public static DialogManager instance;
    // Start is called before the first frame update
    void Start()
    {
        SceneChange.sceneCheck++;//每加载一次新场景加1
        
        ClickGrandpa.stopFirstTalked = false;

        ClickGrandpa.startGrandpaTalked = true;

        ClickGrandpa.endTalkedCount = 0;

        endTxt = false;
        
        Debug.Log("sceneCheck:" + SceneChange.sceneCheck + ClickGrandpa.stopFirstTalked);

        instance = this;

        LoadDialogData.instance.LoadDialogDatas("场景1.txt");
        handleData(LoadDialogData.instance.LoadNext());
    }

    // Update is called once per frame
    void Update()
    {
        if(!ClickGrandpa.stopFirstTalked)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(!endTxt)
                {
                    handleData(LoadDialogData.instance.LoadNext());
                }
                
            }
        }
        
    }

    public void setText(Text text, string content)
    {
        print(text);
        text.text = content;
    }

    public void setImage(Image image, string picName)
    {
        image.sprite = Resources.LoadAll<Sprite>(pictureForlder + "/" + picName)[0];
    }

    public void handleData(DialogData data)
    {
        if(data == null)
            return;
        if(data.type == 0)
        {
            setImage(backgroundPic, data.picName);
            handleData(LoadDialogData.instance.LoadNext());
        }
        else if (data.type == 1)
        {
            if(data.pos.CompareTo("left") == 0)
            {
                leftPic.gameObject.SetActive(true);
                setImage(leftPic, data.picName);
                rightPic.gameObject.SetActive(false);
            }
            else if (data.pos.CompareTo("right") == 0)
            {
                rightPic.gameObject.SetActive(true);
                setImage(rightPic, data.picName);
                if (SceneChange.sceneCheck > 1)
                {
                   leftPic.gameObject.SetActive(false); 
                }
                else
                leftPic.gameObject.SetActive(true);
            }
            else
            {
                ticket.gameObject.SetActive(true);
            }
            setText(charName, data.charName);
            setText(content, data.content);
        }

        else
        {
            rightPic.gameObject.SetActive(false);
            leftPic.gameObject.SetActive(false);
            setText(charName, data.charName);
            setText(content, data.content);
        }
    }
}
