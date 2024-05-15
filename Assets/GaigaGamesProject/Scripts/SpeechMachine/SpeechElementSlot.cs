using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static Utils;

public class SpeechElementSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private SpeechElements speechElementSlotID;

    public SpeechMachineGameEvent elementDetected;

    private void Start()
    {
        elementDetected.AddListener(GameObject.FindGameObjectWithTag("SpeechMachineController").GetComponent<SpeechMachineGameManager>().ElementInSlotDetected);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        // Catch object being dragged by the mouse
        if(eventData.pointerDrag != null)
        {
            var speechElementDragged = eventData.pointerDrag.gameObject.GetComponent<DragDrop>();
            var speechElementID = speechElementDragged.GetSpeechElementID();

            // Position are reset here
            if (speechElementSlotID == speechElementID)
            {
                Debug.Log("Speech Element is equal!");
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            }
            else
            {
                Debug.Log("Speech Element is different. Resetting!");
                speechElementDragged.ResetPosition();
            }

            // Analyse of logic is made in manager
            Debug.Log("Speech Element ID detected, sending event!");
            elementDetected.Invoke(speechElementSlotID, speechElementID);
        }

        Debug.Log(eventData.pointerDrag.gameObject.GetComponent<DragDrop>());

        // On element detection invoke event


    }
}
