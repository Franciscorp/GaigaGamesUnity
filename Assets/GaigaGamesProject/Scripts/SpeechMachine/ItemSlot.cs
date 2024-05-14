using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private SpeechMachineGameManager.SpeechElements speechElementID;


    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        // Catch object being dragged by the mouse
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }

        Debug.Log(eventData.pointerDrag.gameObject.GetComponent<DragDrop>());

        if (speechElementID == eventData.pointerDrag.gameObject.GetComponent<DragDrop>().GetSpeechElementID())
        {
            Debug.Log("Speech Element ID is equal!");
        }
    }
}
