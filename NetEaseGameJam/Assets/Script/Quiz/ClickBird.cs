using UnityEngine.EventSystems;
using UnityEngine;

public class ClickBird : MonoBehaviour, IPointerClickHandler
{
    public static bool startGrandsonTalked = false;

    public static bool stopSecondTalked = false;

    void start()
    {
        startGrandsonTalked = false;
    }
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
       if(startGrandsonTalked && this.gameObject.tag=="Bird")
       {
            DialogManager.endTxt = false;
            stopSecondTalked = true;
            startGrandsonTalked = false;
            LoadDialogData.instance.LoadDialogDatas("bird.txt");
            DialogManager.instance.handleData(LoadDialogData.instance.LoadNext());
        }

    }
}
