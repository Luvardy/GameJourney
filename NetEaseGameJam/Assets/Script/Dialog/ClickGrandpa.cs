using UnityEngine.EventSystems;
using UnityEngine;

public class ClickGrandpa : MonoBehaviour, IPointerClickHandler
{
    public static bool startGrandpaTalked = true;
    public static int endTalkedCount = 0;
    public static bool stopFirstTalked = false;
    public static bool iAmFuckingTalking = false;
    public static bool canGo = false;

    void Start()
    {
        canGo = false;
        endTalkedCount = 0;
        ClickGrandson.stopSecondTalked = false;
    }

    void Update()
    {
        if (!ClickGrandson.stopSecondTalked)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(!DialogManager.endTxt)
                DialogManager.instance.handleData(LoadDialogData.instance.LoadNext());
            }
        }
         
    }

     public void OnPointerClick(PointerEventData eventData)
    {
       if(startGrandpaTalked && this.gameObject.tag=="grandpa")
       {
            DialogManager.endTxt = false;
            stopFirstTalked = true;
            startGrandpaTalked = false;
            LoadDialogData.instance.LoadDialogDatas("场景2.txt");
            DialogManager.instance.handleData(LoadDialogData.instance.LoadNext());
        }

        if(endTalkedCount >= 2 && this.gameObject.tag=="grandpa")
        {  
            canGo = true;
            iAmFuckingTalking = true;
            DialogManager.endTxt = false;
            endTalkedCount = 1;
            ClickLetter.endTalkedCount = 1;
            LoadDialogData.instance.LoadDialogDatas("场景3.txt");
            DialogManager.instance.handleData(LoadDialogData.instance.LoadNext());
        }
    }
}
