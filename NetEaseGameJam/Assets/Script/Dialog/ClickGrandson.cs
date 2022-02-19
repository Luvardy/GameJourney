using UnityEngine.EventSystems;
using UnityEngine;

public class ClickGrandson : MonoBehaviour, IPointerClickHandler
{
    public static bool startGrandsonTalked = false;

    public static bool stopSecondTalked = false;
    // Update is called once per frame
    void Update()
    {
        if(stopSecondTalked && Input.GetMouseButtonDown(0))
        {
            if(!DialogManager.endTxt)
            DialogManager.instance.handleData(LoadDialogData.instance.LoadNext());
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       if(!ClickGrandpa.iAmFuckingTalking && startGrandsonTalked && this.gameObject.tag=="grandson")
       {
            DialogManager.endTxt = false;
            stopSecondTalked = true;
            startGrandsonTalked = false;
            LoadDialogData.instance.LoadDialogDatas("孙子对话.txt");
            DialogManager.instance.handleData(LoadDialogData.instance.LoadNext());
        }

    }
}
