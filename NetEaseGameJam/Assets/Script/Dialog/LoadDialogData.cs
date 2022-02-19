using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadDialogData : MonoBehaviour
{
    public string dialogForlder;
    public static LoadDialogData instance;

    private int index;//对话每一行的索引
    private List<string> txt;

    void Awake()
    {
        instance = this;
        index = 0;
    }

    public void LoadDialogDatas(string txtFileName)
    {
        index = 0;
        txt = new List<string>();
        StreamReader stream = new StreamReader("Assets/" + dialogForlder + "/" + txtFileName);

        while(!stream.EndOfStream)
        {
            txt.Add(stream.ReadLine());//对txt文件中的每一行进行读取并存入txt向量中
            
        }
        stream.Close();
    }

    public DialogData LoadNext()
    {
        if(index < txt.Count)
        {
            string[] datas = txt[index].Split(',');//根据逗号区分需要分别放入数组的data

            int type = int.Parse(datas[0]);

            if(type == 0)
            {
                string picName = datas[1];
                print(datas[1]);
                index++;
                return new DialogData(type, picName);
            }

            else if(type == 1)
            {
                string pos = datas[1];
                string name = datas[2];
                string content = datas[3];
                string picName = datas[4];
                index++;
                return new DialogData(type, pos, name, content, picName);
            }

            else
            {
                string name = datas[1];
                string content = datas[2];
                index++;
                return new DialogData(type, name, content);
            }
        }
        else
        {
            DialogManager.endTxt = true;
            ClickGrandpa.endTalkedCount++;
            ClickLetter.endTalkedCount++;
            Debug.Log(ClickLetter.endTalkedCount);
            ClickGrandpa.iAmFuckingTalking = false;
            if(ClickGrandpa.endTalkedCount == 2)
                ClickGrandson.startGrandsonTalked = true;
            if(ClickLetter.endTalkedCount == 2 )
                ClickBird.startGrandsonTalked = true;
            Debug.Log(ClickBird.startGrandsonTalked);
            return null;
        }
    }

    
}