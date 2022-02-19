using UnityEngine.EventSystems;
using UnityEngine;

public class ClickLetter : MonoBehaviour, IPointerClickHandler
{
    public static bool startGrandpaTalked = true;
    public static int endTalkedCount = 0;
    public static bool stopFirstTalked = false;

    void Start()
    {
        endTalkedCount = 0;
        ClickGrandson.stopSecondTalked = false;
        startGrandpaTalked = true;
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
       if(startGrandpaTalked && this.gameObject.tag=="Letter")
       {
            DialogManager.endTxt = false;
            stopFirstTalked = true;
            startGrandpaTalked = false;
            LoadDialogData.instance.LoadDialogDatas("场景2.txt");
            DialogManager.instance.handleData(LoadDialogData.instance.LoadNext());
        }

    }
}
