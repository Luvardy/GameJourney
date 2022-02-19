using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Go : MonoBehaviour, IPointerClickHandler
{
    public static bool clickTicket = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ClickGrandpa.canGo)
        {
            clickTicket = true;
        }
    }
}
